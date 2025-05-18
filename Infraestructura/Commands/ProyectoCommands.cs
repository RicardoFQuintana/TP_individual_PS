using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.ICommands;
using _4_Dominio;

namespace _2_Infraestructura.Commands
{
    public class ProyectoCommands : IProyectoCommands
    {
        private readonly ProyectosContext _context;

        public ProyectoCommands(ProyectosContext context)
        {
            _context = context;
        }

        public async Task<ProjectProposal> CrearPropuestaAsync(ProjectProposal propuesta)
        {
            _context.ProjectProposals.Add(propuesta);
            await _context.SaveChangesAsync();
            return propuesta;
        }

        public async Task ActualizarPropuestaAsync(ProjectProposal propuesta)
        {
            _context.ProjectProposals.Update(propuesta);
            await _context.SaveChangesAsync();
        }

        public async Task CambiarEstadoPropuestaAsync(Guid propuestaId, int nuevoEstadoId)
        {
            var propuesta = await _context.ProjectProposals.FindAsync(propuestaId);
            if (propuesta != null)
            {
                propuesta.Status_ID = nuevoEstadoId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task EliminarPropuestaAsync(Guid propuestaId)
        {
            var propuesta = await _context.ProjectProposals.FindAsync(propuestaId);
            if (propuesta != null)
            {
                _context.ProjectProposals.Remove(propuesta);
                await _context.SaveChangesAsync();
            }
        }

    }
}
