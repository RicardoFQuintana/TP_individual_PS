using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.ICommands;
using _4_Dominio;

namespace _2_Infraestructura.Commands
{
    public class UsuarioCommands : IUsuarioCommands
    {
        private readonly ProyectosContext _context;

        public UsuarioCommands(ProyectosContext context)
        {
            _context = context;
        }

        public async Task<User> CrearUsuarioAsync(User nuevoUsuario)
        {
            _context.Users.Add(nuevoUsuario);
            await _context.SaveChangesAsync();
            return nuevoUsuario;
        }

        public async Task ActualizarUsuarioAsync(User usuario)
        {
            _context.Users.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarUsuarioAsync(int usuarioId)
        {
            var user = await _context.Users.FindAsync(usuarioId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
