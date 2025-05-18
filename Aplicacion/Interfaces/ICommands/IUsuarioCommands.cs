using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.ICommands
{
    public interface IUsuarioCommands
    {
        Task<User> CrearUsuarioAsync(User nuevoUsuario);
        Task ActualizarUsuarioAsync(User usuario);
        Task EliminarUsuarioAsync(int usuarioId);
    }
}
