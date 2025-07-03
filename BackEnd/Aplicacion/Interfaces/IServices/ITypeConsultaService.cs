using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Response;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface ITypeConsultaService
    {
        Task<List<ProjectType>> ObternerTipos();
        Task<List<GenericResponse>> ObtenerTiposApi();
    }
}
