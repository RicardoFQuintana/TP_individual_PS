using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Response;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;

using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class TypeConsultaService : ITypeConsultaService
    {
        private readonly ITypeQuerys _typeQ;

        public TypeConsultaService(ITypeQuerys typeQ)
        {
            _typeQ = typeQ;
        }

        public async Task<List<ProjectType>> ObternerTipos()
        {
            return await _typeQ.ObtenerTodosAsync();
        }
        public async Task<List<GenericResponse>> ObtenerTiposApi()
        {
            var result = await ObternerTipos();
            return result.Select(t => new GenericResponse{ id = t.Id, name = t.Name }).ToList();
        }
    }
}
