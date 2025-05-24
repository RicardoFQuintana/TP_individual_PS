using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _4_Dominio;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _3_Aplicacion.Dto;

namespace _3_Aplicacion.UseCase
{
    public class ServicioProyectos : IServicioProyectos
    {
        private readonly IProyectoQuerys _proyectoQ;
        private readonly IProyectoCommands _proyectoC;
        private readonly IAprobacionCommands _aprobacionC;
        private readonly IAprobacionQuerys _aprobacionQ;
        private readonly IFlujoAprobacionGenerator _flujoGenerator;
        private readonly IAreaQuerys _areaQ;
        private readonly ITypeQuerys _typeQ;

        public ServicioProyectos(IProyectoQuerys proyectoQ, IProyectoCommands proyectoC, IFlujoAprobacionGenerator flujoGenerator,
                                    IAprobacionCommands aprobacionC, IAprobacionQuerys aprobacionQ, IAreaQuerys areaQ, ITypeQuerys typeQ)
        {
            _proyectoQ = proyectoQ;
            _proyectoC = proyectoC;
            _flujoGenerator = flujoGenerator;
            _aprobacionC = aprobacionC;
            _aprobacionQ = aprobacionQ;
            _typeQ = typeQ;
            _areaQ = areaQ;
        }

        public async Task<List<ProjectProposal>> MisPropuestas(User usuario)
        {
            return await _proyectoQ.ObtenerPropuestasDeUsuarioAsync(usuario.id);
        }
        public async Task<List<ProjectApprovalStep>> MisPasos(List<ProjectProposal> propuestas)
        {
            return await _proyectoQ.ObtenerPasosDePropuestasAsync(propuestas);
        }
        
        public async Task CrearPropuesta(CrearPropuestaDto dto)
        {
            var propuesta = new ProjectProposal
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Area_ID = dto.Area_ID,
                Type_ID = dto.Type_ID,
                EstimatedAmount = dto.EstimatedAmount,
                EstimatedDuration = dto.EstimatedDuration,
                CreateAt = DateTime.Now,
                Status_ID = 1,// Pendiente
                CreateBy_ID = dto.CreateBy_ID,
            };

            await _proyectoC.CrearPropuestaAsync(propuesta);

            var pasos = await _flujoGenerator.GenerarFlujoAsync(propuesta);

            await _aprobacionC.GuardarPasosAsync(pasos);
        }
        public async Task<List<Area>> ObternerArea()
        {
            return await _areaQ.ObtenerTodasAsync();

        }
        public async Task<List<ProjectType>> ObternerTipos()
        {
            return await _typeQ.ObtenerTodosAsync();
        }
        public async Task EvaluarEstadoPropuesta(Guid propuestaId)
        {
            var pasos = await _proyectoQ.ObtenerPasosPorPropuestaAsync(propuestaId);

            if (pasos.All(p => p.Status_ID == 2)) // Todos aprobados
            {
                await _proyectoC.CambiarEstadoPropuestaAsync(propuestaId, 2);
            }
            else if (pasos.Any(p => p.Status_ID == 3)) // Alguno rechazado
            {
                await _proyectoC.CambiarEstadoPropuestaAsync(propuestaId, 3);
            }
            else if (pasos.Any(p => p.Status_ID == 4)) // Alguno observado
            {
                await _proyectoC.CambiarEstadoPropuestaAsync(propuestaId, 4);
            }
        }
        public async Task<List<ProjectApprovalStep>> ObtenerPasosDePropuesta(Guid id)
        {
            return await _proyectoQ.ObtenerPasosPorPropuestaAsync(id);
        }
        public async Task<ProjectProposal> ObtenerPropuestaPorId(Guid id)
        {
            var propuesta = await _proyectoQ.ObtenerPropuestaPorIdAsync(id);
            if (propuesta == null)
            {
                return null;
            }
            return propuesta;
        }
        public async Task<bool> ExisteTitulo(string Title)
        {
            return await _proyectoQ.ExisteTituloAsync(Title);
        }
        public async Task ActualizarPropuesta(ProjectProposal propuesta)
        {
            await _proyectoC.ActualizarPropuestaAsync(propuesta);
        }
        public async Task<List<ProjectProposal>> ListarProyectosFiltrados(string? title, int? statusId, int? createdByUserId, int? approverUserId)
        {
            var query = await _proyectoQ.ObtenerTodosPropuestasAsync();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(p => p.Title.ToLower().Contains(title.ToLower())).ToList();

            if (statusId.HasValue)
                query = query.Where(p => p.Status_ID == statusId.Value).ToList();

            if (createdByUserId.HasValue)
                query = query.Where(p => p.CreateBy_ID == createdByUserId.Value).ToList();

            if (approverUserId.HasValue)
            {
                // Obtener solo los IDs de las propuestas que tienen pasos aprobados por ese usuario
                var pasos = await _aprobacionQ.ObtenerPasosPorUsuarioAsync(approverUserId.Value);
                var idsFiltrados = pasos.Select(p => p.ProjectProposal_ID).Distinct().ToList();

                query = query.Where(p => idsFiltrados.Contains(p.Id)).ToList();
            }
            return query;
        }
    }
}
