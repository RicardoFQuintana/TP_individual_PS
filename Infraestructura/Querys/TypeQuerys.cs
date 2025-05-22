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
    public class TypeQuerys : ITypeQuerys
    {
        private readonly ProyectosContext _context;

        public TypeQuerys(ProyectosContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectType>> ObtenerTodosAsync()
        {
            return await _context.ProjectTypes.ToListAsync();
        }
    }
}
