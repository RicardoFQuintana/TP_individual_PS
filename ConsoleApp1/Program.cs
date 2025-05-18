using System;
using Microsoft.EntityFrameworkCore;
using _2_Infraestructura;

using _3_Aplicacion;
using _4_Dominio;

using var context = new ProyectosContext(new DbContextOptions<ProyectosContext>());

Servicio_Usuarios usuario = new Servicio_Usuarios(context);
Servicio_Proyectos proyecto = new Servicio_Proyectos(context);
Servicio_Aprobacion_Proyectos  aprobacion = new Servicio_Aprobacion_Proyectos(context);

User? usuarioActivo = null;

while (true)
{
    Console.Clear();
    if (usuarioActivo == null)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("===== Inicio de Sesión =====");
        usuarioActivo = usuario.Login();

        if (usuarioActivo == null)
        {
            Console.WriteLine("Presione una tecla para volver a intentar...");
            Console.ReadKey();
            continue;
        }
    }

    Console.Clear();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine($"Usuario activo: {usuarioActivo.Name}");
    Console.WriteLine("________________________________________________");
    Console.WriteLine("Ingrese el N° de la opción que desea usar:");
    Console.WriteLine("________________________________________________");
    Console.WriteLine("1 - Crear propuesta de proyecto");
    Console.WriteLine("2 - Ver mis propuestas y su flujo de aprobación");
    Console.WriteLine("3 - Aprobar/Rechazar proyectos pendientes");
    Console.WriteLine("4 - Cambiar de usuario");
    Console.WriteLine("5 - Salir");
    Console.WriteLine("________________________________________________");
    Console.Write("N°: ");
    string opcion = Console.ReadLine();
    Console.WriteLine("________________________________________________");
    Console.Clear();

    switch (opcion)
    {
        case "1":
            proyecto.CrearPropuesta(usuarioActivo);
            break;
        case "2":
            proyecto.VerMisPropuestas(usuarioActivo);
            break;
        case "3":
            aprobacion.AprobarPropuestasPendientes(usuarioActivo);
            break;
        case "4":
            usuarioActivo = null;
            break;
        case "5":
            Console.WriteLine("Gracias por usar el programa.");
            Console.ReadKey();
            return;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ingresaste un número erróneo, vuelva a intentarlo.\n\n");
            break;
    }

    Console.ForegroundColor = ConsoleColor.White;
    Console.Write("Presiona Enter para continuar . . . ");
    Console.ReadKey(true);
}