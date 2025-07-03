using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Response;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class UsuarioConsultaService : IUsuarioConsultaService
    {
        private readonly IUsuarioQuerys _usuarioQ;

        public UsuarioConsultaService(IUsuarioQuerys usuarioQ)
        {
            _usuarioQ = usuarioQ;
        }

        public async Task<List<User>> MostrarUsuarios()
        {
            return await _usuarioQ.ObtenerTodosLosUsuariosAsync();
        }

        public async Task<List<Users>> ObtenerUsuariosApi()
        {
            var result = await MostrarUsuarios();

            return result.Select(u => new Users
            {
                id = u.Id,
                name = u.Name,
                email = u.Email,
                role = new GenericResponse
                {
                    id = u.Role.Id,
                    name = u.Role.Name
                }
            }).ToList();
        }
    }
}
