using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface IServicioAprobacionProyectos
    {
        List<ProjectApprovalStep> ObtenerPasosFiltrados(List<ProjectApprovalStep> Pasos);
        Task<List<ProjectApprovalStep>> ObtenerPasosPendientes(User usuario);
        Task<bool> AprobarPaso(long pasoId, int usuarioId, string observacion);
        Task<bool> RechazarPaso(long pasoId, int usuarioId, string observacion);
        Task<bool> ObservarPaso(long pasoId, int usuarioId, string observacion);
    }
}
