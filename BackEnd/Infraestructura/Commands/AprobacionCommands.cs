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

            if (paso == null || paso.StatusId != 1)
                return false;

            var primerPasoPendiente = await _context.ProjectApprovalSteps
                .Where(p => p.ProjectProposalId == paso.ProjectProposalId && p.StatusId == 1)
                .OrderBy(p => p.StepOrder)
                .FirstOrDefaultAsync();

            if (primerPasoPendiente == null || primerPasoPendiente.Id != paso.Id)
                return false;

            if (paso.ApproverUserId != null)
                return false;

            if (paso.ApproverRoleId == null)
                return false;

            // Verificamos si el usuario tiene el rol requerido
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (user == null || user.RoleId != paso.ApproverRoleId)
                return false;

            paso.StatusId = 2; // Aprobado
            paso.ApproverUserId = usuarioId;
            paso.DecisionDate = DateTime.UtcNow;
            paso.Observations = observacion;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RechazarPasoAsync(long pasoId, int usuarioId, string observacion)
        {
            var paso = await _context.ProjectApprovalSteps.FindAsync(pasoId);

            if (paso == null || paso.StatusId != 1)
                return false;

            var primerPasoPendiente = await _context.ProjectApprovalSteps
                .Where(p => p.ProjectProposalId == paso.ProjectProposalId && p.StatusId == 1)
                .OrderBy(p => p.StepOrder)
                .FirstOrDefaultAsync();

            if (primerPasoPendiente == null || primerPasoPendiente.Id != paso.Id)
                return false;

            if (paso.ApproverUserId != null)
                return false;

            if (paso.ApproverRoleId == null)
                return false;

            // Verificamos si el usuario tiene el rol requerido
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (user == null || user.RoleId != paso.ApproverRoleId)
                return false;

            paso.StatusId = 3; // Rechazado
            paso.ApproverUserId = usuarioId;
            paso.DecisionDate = DateTime.UtcNow;
            paso.Observations = observacion;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ObservarPasoAsync(long pasoId, int usuarioId, string observacion)
        {
            var paso = await _context.ProjectApprovalSteps.FindAsync(pasoId);

            if (paso == null || paso.StatusId != 1)
                return false;

            var primerPasoPendiente = await _context.ProjectApprovalSteps
                .Where(p => p.ProjectProposalId == paso.ProjectProposalId && p.StatusId == 1)
                .OrderBy(p => p.StepOrder)
                .FirstOrDefaultAsync();

            if (primerPasoPendiente == null || primerPasoPendiente.Id != paso.Id)
                return false;

            if (paso.ApproverUserId != null)
                return false;

            if (paso.ApproverRoleId == null)
                return false;

            // Verificamos si el usuario tiene el rol requerido
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            if (user == null || user.RoleId != paso.ApproverRoleId)
                return false;

            paso.StatusId = 4; // Observado
            paso.ApproverUserId = usuarioId;
            paso.DecisionDate = DateTime.UtcNow;
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
