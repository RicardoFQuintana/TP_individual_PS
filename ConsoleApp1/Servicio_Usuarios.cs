using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD;

namespace ConsoleApp1
{
    public class Servicio_Usuarios
    {
        private readonly ProyectosContext context;

        public Servicio_Usuarios(ProyectosContext context)
        {
            this.context = context;
        }

        public User? Login()
        {
            var usuarios = context.Users.ToList();
            //int opcion;

            Console.WriteLine("\n===== Seleccione un usuario =====");
            for (int i = 0; i < usuarios.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {usuarios[i].Name}. ({usuarios[i].Email})");
            }
            
            Console.WriteLine("\nIngrese el numero del usuario que desea.");
            Console.Write("Opción: ");

            if (int.TryParse(Console.ReadLine(), out int seleccion) && seleccion >= 1 && seleccion <= usuarios.Count)
            {
                var usuario = usuarios[seleccion - 1];
                Console.WriteLine($"\nBienvenido {usuario.Name}!\n");
                System.Threading.Thread.Sleep(1000);
                return usuario;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Opción inválida.");
            return null;
        }

        public void MostrarUsuarios()
        {
            var usuarios = context.Users.ToList();

            Console.WriteLine("===== Lista de usuarios disponibles =====");
            foreach (var user in usuarios)
            {
                Console.WriteLine($"• {user.id}: {user.Name}. ({user.Email})");
            }
        }
    }
}
