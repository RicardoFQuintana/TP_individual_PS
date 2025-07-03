using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class FlujoAprobacionGenerator : IFlujoAprobacionGenerator
    {
        private readonly IAprobacionQuerys _aprobacionQuery;

        public FlujoAprobacionGenerator(IAprobacionQuerys aprobacionQuery)
        {
            _aprobacionQuery = aprobacionQuery;
        }

        public async Task<List<ProjectApprovalStep>> GenerarFlujoAsync(ProjectProposal propuesta)
        {
            var reglasAplicables = await _aprobacionQuery.ObtenerReglasAplicablesAsync(propuesta);

            var reglasPorOrden = reglasAplicables
                .GroupBy(r => r.StepOrder)
                .Select(g =>
                    g.OrderByDescending(r => (r.AreaId.HasValue ? 1 : 0) + (r.TypeId.HasValue ? 1 : 0)) // más específicos primero
                    .First()
                )
                .OrderBy(r => r.StepOrder)
                .ToList();

            var pasos = reglasPorOrden.Select(regla => new ProjectApprovalStep
            {
                ProjectProposalId = propuesta.Id,
                ApproverRoleId = regla.ApproverRoleId,
                StepOrder = regla.StepOrder,
                StatusId = 1 // Pendiente
            }).ToList();

            return pasos;
        }
    }
}
