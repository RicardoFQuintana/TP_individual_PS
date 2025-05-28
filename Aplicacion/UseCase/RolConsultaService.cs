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
    public class RolConsultaService : IRolConsultaService
    {
        private readonly IRolQuerys _rolQ;

        public RolConsultaService(IRolQuerys rolQ)
        {
            _rolQ = rolQ;
        }
       
        public async Task<List<ApproverRole>> ObtenerRolesDisponibles()
        {
            return await _rolQ.ObtenerRolesAsync();
        }
    }   
}
