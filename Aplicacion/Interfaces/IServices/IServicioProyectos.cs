using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface IServicioProyectos
    {
        Task<List<ProjectProposal>> MisPropuestas(User usuario);
        Task<List<ProjectApprovalStep>> MisPasos(List<ProjectProposal> propuestas);
        Task CrearPropuesta(CrearPropuestaDto dto);
        Task<List<Area>> ObternerArea();
        Task<List<ProjectType>> ObternerTipos();
        Task EvaluarEstadoPropuesta(Guid propuestaId);
        Task<ProjectProposal> ObtenerPropuestaPorId(Guid id);
        Task<List<ProjectApprovalStep>> ObtenerPasosDePropuesta(Guid id);
        Task<bool> ExisteTitulo(string Title);
        Task ActualizarPropuesta(ProjectProposal propuesta);
        Task<List<ProjectProposal>> ListarProyectosFiltrados(string? title, int? statusId, int? createdByUserId, int? approverUserId);
    }
}
