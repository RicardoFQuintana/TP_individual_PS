using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Response;
using _3_Aplicacion.Interfaces.IServices;

namespace _3_Aplicacion.UseCase
{
    public class ApiProjectListService : IApiProjectListService
    {
        private readonly IProyectoConsultaService _consulta;

        public ApiProjectListService(IProyectoConsultaService consulta)
        {
            _consulta = consulta;
        }

        public async Task<List<ProjectShort>> Listar(string? title, int? status, int? applicant, int? approvalUser, int? typeId, int? areaId)
        {
            var proyectos = await _consulta.ListarProyectosFiltrados(title, status, applicant, approvalUser, typeId, areaId);

            return proyectos.Select(p => new ProjectShort
            {
                Id = p.Id,
                title = p.Title,
                description = p.Description,
                amount = (double)p.EstimatedAmount,
                duration = p.EstimatedDuration,
                area = p.Area?.Name ?? "No especificada",
                status = p.Status?.Name ?? "No especificado",
                type = p.Type?.Name ?? "No especificado"
            }).ToList();
        }
    }
}
