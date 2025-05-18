using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.IQuerys;
using _4_Dominio;
using Microsoft.EntityFrameworkCore;

namespace _2_Infraestructura.Querys
{
    public class ProyectoQuerys : IProyectoQuerys
    {
        private readonly ProyectosContext _context;

        public ProyectoQuerys(ProyectosContext context)
        {
            _context = context;
        }

        public async Task<ProjectProposal?> ObtenerPropuestaPorIdAsync(Guid propuestaId)
        {
            var propuesta = await _context.ProjectProposals
                             .Include(p => p.Area)
                             .Include(p => p.Type)
                             .Include(p => p.Status)
                             .FirstOrDefaultAsync(p => p.Id == propuestaId);
            return propuesta;
        }

        public async Task<List<ProjectProposal>> ObtenerPropuestasDeUsuarioAsync(int userId)
        {
            return await _context.ProjectProposals
                         .Where(p => p.CreateBy_ID == userId)
                         .Include(p => p.Area)
                         .Include(p => p.Type)
                         .Include(p => p.Status)
                         .ToListAsync();
        }
    }
}
