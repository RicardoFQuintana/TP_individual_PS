using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Response;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface IApiProjectGetService
    {
        Task<ProjectProposal?> Obtener(Guid id);
        Task<Project?> ObtenerCompleto(Guid id);
    }
}
