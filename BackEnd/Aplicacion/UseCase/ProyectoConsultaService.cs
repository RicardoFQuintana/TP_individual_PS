using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Request;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class ProyectoConsultaService : IProyectoConsultaService
    {
        private readonly IProyectoQuerys _proyectoQ;
        private readonly IAprobacionQuerys _aprobacionQ;
        public ProyectoConsultaService(IProyectoQuerys proyectoQ, IAprobacionQuerys aprobacionQ)
        {
            _proyectoQ = proyectoQ;
            _aprobacionQ = aprobacionQ;
        }

        public async Task<List<ProjectProposal>> MisPropuestas(User usuario)
        {
            return await _proyectoQ.ObtenerPropuestasDeUsuarioAsync(usuario.Id);
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
        
        public async Task<List<ProjectProposal>> ListarProyectosFiltrados(string? title, int? statusId, int? createdByUserId, int? approverUserId, int? typeId, int? areaId)
        {
            var query = await _proyectoQ.ObtenerTodosPropuestasAsync();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(p => p.Title.ToLower().Contains(title.ToLower())).ToList();

            if (statusId.HasValue)
                query = query.Where(p => p.StatusId == statusId.Value).ToList();

            if (createdByUserId.HasValue)
                query = query.Where(p => p.CreateById == createdByUserId.Value).ToList();

            if (approverUserId.HasValue)
            {
                // Obtener solo los IDs de las propuestas que tienen pasos aprobados por ese usuario
                var pasos = await _aprobacionQ.ObtenerPasosPorUsuarioAsync(approverUserId.Value);
                var idsFiltrados = pasos.Select(p => p.ProjectProposalId).Distinct().ToList();

                query = query.Where(p => idsFiltrados.Contains(p.Id)).ToList();
            }

            if (typeId.HasValue)
                query = query.Where(p => p.TypeId == typeId.Value).ToList();

            if (areaId.HasValue)
                query = query.Where(p => p.AreaId == areaId.Value).ToList();

            return query;
        }
    }
}
