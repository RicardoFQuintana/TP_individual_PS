using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class AprobacionDecisionService : IAprobacionDecisionService
    {
        private readonly IAprobacionCommands _aprobacionC;

        public AprobacionDecisionService(IAprobacionCommands aprobacionC)
        {
            _aprobacionC = aprobacionC;
        }

        public async Task<bool> AprobarPaso(long pasoId, int usuarioId, string observacion)
        {
            return await _aprobacionC.AprobarPasoAsync(pasoId, usuarioId, observacion);
        }
        public async Task<bool> RechazarPaso(long pasoId, int usuarioId, string observacion)
        {
            return await _aprobacionC.RechazarPasoAsync(pasoId, usuarioId, observacion);
        }
        public async Task<bool> ObservarPaso(long pasoId, int usuarioId, string observacion)
        {
            return await _aprobacionC.ObservarPasoAsync(pasoId, usuarioId, observacion);
        }
    }
}
