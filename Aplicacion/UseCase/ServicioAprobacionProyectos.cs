using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _4_Dominio;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;

namespace _3_Aplicacion.UseCase
{
    public class ServicioAprobacionProyectos : IServicioAprobacionProyectos
    {
        private readonly IAprobacionQuerys _aprobacionQ;
        private readonly IAprobacionCommands _aprobacionC;

        public ServicioAprobacionProyectos(IAprobacionQuerys aprobacionQ, IAprobacionCommands aprobacionC)
        {
            _aprobacionQ = aprobacionQ;
            _aprobacionC = aprobacionC;
        }
        public async Task<List<ProjectApprovalStep>> ObtenerPasosPendientes(User usuario)
        {
            var pasos = await _aprobacionQ.ObtenerPasosPendientesPorUsuarioAsync(usuario);
            return pasos;
        }
        public List<ProjectApprovalStep> ObtenerPasosFiltrados(List<ProjectApprovalStep> Pasos)
        {
            var pasosFiltrados = Pasos
                .Where(p => p.ApproverUser_ID == null)
                .GroupBy(p => p.ProjectProposal_ID)
                .Select(g => g.OrderBy(p => p.StepOrder).First())
                .ToList();
            return pasosFiltrados;
        }
        public async Task<bool> AprobarPaso(long pasoId, int usuarioId)
        {
            return await _aprobacionC.AprobarPasoAsync(pasoId, usuarioId);
        }
        public async Task<bool> RechazarPaso(long pasoId, int usuarioId)
        {
            return await _aprobacionC.RechazarPasoAsync(pasoId, usuarioId);
        }
        public async Task<bool> ObservarPaso(long pasoId, int usuarioId)
        {
            return await _aprobacionC.ObservarPasoAsync(pasoId, usuarioId);
        }
    }
}
