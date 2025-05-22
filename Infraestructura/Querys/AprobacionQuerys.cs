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
                         .Where(p => p.ProjectProposal_ID == propuestaId)
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
                    p.Status_ID == 1 && // Pendiente
                    (p.ApproverUser_ID == null || p.ApproverUser_ID == usuario.id) &&
                    usuario.Role_ID == p.ApproverRole_ID)
                .ToListAsync();
        }

        public async Task<List<ApprovalRule>> ObtenerReglasAplicablesAsync(ProjectProposal propuesta)
        {
            return await _context.ApprovalRules
                .Where(r =>
                    (r.Area_ID == null || r.Area_ID == propuesta.Area_ID) &&
                    (r.Type_ID == null || r.Type_ID == propuesta.Type_ID) &&
                    propuesta.EstimatedAmount >= r.MinAmount &&
                    propuesta.EstimatedAmount <= r.MaxAmount)
                .ToListAsync();
        }
    }
}
