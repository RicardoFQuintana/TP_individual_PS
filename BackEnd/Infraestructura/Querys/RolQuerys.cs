using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.IQuerys;
using _4_Dominio;
using Microsoft.EntityFrameworkCore;

namespace _2_Infraestructura.Querys
{
    public class RolQuerys : IRolQuerys
    {
        private readonly ProyectosContext _context;

        public RolQuerys(ProyectosContext context)
        {
            _context = context;
        }

        public async Task<List<ApproverRole>> ObtenerRolesAsync()
        {
            return await _context.ApproverRoles.ToListAsync();
        }
    }
}
