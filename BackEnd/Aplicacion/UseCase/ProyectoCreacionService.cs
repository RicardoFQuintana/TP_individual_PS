using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Request;
using _3_Aplicacion.Dto.Response;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class ProyectoCreacionService : IProyectoCreacionService
    {
        private readonly IProyectoCommands _proyectoC;
        private readonly IAprobacionCommands _aprobacionC;
        private readonly IFlujoAprobacionGenerator _flujoGenerator;
        private readonly IProyectoConsultaService _projectCS;
        private readonly IProyectoPasoConsultaService _projectPCS;

        public ProyectoCreacionService(IProyectoCommands proyectoC, IFlujoAprobacionGenerator flujoGenerator, IAprobacionCommands aprobacionC,IProyectoConsultaService projectCS, IProyectoPasoConsultaService projectPCS)
        {
            _proyectoC = proyectoC;
            _flujoGenerator = flujoGenerator;
            _aprobacionC = aprobacionC;
            _projectCS = projectCS;
            _projectPCS = projectPCS;
        }

        
        public async Task<ProjectProposal> CrearPropuesta(ProjectCreate dto)
        {
            var propuesta = new ProjectProposal
            {
                Id = Guid.NewGuid(),
                Title = dto.title,
                Description = dto.description,
                AreaId = dto.area,
                TypeId = dto.type,
                EstimatedAmount = (decimal) dto.amount,
                EstimatedDuration = dto.duration,
                CreateAt = DateTime.Now,
                StatusId = 1,// Pendiente
                CreateById = dto.user,
            };

            await _proyectoC.CrearPropuestaAsync(propuesta);

            var pasos = await _flujoGenerator.GenerarFlujoAsync(propuesta);

            await _aprobacionC.GuardarPasosAsync(pasos);

            return propuesta;

        }
        public async Task<Project> PropuestaCompleta(Guid id)
        {
            var proyecto = await _projectCS.ObtenerPropuestaPorId(id);
            var step = await _projectPCS.ObtenerPasosDePropuesta(id);

            return new Project
            {
                id = proyecto.Id,
                title = proyecto.Title,
                description = proyecto.Description,
                amount = (double)proyecto.EstimatedAmount,
                duration = proyecto.EstimatedDuration,
                area = proyecto.Area != null ? new GenericResponse
                {
                    id = proyecto.AreaId,
                    name = proyecto.Area.Name,
                } : null,
                status = proyecto.Status != null ? new GenericResponse
                {
                    id = proyecto.StatusId,
                    name = proyecto.Status.Name,
                } : null,
                type = proyecto.Type != null ? new GenericResponse
                {
                    id = proyecto.TypeId,
                    name = proyecto.Type.Name,
                } : null,
                user = proyecto.CreateBy != null ? new Users
                {
                    id = proyecto.CreateById,
                    name = proyecto.CreateBy.Name,
                    email = proyecto.CreateBy.Email,
                    role = proyecto.CreateBy.Role != null ? new GenericResponse
                    {
                        id = proyecto.CreateBy.Role.Id,
                        name = proyecto.CreateBy.Role.Name
                    } : null
                } : null,
                steps = step.Select(s => new ApprovalStep
                {
                    id = s.Id,
                    stepOrder = s.StepOrder,
                    decisionDate = s.DecisionDate,
                    observations = s.Observations,
                    approverUser = s.ApproverUser != null ? new Users
                    {
                        id = s.ApproverUser.Id,
                        name = s.ApproverUser.Name,
                        email = s.ApproverUser.Email,
                        role = s.ApproverUser.Role != null ? new GenericResponse
                        {
                            id = s.ApproverUser.Role.Id,
                            name = s.ApproverUser.Role.Name
                        } : null
                    } : null,
                    approverRole = s.ApproverRole != null ? new GenericResponse
                    {
                        id = s.ApproverRole.Id,
                        name = s.ApproverRole.Name,
                    } : null,
                    status = s.Status != null ? new GenericResponse
                    {
                        id = s.Status.Id,
                        name = s.Status.Name,
                    } : null,
                    projectId = s.ProjectProposalId
                }).ToList()
            };
        }

    }
}
