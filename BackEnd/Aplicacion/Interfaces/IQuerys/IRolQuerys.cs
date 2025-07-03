using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.IQuerys
{
    public interface IRolQuerys
    {
        Task<List<ApproverRole>> ObtenerRolesAsync();
    }
}
