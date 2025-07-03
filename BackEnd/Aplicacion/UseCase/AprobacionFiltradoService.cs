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
    public class AprobacionFiltradoService : IAprobacionFiltradoService
    {

        public AprobacionFiltradoService()
        {
        }

        public List<ProjectApprovalStep> ObtenerPasosFiltrados(List<ProjectApprovalStep> Pasos)
        {
            var pasosFiltrados = Pasos
                .Where(p => p.ApproverUserId == null)
                .GroupBy(p => p.ProjectProposalId)
                .Select(g => g.OrderBy(p => p.StepOrder).First())
                .ToList();
            return pasosFiltrados;
        }
    }
}
