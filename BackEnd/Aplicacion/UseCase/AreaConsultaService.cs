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
    public class AreaConsultaService : IAreaConsultaService
    {
        private readonly IAreaQuerys _areaQ;

        public AreaConsultaService(IAreaQuerys areaQ) 
        { 
            _areaQ = areaQ;
        }

        public async Task<List<Area>> ObternerArea()
        {
            return await _areaQ.ObtenerTodasAsync();

        }

        public async Task<List<GenericResponse>> ObtenerAreasApi()
        {
            var result = await ObternerArea();
            return result.Select(a => new GenericResponse { id = a.Id, name = a.Name }).ToList();
        }
    }
}
