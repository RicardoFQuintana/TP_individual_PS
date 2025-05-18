using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.ICommands;

namespace _2_Infraestructura.Commands
{
    public class AprobacionCommands : IAprobacionCommands
    {
        private readonly ProyectosContext _context;

        public AprobacionCommands(ProyectosContext context)
        {
            _context = context;
        }

        public async Task AprobarPasoAsync(int pasoId, int usuarioId)
        {
            var paso = await _context.ProjectApprovalSteps.FindAsync(pasoId);
            if (paso != null)
            {
                paso.Status_ID = 2; // Aprobado
                paso.ApproverUser_ID = usuarioId;
                paso.DecisionDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RechazarPasoAsync(int pasoId, int usuarioId)
        {
            var paso = await _context.ProjectApprovalSteps.FindAsync(pasoId);
            if (paso != null)
            {
                paso.Status_ID = 3; // Rechazado
                paso.ApproverUser_ID = usuarioId;
                paso.DecisionDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ObservarPasoAsync(int pasoId, int usuarioId)
        {
            var paso = await _context.ProjectApprovalSteps.FindAsync(pasoId);
            if (paso != null)
            {
                paso.Status_ID = 4; // Observado
                paso.ApproverUser_ID = usuarioId;
                paso.DecisionDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
    }
}
