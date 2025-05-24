using System;
using Microsoft.EntityFrameworkCore;
using _2_Infraestructura;
using _2_Infraestructura.Querys;
using _2_Infraestructura.Commands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.UseCase;
using _4_Dominio;
using _1_ConsoleApp.Menu;
using _3_Aplicacion.Interfaces.IServices;

using var context = new ProyectosContext(new DbContextOptions<ProyectosContext>());


IAprobacionQuerys aprobacionQ = new AprobacionQuerys(context);
IProyectoQuerys proyectoQ = new ProyectoQuerys(context);
IUsuarioQuerys usuarioQ = new UsuarioQuerys(context);
IRolQuerys rolQ = new RolQuerys(context);
IAreaQuerys areaQ = new AreaQuerys(context);
ITypeQuerys typeQ = new TypeQuerys(context);

IAprobacionCommands aprobacionC = new AprobacionCommands(context);
IProyectoCommands proyectoC = new ProyectoCommands(context);
IUsuarioCommands usuarioC = new UsuarioCommands(context);


IFlujoAprobacionGenerator flujo = new FlujoAprobacionGenerator(aprobacionQ);
IServicioUsuario usuario = new ServicioUsuarios(usuarioQ, usuarioC, rolQ);
IServicioProyectos proyecto = new ServicioProyectos(proyectoQ, proyectoC, flujo, aprobacionC, aprobacionQ, areaQ, typeQ);
IServicioAprobacionProyectos aprobacion = new ServicioAprobacionProyectos(aprobacionQ, aprobacionC);


var menuInicio = new MenuInicio(usuario);
var menuPrincipal = new MenuPrincipal(proyecto, aprobacion, usuario);

// Bucle general
while (true)
{
    try
    {
        var usuarioActivo = await menuInicio.Mostrar();
        await menuPrincipal.Mostrar(usuarioActivo);
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error inesperado global: {ex.Message}");
        Console.ResetColor();
        Console.WriteLine("Presiona una tecla para continuar...");
        Console.ReadKey();
    }
}