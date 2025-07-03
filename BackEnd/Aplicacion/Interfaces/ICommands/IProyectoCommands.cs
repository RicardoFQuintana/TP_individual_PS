using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.ICommands
{
    public interface IProyectoCommands
    {
        Task<ProjectProposal> CrearPropuestaAsync(ProjectProposal propuesta);
        Task ActualizarPropuestaAsync(ProjectProposal propuesta);
        Task CambiarEstadoPropuestaAsync(Guid propuestaId, int nuevoEstadoId);
        Task EliminarPropuestaAsync(Guid propuestaId);
    }
}
