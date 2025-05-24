using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.IQuerys
{
    public interface IProyectoQuerys
    {
        Task<List<ProjectProposal>> ObtenerTodosPropuestasAsync();
        Task<ProjectProposal?> ObtenerPropuestaPorIdAsync(Guid propuestaId);
        Task<List<ProjectProposal>> ObtenerPropuestasDeUsuarioAsync(int usuarioId);
        Task<List<ProjectApprovalStep>> ObtenerPasosDePropuestasAsync(List<ProjectProposal> propuestas);
        Task<List<ProjectApprovalStep>> ObtenerPasosPorPropuestaAsync(Guid propuestaId);
        Task<bool> ExisteTituloAsync(string Title);
    }
}
