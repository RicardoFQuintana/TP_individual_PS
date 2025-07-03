using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Response;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class ApiProjectGetService : IApiProjectGetService
    {
        private readonly IProyectoConsultaService _consulta;
        private readonly IProyectoCreacionService _creacion;

        public ApiProjectGetService(IProyectoConsultaService consulta, IProyectoCreacionService creacion)
        {
            _consulta = consulta;
            _creacion = creacion;
        }

        public async Task<Project?> ObtenerCompleto(Guid id)
        {
            return await _creacion.PropuestaCompleta(id);
        }

        public async Task<ProjectProposal?> Obtener(Guid id)
        {
            return await _consulta.ObtenerPropuestaPorId(id);
        }
    }
}
