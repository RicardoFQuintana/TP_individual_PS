using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4_Dominio;
using _3_Aplicacion.Interfaces.IQuerys;
using Microsoft.EntityFrameworkCore;

namespace _2_Infraestructura.Querys
{
   public class StatusQuerys : IStatusQuerys
   {
        private readonly ProyectosContext _context;

        public StatusQuerys(ProyectosContext context)
        {
            _context = context;
        }

        public async Task<List<ApprovalStatus>> ObtenesTodosAsync()
        {
            return await _context.ApprovalStatuses.ToListAsync();
        }
    }
}
