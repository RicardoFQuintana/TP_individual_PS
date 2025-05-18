using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.IQuerys;
using _4_Dominio;
using Microsoft.EntityFrameworkCore;

namespace _2_Infraestructura.Querys
{
    public class UsuarioQuerys : IUsuarioQuerys
    {
        private readonly ProyectosContext _context;

        public UsuarioQuerys(ProyectosContext context)
        {
            _context = context;
        }
        
        public async Task<User?> ObtenerUsuarioPorIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
            

        public async Task<List<User>> ObtenerUsuariosPorRolAsync(string rol)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Role.Name == rol)
                .ToListAsync();
        }

        public async Task<List<User>> ObtenerTodosLosUsuariosAsync()
        {
            return await _context.Users.ToListAsync();
        }
           
    }
}
