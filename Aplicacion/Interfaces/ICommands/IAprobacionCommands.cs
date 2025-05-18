using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Aplicacion.Interfaces.ICommands
{
    public interface IAprobacionCommands
    {
        Task AprobarPasoAsync(int pasoId, int usuarioId);
        Task RechazarPasoAsync(int pasoId, int usuarioId);
        Task ObservarPasoAsync(int pasoId, int usuarioId);
    }
}
