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
    public class UsuarioAutenticacionService : IUsuarioAutenticacionService
    {
        private readonly IUsuarioQuerys _usuarioQ;

        public UsuarioAutenticacionService(IUsuarioQuerys usuarioQ)
        {
            _usuarioQ = usuarioQ;
        }
        
        public async Task<User?> Login(int id)
        {
            var user = await _usuarioQ.ObtenerUsuarioPorIdAsync(id);
            return user;
        }
    }
}
