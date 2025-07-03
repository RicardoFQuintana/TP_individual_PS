using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Response;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface IApiProjectListService
    {
        Task<List<ProjectShort>> Listar(string? title, int? status, int? applicant, int? approvalUser, int? typeId, int? areaId);
    }
}
