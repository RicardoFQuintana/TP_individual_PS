using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _3_Aplicacion.UseCase
{
    public class ServicioUsuarios : IServicioUsuario
    {
        private readonly IUsuarioQuerys _usuarioQ;
        private readonly IUsuarioCommands _usuarioC;
        private readonly IRolQuerys _rolQ;

        public ServicioUsuarios(IUsuarioQuerys usuarioQ, IUsuarioCommands usuarioC, IRolQuerys rolQ)
        {
            _usuarioQ = usuarioQ;
            _usuarioC = usuarioC;
            _rolQ = rolQ;
        }
        public async Task<User?> Login(int id)
        { 
            var user = await _usuarioQ.ObtenerUsuarioPorIdAsync(id);
            return user;
        }
        public async Task<List<User>> MostrarUsuarios()
        {
            return await _usuarioQ.ObtenerTodosLosUsuariosAsync();
        }
        public async Task<User> RegistrarUsuario(string nombre, string email, int roleId)
        {
            var nuevo = new User
            {
                Name = nombre,
                Email = email,
                Role_ID = roleId
            };

            await _usuarioC.CrearUsuarioAsync(nuevo);
            return nuevo;
        }
        public async Task<List<ApproverRole>> ObtenerRolesDisponibles()
        {
            return await _rolQ.ObtenerRolesAsync();
        }
    }
}
