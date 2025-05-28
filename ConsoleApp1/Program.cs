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
IUsuarioAutenticacionService userAS = new UsuarioAutenticacionService(usuarioQ);
IUsuarioRegistroService userRS = new UsuarioRegistroService(usuarioQ, usuarioC);
IUsuarioConsultaService userCS = new UsuarioConsultaService(usuarioQ);
IRolConsultaService rolCS = new RolConsultaService(rolQ);
IAreaConsultaService areaCS = new AreaConsultaService(areaQ);
ITypeConsultaService typeCS = new TypeConsultaService(typeQ);
IProyectoCreacionService projectCreateS = new ProyectoCreacionService(proyectoC, flujo, aprobacionC);
IProyectoConsultaService projectCS = new ProyectoConsultaService(proyectoQ, aprobacionQ);
IProyectoFlujoService projectFS = new ProyectoFlujoService(proyectoQ,proyectoC);
IProyectoPasoConsultaService projectPCS = new ProyectoPasoConsultaService(proyectoQ); 
IAprobacionDecisionService approvalDS = new AprobacionDecisionService(aprobacionC);
IAprobacionFiltradoService approvalFS = new AprobacionFiltradoService();
IAprobacionConsultaService approvalCS = new AprobacionConsultaService(aprobacionQ, approvalFS);




var menuInicio = new MenuInicio(userAS, userRS, userCS, rolCS);
var menuPrincipal = new MenuPrincipal(projectCreateS, projectCS, projectFS, projectPCS, approvalDS, approvalCS, areaCS, typeCS);

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