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
    public class ProyectoFlujoService : IProyectoFlujoService
    {
        private readonly IProyectoQuerys _proyectoQ;
        private readonly IProyectoCommands _proyectoC;
        public ProyectoFlujoService(IProyectoQuerys proyectoQ, IProyectoCommands proyectoC)
        {
            _proyectoQ = proyectoQ;
            _proyectoC = proyectoC;
        }

        public async Task EvaluarEstadoPropuesta(Guid propuestaId)
        {
            var pasos = await _proyectoQ.ObtenerPasosPorPropuestaAsync(propuestaId);

            if (pasos.All(p => p.StatusId == 2)) // Todos aprobados
            {
                await _proyectoC.CambiarEstadoPropuestaAsync(propuestaId, 2);
            }
            else if (pasos.Any(p => p.StatusId == 3)) // Alguno rechazado
            {
                await _proyectoC.CambiarEstadoPropuestaAsync(propuestaId, 3);
            }
            else if (pasos.Any(p => p.StatusId == 4)) // Alguno observado
            {
                await _proyectoC.CambiarEstadoPropuestaAsync(propuestaId, 4);
            }
            else
            {
                // Hay pasos pendientes u otro estado, el proyecto debe quedar Pending
                await _proyectoC.CambiarEstadoPropuestaAsync(propuestaId, 1);
            }
        }
    }
}
