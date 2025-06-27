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
    public class AprobacionQuerys : IAprobacionQuerys
    {
        private readonly ProyectosContext _context;

        public AprobacionQuerys(ProyectosContext context) 
        {
            _context = context;
        }

        public async Task<List<ProjectApprovalStep>> ObtenerPasosPorPropuestaAsync(Guid propuestaId)
        {
            return await _context.ProjectApprovalSteps
                         .Where(p => p.ProjectProposalId == propuestaId)
                         .Include(p => p.ApproverUser)
                         .Include(p => p.ApproverRole)
                         .Include(p => p.Status)
                         .OrderBy(p => p.StepOrder)
                         .ToListAsync();
        }

        public async Task<List<ProjectApprovalStep>> ObtenerPasosPendientesPorUsuarioAsync(User usuario)
        {
            return await _context.ProjectApprovalSteps
                .Include(p => p.ProjectProposal)
                    .ThenInclude(p => p.Area)
                .Include(p => p.ProjectProposal)
                    .ThenInclude(p => p.Type)
                .Include(p => p.ApproverRole)
                .Include(p => p.Status)
                .Where(p =>
                    p.StatusId == 1 && // Pendiente
                    p.ProjectProposal.StatusId == 1 &&
                    (p.ApproverUserId == null || p.ApproverUserId == usuario.Id) &&
                    usuario.RoleId == p.ApproverRoleId)
                .ToListAsync();
        }

        public async Task<List<ApprovalRule>> ObtenerReglasAplicablesAsync(ProjectProposal propuesta)
        {
            return await _context.ApprovalRules
                .Where(r =>
                    (r.AreaId == null || r.AreaId == propuesta.AreaId) &&
                    (r.TypeId == null || r.TypeId == propuesta.TypeId) &&
                    propuesta.EstimatedAmount >= r.MinAmount &&
                    propuesta.EstimatedAmount <= r.MaxAmount)
                .ToListAsync();
        }
        public async Task<List<ProjectApprovalStep>> ObtenerPasosPorUsuarioAsync(int userId)
        {
            return await _context.ProjectApprovalSteps
                .Include(p => p.ProjectProposal)
                .Where(p => p.ApproverUserId == userId)
                .ToListAsync();
        }
    }
}
