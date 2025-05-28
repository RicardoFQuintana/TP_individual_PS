using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.ICommands;
using _4_Dominio;
using Microsoft.EntityFrameworkCore;

namespace _2_Infraestructura.Commands
{
    public class AprobacionCommands : IAprobacionCommands
    {
        private readonly ProyectosContext _context;

        public AprobacionCommands(ProyectosContext context)
        {
            _context = context;
        }

        public async Task<bool> AprobarPasoAsync(long pasoId, int usuarioId, string observacion)
        {
            var paso = await _context.ProjectApprovalSteps.FindAsync(pasoId);
            if (paso == null || paso.ApproverUserId != null)
                return false;

            bool yaAprobo = await _context.ProjectApprovalSteps
                .AnyAsync(p =>
                    p.ProjectProposalId == paso.ProjectProposalId &&
                    p.ApproverUserId == usuarioId);

            if (yaAprobo)
                return false;

            paso.StatusId = 2; // Aprobado
            paso.ApproverUserId = usuarioId;
            paso.DecisionDate = DateTime.Now;
            paso.Observations = observacion;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RechazarPasoAsync(long pasoId, int usuarioId, string observacion)
        {
            var paso = await _context.ProjectApprovalSteps.FindAsync(pasoId);
            if (paso == null || paso.ApproverUserId != null)
                return false;

            bool yaAprobo = await _context.ProjectApprovalSteps
                .AnyAsync(p =>
                    p.ProjectProposalId == paso.ProjectProposalId &&
                    p.ApproverUserId == usuarioId);

            if (yaAprobo)
                return false;

            paso.StatusId = 3; // Rechazado
            paso.ApproverUserId = usuarioId;
            paso.DecisionDate = DateTime.Now;
            paso.Observations = observacion;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ObservarPasoAsync(long pasoId, int usuarioId, string observacion)
        {
            var paso = await _context.ProjectApprovalSteps.FindAsync(pasoId);
            if (paso == null || paso.ApproverUserId != null)
                return false;

            bool yaAprobo = await _context.ProjectApprovalSteps
                .AnyAsync(p =>
                    p.ProjectProposalId == paso.ProjectProposalId &&
                    p.ApproverUserId == usuarioId);

            if (yaAprobo)
                return false;

            paso.StatusId = 4; // Observado
            paso.ApproverUserId = usuarioId;
            paso.DecisionDate = DateTime.Now;
            paso.Observations = observacion;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task GuardarPasosAsync(List<ProjectApprovalStep> pasos)
        {
            await _context.ProjectApprovalSteps.AddRangeAsync(pasos);
            await _context.SaveChangesAsync();
        }
    }
}
