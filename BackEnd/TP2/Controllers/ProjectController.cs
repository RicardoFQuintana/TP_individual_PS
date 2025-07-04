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
        private readonly IApiProjectListService _listService;
        private readonly IApiProjectGetService _getService;
        private readonly IApiProjectCreateService _createService;
        private readonly IApiProjectUpdateService _updateService;
        private readonly IApiDecisionService _decisionService;

        public ProjectController(IApiProjectListService listService, IApiProjectGetService getService, IApiProjectCreateService createService,
                                                                    IApiProjectUpdateService updateService, IApiDecisionService decisionService)
        {
            _listService = listService;
            _getService = getService;
            _createService = createService;
            _updateService = updateService;
            _decisionService = decisionService;
        }

        //GET /api/Project?title=&statusId=&createdByUserId=&approverUserId=
        [HttpGet]
        public async Task<ActionResult<List<ProjectShort>>> GetProjects([FromQuery] string? title, [FromQuery] int? status, [FromQuery] int? applicant,
                                                                            [FromQuery] int? approvalUser, [FromQuery] int? typeId, [FromQuery] int? areaId)
        {
            try
            {
                if ((applicant.HasValue && (applicant <= 0 || applicant >= 7)) || (approvalUser.HasValue && (approvalUser <= 0 || approvalUser >= 7)) || (status.HasValue && (status <= 0 || status >= 5)) 
                                                                                                    || (areaId.HasValue && (areaId <= 0 || areaId >= 5)) || (typeId.HasValue && (typeId <= 0 || typeId >= 5)))
                    return BadRequest(new ApiError { message = "Parámetro de consulta inválido" });


                var result = await _listService.Listar(title, status, applicant, approvalUser, typeId, areaId);

                if (result.Count == 0)
                    return BadRequest(new ApiError { message = "no hay proyectos" });

                return Ok(result);

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
                                                project.area <= 0 || project.area >= 5 || project.type <= 0 || project.type >= 5 || project.user <= 0)
                    return BadRequest(new ApiError { message = "Datos del proyecto inválidos" });

                var id = await _createService.Crear(project);
                if (id == null)
                    return BadRequest(new ApiError { message = "Usuario no válido o título duplicado." });

                var proyecto = await _getService.ObtenerCompleto(id.Value);
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

                var proyecto = await _getService.Obtener(id);
                if (proyecto == null)
                    return NotFound(new ApiError { message = "Proyecto no encontrado" });

                if (proyecto.StatusId != 1)
                    return Conflict(new ApiError { message = "El proyecto ya no se encuentra en un estado que permite modificaciones" });

                var resultado = await _decisionService.TomarDecision(id, dto);

                if (!resultado)
                    return Conflict(new ApiError { message = "No se pudo aplicar la decisión. Verificá las reglas." });

                var proyectoCompleto = await _getService.ObtenerCompleto(id);
                return Ok(proyectoCompleto);

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

                if (string.IsNullOrWhiteSpace(update.title) || string.IsNullOrWhiteSpace(update.description) || update.duration <= 0)
                    return BadRequest(new ApiError { message = "Datos de actualización inválidos" });

                var proyecto = await _getService.Obtener(id);
                if (proyecto == null)
                    return NotFound(new ApiError { message = "Proyecto no encontrado" });

                if (proyecto.StatusId != 4)
                    return Conflict(new ApiError { message = "El proyecto ya no se encuentra en un estado que permite modificaciones" });

                var actualizado = await _updateService.Actualizar(id, update);
                if (!actualizado)
                    return Conflict(new ApiError { message = "Ya existe una propuesta con ese título." });

                var proyectoCompleto = await _getService.ObtenerCompleto(id);
                return Ok(proyectoCompleto);

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

                var proyecto = await _getService.Obtener(id);

                if (proyecto == null)
                    return NotFound(new ApiError { message = "Proyecto no encontrado" });

                var completo = await _getService.ObtenerCompleto(id);

                return Ok(completo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Ocurrió un error al obtener el proyecto: {ex.Message}" });
            }
        }

    }
}
