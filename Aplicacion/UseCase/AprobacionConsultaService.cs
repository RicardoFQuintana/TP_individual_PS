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
    public class AprobacionConsultaService : IAprobacionConsultaService
    {
        private readonly IAprobacionQuerys _aprobacionQ;
        private readonly IAprobacionFiltradoService _approvalFS;

        public AprobacionConsultaService(IAprobacionQuerys aprobacionQ, IAprobacionFiltradoService approvalFS)
        {
            _aprobacionQ = aprobacionQ;
            _approvalFS = approvalFS;
        }

        public async Task<List<ProjectApprovalStep>> ObtenerPasosPendientesFiltrados(User usuario)
        {
            var pasos = await _aprobacionQ.ObtenerPasosPendientesPorUsuarioAsync(usuario);

            var pasosFiltrados = _approvalFS.ObtenerPasosFiltrados(pasos);

            return pasosFiltrados;
        }
    }
}
