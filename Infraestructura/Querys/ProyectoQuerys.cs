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
        public async Task<List<ProjectApprovalStep>> ObtenerPasosPorPropuestaAsync(Guid propuestaId)
        {
            return await _context.ProjectApprovalSteps
                .Where(p => p.ProjectProposal_ID == propuestaId)
                .Include(p => p.Status)
                .OrderBy(p => p.StepOrder)
                .ToListAsync();
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

        public async Task<List<ProjectApprovalStep>> ObtenerPasosDePropuestasAsync(List<ProjectProposal> propuestas)
        {
            return await _context.ProjectApprovalSteps
                .Include(pas => pas.ApproverUser)
                .Include(pas => pas.ApproverRole)
                .Include(pas => pas.Status)
                .Where(pas => propuestas.Select(p => p.Id).Contains(pas.ProjectProposal_ID))
                .OrderBy(pas => pas.StepOrder)
                .ToListAsync();
        }
    }
}
