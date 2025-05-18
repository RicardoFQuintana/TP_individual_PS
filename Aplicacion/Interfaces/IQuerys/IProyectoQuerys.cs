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
        Task<ProjectProposal?> ObtenerPropuestaPorIdAsync(Guid propuestaId);
        Task<List<ProjectProposal>> ObtenerPropuestasDeUsuarioAsync(int usuarioId);
    }
}
