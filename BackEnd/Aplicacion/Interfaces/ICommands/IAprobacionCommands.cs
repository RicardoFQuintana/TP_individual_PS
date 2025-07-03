using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.ICommands
{
    public interface IAprobacionCommands
    {
        Task<bool> AprobarPasoAsync(long pasoId, int usuarioId, string observacion);
        Task<bool> RechazarPasoAsync(long pasoId, int usuarioId, string observacion);
        Task<bool> ObservarPasoAsync(long pasoId, int usuarioId, string observacion);
        Task GuardarPasosAsync(List<ProjectApprovalStep> pasos);
    }
}
