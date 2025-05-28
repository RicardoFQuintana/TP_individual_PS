using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Request;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class ProyectoPasoConsultaService : IProyectoPasoConsultaService
    {
        private readonly IProyectoQuerys _proyectoQ;

        public ProyectoPasoConsultaService(IProyectoQuerys proyectoQ)
        {
            _proyectoQ = proyectoQ;
        }

        public async Task<List<ProjectApprovalStep>> MisPasos(List<ProjectProposal> propuestas)
        {
            return await _proyectoQ.ObtenerPasosDePropuestasAsync(propuestas);
        }

        public async Task<List<ProjectApprovalStep>> ObtenerPasosDePropuesta(Guid id)
        {
            return await _proyectoQ.ObtenerPasosPorPropuestaAsync(id);
        }
       
    }
}
