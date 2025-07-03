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

        public async Task<List<ProjectProposal>> ObtenerTodosPropuestasAsync()
        {
             var propuestas = await _context.ProjectProposals
                .Include(p => p.Area)
                .Include(p => p.Type)
                .Include(p => p.Status)
                .ToListAsync();
            return propuestas;

        }
        public async Task<ProjectProposal?> ObtenerPropuestaPorIdAsync(Guid propuestaId)
        {
            var propuesta = await _context.ProjectProposals
                             .Include(p => p.CreateBy)
                                .ThenInclude(u => u.Role)
                             .Include(p => p.Area)
                             .Include(p => p.Type)
                             .Include(p => p.Status)
                             .FirstOrDefaultAsync(p => p.Id == propuestaId);
            return propuesta;
        }
        public async Task<List<ProjectApprovalStep>> ObtenerPasosPorPropuestaAsync(Guid propuestaId)
        {
            return await _context.ProjectApprovalSteps
                .Where(p => p.ProjectProposalId == propuestaId)
                .Include(p => p.Status)
                .Include(p => p.ApproverUser)
                .Include(p => p.ApproverRole)
                .OrderBy(p => p.StepOrder)
                .ToListAsync();
        }
        public async Task<List<ProjectProposal>> ObtenerPropuestasDeUsuarioAsync(int userId)
        {
            return await _context.ProjectProposals
                         .Where(p => p.CreateById == userId)
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
                .Where(pas => propuestas.Select(p => p.Id).Contains(pas.ProjectProposalId))
                .OrderBy(pas => pas.StepOrder)
                .ToListAsync();
        }
        public async Task<bool> ExisteTituloAsync(string Title)
        {
            return await _context.ProjectProposals
                .AnyAsync(p => p.Title.ToLower() == Title.ToLower());
        }
        public async Task<ProjectApprovalStep?> ObtenerPasoPorIdAsync(int pasoId)
        {
            return await _context.ProjectApprovalSteps
                .Include(p => p.Status)
                .Include(p => p.ApproverUser)
                .Include(p => p.ApproverRole)
                .FirstOrDefaultAsync(p => p.Id == pasoId);
        }
    }
}
