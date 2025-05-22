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

        public async Task<bool> AprobarPasoAsync(long pasoId, int usuarioId)
        {
            var paso = await _context.ProjectApprovalSteps.FindAsync(pasoId);
            if (paso == null || paso.ApproverUser_ID != null)
                return false;

            bool yaAprobo = await _context.ProjectApprovalSteps
                .AnyAsync(p =>
                    p.ProjectProposal_ID == paso.ProjectProposal_ID &&
                    p.ApproverUser_ID == usuarioId);

            if (yaAprobo)
                return false;

            paso.Status_ID = 2; // Aprobado
            paso.ApproverUser_ID = usuarioId;
            paso.DecisionDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RechazarPasoAsync(long pasoId, int usuarioId)
        {
            var paso = await _context.ProjectApprovalSteps.FindAsync(pasoId);
            if (paso == null || paso.ApproverUser_ID != null)
                return false;

            bool yaAprobo = await _context.ProjectApprovalSteps
                .AnyAsync(p =>
                    p.ProjectProposal_ID == paso.ProjectProposal_ID &&
                    p.ApproverUser_ID == usuarioId);

            if (yaAprobo)
                return false;

            paso.Status_ID = 3; // Rechazado
            paso.ApproverUser_ID = usuarioId;
            paso.DecisionDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ObservarPasoAsync(long pasoId, int usuarioId)
        {
            var paso = await _context.ProjectApprovalSteps.FindAsync(pasoId);
            if (paso == null || paso.ApproverUser_ID != null)
                return false;

            bool yaAprobo = await _context.ProjectApprovalSteps
                .AnyAsync(p =>
                    p.ProjectProposal_ID == paso.ProjectProposal_ID &&
                    p.ApproverUser_ID == usuarioId);

            if (yaAprobo)
                return false;

            paso.Status_ID = 4; // Observado
            paso.ApproverUser_ID = usuarioId;
            paso.DecisionDate = DateTime.Now;

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
