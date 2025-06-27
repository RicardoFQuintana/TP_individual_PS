using Microsoft.AspNetCore.Mvc;
using _3_Aplicacion.Dto.Request;
using _3_Aplicacion.Dto.Response;
using _3_Aplicacion.Interfaces.IServices;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProyectoCreacionService _projectCreateS;
        private readonly IProyectoConsultaService _projectCS;
        private readonly IProyectoFlujoService _projectFS;
        private readonly IProyectoPasoConsultaService _projectPCS;
        private readonly IAprobacionDecisionService _approvalDS;
        private readonly IAprobacionConsultaService _approvalCS;
        private readonly IProyectoActualizacionService _projectAS;
        private readonly IProyectoValidacionService _projectVS;
        private readonly IUsuarioAutenticacionService _userAS;

        public ProjectController(IProyectoCreacionService projectCreateS, IProyectoConsultaService projectCS, IProyectoFlujoService projectFS, IProyectoPasoConsultaService projectPCS, IUsuarioAutenticacionService userAS,
                                            IAprobacionDecisionService approvalDS, IProyectoActualizacionService projectAS, IProyectoValidacionService projectVS, IAprobacionConsultaService approvalCS)
        {
            _approvalDS = approvalDS;
            _approvalCS = approvalCS;
            _projectCreateS = projectCreateS;
            _projectCS = projectCS;
            _projectFS = projectFS;
            _projectPCS = projectPCS;
            _projectAS = projectAS;
            _projectVS = projectVS;
            _userAS = userAS;
        }

        //GET /api/Project?title=&statusId=&createdByUserId=&approverUserId=
        [HttpGet]
        public async Task<ActionResult<List<ProjectShort>>> GetProjects([FromQuery] string? title, [FromQuery] int? status, [FromQuery] int? applicant,
                                                                            [FromQuery] int? approvalUser,[FromQuery] int? typeId, [FromQuery] int? areaId)
        {
            try
            {

                var proyectos = await _projectCS.ListarProyectosFiltrados(title, status, applicant, approvalUser, typeId, areaId);

                if (proyectos.Count == 0)
                    return BadRequest(new ApiError { message = "Parámetro de consulta inválido" });

                var resultado = proyectos
                    .Select(p => new ProjectShort
                    {
                        Id = p.Id,
                        title = p.Title,
                        description = p.Description,
                        amount = (double)p.EstimatedAmount,
                        duration = p.EstimatedDuration,
                        area = p.Area?.Name ?? "Área no especificada",
                        status = p.Status?.Name ?? "Área no especificada",
                        type = p.Type?.Name ?? "Área no especificada"
                    }).ToList();

                return Ok(resultado);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Error al obtener proyectos: {ex.Message}" });
            }
        }

        //OPTIONS => POST /api/Project (POST usado para crear un nuevo recurso)
        [HttpPost]
        public async Task<IActionResult> CrearPropuesta([FromBody] ProjectCreate project)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(project.title) || string.IsNullOrWhiteSpace(project.description) || project.amount <= 0 || project.duration <= 0 ||
                                                project.area <= 0 || project.area >=5 || project.type <= 0 || project.type >= 5 || project.user <= 0)
                    return BadRequest(new ApiError { message = "Datos del proyecto inválidos" });

                var propuestaYaExiste = await _projectVS.ExisteTitulo(project.title);
                if (propuestaYaExiste)
                    return Conflict(new ApiError { message = "Ya existe una propuesta con ese título." });

                var usuario = await _userAS.Login(project.user);
                if (usuario == null)
                    return BadRequest(new ApiError { message = "Usuario no válido" });

                var resultado = await _projectCreateS.CrearPropuesta(project);

                var proyecto = await _projectCreateS.PropuestaCompleta(resultado.Id);

                return StatusCode(201, proyecto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Error inesperado al crear la propuesta: {ex.Message}" });
            }
        }

        //OPTIONS => PATCH /api/Project/{id}/decision (PATCH usado para modificar un recurso existente)
        [HttpPatch("{id}/decision")]
        public async Task<IActionResult> TomarDecision(Guid id, [FromBody] DecisionStep dto)
        {
            try
            {
                // Validación base
                if (id == Guid.Empty)
                    return BadRequest(new ApiError { message = "ID de proyecto inválido." });

                if (dto == null)
                    return BadRequest(new ApiError { message = "Datos de decisión no enviados." });

                if (dto.user <= 0 || dto.id <= 0 || dto.status < 2 || dto.status > 4)
                    return BadRequest(new ApiError { message = "Datos de decisión inválidos" });

                var proyect = await _projectCS.ObtenerPropuestaPorId(id);
                if (proyect == null)
                    return NotFound(new ApiError { message = "Proyecto no encontrado" });

                if (proyect.StatusId != 1) 
                    return Conflict(new ApiError { message = "El proyecto ya no se encuentra en un estado que permite modificaciones" });

                var paso = await _projectPCS.ObtenerPasoPorId(dto.id);
                if (paso == null)
                    return NotFound(new ApiError { message = "Paso de aprobación no encontrado." });

                if (paso.StatusId != 1)
                    return Conflict(new ApiError { message = "El paso ya fue evaluado." });

                var pasos = await _projectPCS.ObtenerPasosDePropuesta(id);
                var anteriorPendiente = pasos.Any(p => p.StepOrder < paso.StepOrder && p.StatusId == 1);

                if (anteriorPendiente)
                    return Conflict(new ApiError { message = "No se puede aprobar este paso aún. Hay pasos anteriores pendientes." });

                bool resultado = dto.status switch
                {
                    2 => await _approvalDS.AprobarPaso(dto.id, dto.user, dto.observation ?? ""),
                    3 => await _approvalDS.RechazarPaso(dto.id, dto.user, dto.observation ?? ""),
                    4 => await _approvalDS.ObservarPaso(dto.id, dto.user, dto.observation ?? ""),
                    _ => false
                };

                if (!resultado)
                    return BadRequest(new ApiError { message = "No se pudo aplicar la decisión. Verificá las reglas." });

                await _projectFS.EvaluarEstadoPropuesta(id);

                var proyecto = await _projectCreateS.PropuestaCompleta(id);

                return Ok(proyecto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Error inesperado al actualizar la propuesta: {ex.Message}" });
            }
        }

        //OPTIONS => PATCH /api/Project/{id} (PUT usado para modificar un recurso existente)
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditarPropuesta(Guid id, [FromBody] ProjectUpdate update)
        {
            try
            {

                if (id == Guid.Empty)
                    return BadRequest(new ApiError { message = "ID de proyecto inválido." });

                if (string.IsNullOrWhiteSpace(update.title) || string.IsNullOrWhiteSpace(update.description) || update.duration <= 0 )
                    return BadRequest(new ApiError { message = "Datos de actualización inválidos" });

                var propuesta = await _projectCS.ObtenerPropuestaPorId(id);
                if (propuesta == null)
                    return NotFound(new ApiError { message = "Proyecto no encontrado" });

                if (propuesta.StatusId != 4) // Observed
                    return Conflict(new ApiError { message = "El proyecto ya no se encuentra en un estado que permite modificaciones" });

                if (!string.Equals(propuesta.Title.Trim(), update.title.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    var propuestaYaExiste = await _projectVS.ExisteTitulo(update.title);
                    if (propuestaYaExiste)
                        return Conflict(new ApiError { message = "Ya existe una propuesta con ese título." });
                }


                propuesta.Title = update.title;
                propuesta.Description = update.description;
                propuesta.EstimatedDuration = update.duration;

                await _projectAS.ActualizarPropuesta(propuesta);

                var proyecto = await _projectCreateS.PropuestaCompleta(id);

                return Ok(proyecto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Error inesperado al actualizar la propuesta: {ex.Message}" });
            }
        }

        //GET /api/Project/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(Guid id)
        {
            try
            {

                if (id == Guid.Empty)
                    return BadRequest(new ApiError { message = "ID de proyecto inválido." });

                var proyecto = await _projectCS.ObtenerPropuestaPorId(id);
                if (proyecto == null)
                    return NotFound(new ApiError { message = "Proyecto no encontrado" });

                var proyect = await _projectCreateS.PropuestaCompleta(id);

                return Ok(proyect);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Ocurrió un error al obtener el proyecto: {ex.Message}" });
            }
        }

        [HttpGet("pendientes/{userId}")]
        public async Task<ActionResult<List<ApprovalStep>>> GetPasosPendientes(int userId)
        {
            try
            {
                var usuario = await _userAS.Login(userId);

                if (usuario == null)
                    return NotFound(new ApiError { message = "Usuario no encontrado." });

                var pasosFiltrados = await _approvalCS.ObtenerPasosPendientesFiltrados(usuario);

                if (!pasosFiltrados.Any())
                    return Ok(new List<ApprovalStep>());

                var resultado = pasosFiltrados.Select(p => new ApprovalStep
                {
                    id = p.Id,
                    stepOrder = p.StepOrder,
                    decisionDate = p.DecisionDate,
                    observations = p.Observations,
                    approverUser = p.ApproverUser != null ? new Users
                    {
                        id = p.ApproverUser.Id,
                        name = p.ApproverUser.Name,
                        email = p.ApproverUser.Email,
                        role = p.ApproverUser.Role != null ? new GenericResponse
                        {
                            id = p.ApproverUser.Role.Id,
                            name = p.ApproverUser.Role.Name
                        } : null
                    } : null,
                    approverRole = p.ApproverRole != null ? new GenericResponse
                    {
                        id = p.ApproverRole.Id,
                        name = p.ApproverRole.Name,
                    } : null,
                    status = p.Status != null ? new GenericResponse
                    {
                        id = p.Status.Id,
                        name = p.Status.Name,
                    } : null,
                }).ToList();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Error al obtener pasos pendientes: {ex.Message}" });
            }
        }
    }
}
