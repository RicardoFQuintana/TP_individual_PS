using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Response;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class StatusConsultaServices : IStatusConsultaServices
    {
        private readonly IStatusQuerys _statusQ;

        public StatusConsultaServices(IStatusQuerys statusQ)
        {
            _statusQ = statusQ;
        }

        public async Task<List<ApprovalStatus>> ObternerStatus()
        {
            return await _statusQ.ObtenesTodosAsync();
        }

        public async Task<List<GenericResponse>> ObtenerStatusApi()
        {
            var result = await ObternerStatus();
            return result.Select(s => new GenericResponse { id = s.Id, name = s.Name }).ToList();
        }
    }
}
