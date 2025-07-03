using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class UsuarioRegistroService : IUsuarioRegistroService
    {
        private readonly IUsuarioCommands _usuarioC;

        public UsuarioRegistroService(IUsuarioQuerys usuarioQ, IUsuarioCommands usuarioC)
        {
            _usuarioC = usuarioC;
        }
        
        public async Task<User> RegistrarUsuario(string nombre, string email, int roleId)
        {
            var nuevo = new User
            {
                Name = nombre,
                Email = email,
                RoleId = roleId
            };

            await _usuarioC.CrearUsuarioAsync(nuevo);
            return nuevo;
        }
        
    }
}
