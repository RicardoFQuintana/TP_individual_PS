using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2_Infraestructura;
using _3_Aplicacion.Interfaces.IQuerys;
using _4_Dominio;
using Microsoft.EntityFrameworkCore;

namespace _2_Infraestructura.Querys
{
    public class AreaQuerys : IAreaQuerys
    {
        private readonly ProyectosContext _context;

        public AreaQuerys(ProyectosContext context)
        {
            _context = context;
        }

        public async Task<List<Area>> ObtenerTodasAsync()
        {
            return await _context.Areas.ToListAsync();
        }
    }
}
