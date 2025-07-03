using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface IAprobacionDecisionService
    {
        Task<bool> AprobarPaso(long pasoId, int usuarioId, string observacion);
        Task<bool> RechazarPaso(long pasoId, int usuarioId, string observacion);
        Task<bool> ObservarPaso(long pasoId, int usuarioId, string observacion);
    }
}
