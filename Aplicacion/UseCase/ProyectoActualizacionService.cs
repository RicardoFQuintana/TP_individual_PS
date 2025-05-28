using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class ProyectoActualizacionService : IProyectoActualizacionService
    {
        private readonly IProyectoCommands _proyectoC;

        public ProyectoActualizacionService(IProyectoCommands proyectoC)
        {
            _proyectoC = proyectoC;
        }

        public async Task ActualizarPropuesta(ProjectProposal propuesta)
        {
            await _proyectoC.ActualizarPropuestaAsync(propuesta);
        }
    }
}
