using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface IProyectoConsultaService
    {
        Task<List<ProjectProposal>> MisPropuestas(User usuario);
        Task<List<ProjectProposal>> ListarProyectosFiltrados(string? title, int? statusId, int? createdByUserId, int? approverUserId, int? typeId, int? areaId);
        Task<ProjectProposal> ObtenerPropuestaPorId(Guid id);
    }
}
