using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface IServicioUsuario
    {
        Task<User?> Login(int id);
        Task<List<User>> MostrarUsuarios();
        Task<User> RegistrarUsuario(string nombre, string email, int roleId);
        Task<List<ApproverRole>> ObtenerRolesDisponibles();
    }
}
