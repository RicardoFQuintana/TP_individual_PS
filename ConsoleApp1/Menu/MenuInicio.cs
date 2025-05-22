using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Interfaces.IServices;
using _3_Aplicacion.UseCase;
using _4_Dominio;

namespace _1_ConsoleApp.Menu
{
    public class MenuInicio
    {
        private readonly IServicioUsuario _servicioUsuarios;

        public MenuInicio(IServicioUsuario servicioUsuarios)
        {
            _servicioUsuarios = servicioUsuarios;
        }

        public async Task<User> Mostrar()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("===== Bienvenido =====");
                    Console.WriteLine("1 - Iniciar sesión");
                    Console.WriteLine("2 - Registrarse");
                    Console.WriteLine("3 - Salir");
                    Console.Write("Opción: ");
                    var opcion = Console.ReadLine();

                    User? resultado = null;

                    switch (opcion)
                    {
                        case "1":
                            resultado = await Login();
                            break;
                        case "2":
                            resultado = await Registrar();
                            break;
                        case "3":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ingresaste un número erróneo, vuelva a intentarlo.\n\n");
                            break;
                    }

                    if (resultado != null)
                        return resultado;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error inesperado en menú inicio: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("Presiona una tecla para continuar...");
                Console.ReadKey();
                throw;
            }
        }
        private async Task<User?> Login()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Iniciar Sesión =====");

                var usuarios = await _servicioUsuarios.MostrarUsuarios();

                if (usuarios.Count == 0)
                {
                    Console.WriteLine("No hay usuarios registrados. Presione una tecla para continuar...");
                    Console.ReadKey();
                    return null;
                }

                Console.WriteLine("===== Lista de usuarios disponibles =====");

                foreach (var user in usuarios)
                {
                    Console.WriteLine($"• {user.id}: {user.Name}. ({user.Email})");
                }

                Console.WriteLine("\nIngrese el ID del usuario que desea.");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine() ?? "";

                if (!int.TryParse(opcion, out int idSeleccionado))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ID inválido. Presione una tecla para continuar...");
                    Console.ReadKey();
                    return null;
                }

                var usuario = await _servicioUsuarios.Login(idSeleccionado);
                if (usuario == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Usuario no encontrado. Presione una tecla para volver...");
                    Console.ReadKey();
                    return null;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nBienvenido {usuario.Name}!\n");
                await Task.Delay(1000); // reemplazo de Thread.Sleep
                return usuario;
            }
            catch (Exception ex)
            {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error en Login: {ex.Message}");
                    Console.ResetColor();
                    return null;
            }
        }
        private async Task<User?> Registrar()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("===== Registro de Usuario =====");

                string nombre;
                do
                {
                    Console.Write("Título: ");
                    nombre = Console.ReadLine()?.Trim() ?? "";
                } while (string.IsNullOrWhiteSpace(nombre));

                string email;
                do
                {
                    Console.Write("Título: ");
                    email = Console.ReadLine()?.Trim() ?? "";
                } while (string.IsNullOrWhiteSpace(email));

                var roles = await _servicioUsuarios.ObtenerRolesDisponibles();

                Console.WriteLine("\n===== Roles disponibles =====");
                foreach (var rol in roles)
                {
                    Console.WriteLine($"{rol.id}: {rol.Name}");
                }

                int roleId;
                while (true)
                {
                    Console.Write("\nIngrese el ID de rol deseado: ");
                    var input = Console.ReadLine();

                    if (int.TryParse(input, out roleId) && roles.Any(r => r.id == roleId))
                    {
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ID de rol inválido. Intente nuevamente.");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var creado = await _servicioUsuarios.RegistrarUsuario(nombre, email, roleId);
                Console.WriteLine("Usuario creado exitosamente. Presione una tecla para continuar...");
                Console.ReadKey();

                return creado;
            }
            catch (Exception ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en Registrar: {ex.Message}");
                Console.ResetColor();
                return null;
            }
        }

    }
}
