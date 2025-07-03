using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto;
using _3_Aplicacion.Dto.Request;
using _3_Aplicacion.Interfaces.IServices;
using _3_Aplicacion.UseCase;
using _4_Dominio;

namespace _1_ConsoleApp.Menu
{
    public class MenuPrincipal
    {
        private readonly IProyectoCreacionService _projectCreateS;
        private readonly IProyectoConsultaService _projectCS;
        private readonly IProyectoFlujoService _projectFS;
        private readonly IProyectoPasoConsultaService _projectPCS;
        private readonly IAprobacionDecisionService _approvalDS;
        private readonly IAprobacionConsultaService _approvalCS;
        private readonly IAreaConsultaService _areaCS;
        private readonly ITypeConsultaService _typeCS;

        public MenuPrincipal(IProyectoCreacionService projectCreateS,  IProyectoConsultaService projectCS, IProyectoFlujoService projectFS, IProyectoPasoConsultaService projectPCS,
                                            IAprobacionDecisionService approvalDS, IAprobacionConsultaService approvalCS, IAreaConsultaService areaCS, ITypeConsultaService typeCS)
        {
            _approvalDS = approvalDS;
            _approvalCS = approvalCS;
            _projectCreateS = projectCreateS;
            _projectCS = projectCS;
            _projectFS = projectFS;
            _projectPCS = projectPCS;
            _areaCS = areaCS;
            _typeCS = typeCS;
        }

        public async Task Mostrar(User usuarioActivo)
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Usuario activo: {usuarioActivo.Name}");
                    Console.WriteLine("________________________________________________");
                    Console.WriteLine("1 - Crear propuesta de proyecto");
                    Console.WriteLine("2 - Ver mis propuestas y su flujo de aprobación");
                    Console.WriteLine("3 - Aprobar/Rechazar proyectos pendientes");
                    Console.WriteLine("4 - Cambiar de usuario");
                    Console.WriteLine("5 - Salir");
                    Console.WriteLine("________________________________________________");
                    Console.Write("N°: ");
                    string opcion = Console.ReadLine() ?? "";
                    Console.WriteLine("________________________________________________");

                    Console.Clear();

                    switch (opcion)
                    {
                        case "1":
                            await CrearPropuesta(usuarioActivo);
                            break;
                        case "2":
                            await VerMisPropuestas(usuarioActivo);
                            break;
                        case "3":
                            await AprobarPropuestasPendientes(usuarioActivo);
                            break;
                        case "4":
                            return; // volver a MenuInicio
                        case "5":
                            Console.WriteLine("Gracias por usar el programa.");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Ingresaste un número erróneo, vuelva a intentarlo.\n\n");
                            break;
                    }

                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error inesperado en menú principal: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine("Presiona una tecla para continuar...");
                Console.ReadKey();
            }
        }

        private async Task VerMisPropuestas(User usuario)
        {
            try
            {
                var propuestas = await _projectCS.MisPropuestas(usuario);

                if (propuestas.Count == 0)
                {
                    Console.WriteLine("No tenés propuestas registradas.");
                    return;
                }

                var approvalSteps = await _projectPCS.MisPasos(propuestas);

                if (approvalSteps.Count == 0)
                {
                    Console.WriteLine("No se encontraron pasos de aprobación.");
                    return;
                }

                foreach (var propuesta in propuestas)
                {

                    var stepsForProposal = approvalSteps
                        .Where(pas => pas.ProjectProposalId == propuesta.Id)
                        .ToList();

                    Console.WriteLine($"\n================= Proyecto {propuesta.Id} =================");
                    Console.WriteLine($"Título: {propuesta.Title}");
                    Console.WriteLine($"Descripción: {propuesta.Description}");
                    Console.WriteLine($"Área: {propuesta.Area?.Name ?? "No especificada"}");
                    Console.WriteLine($"Tipo: {propuesta.Type?.Name ?? "No especificado"}");
                    Console.WriteLine($"Monto estimado: ${propuesta.EstimatedAmount}");
                    Console.WriteLine($"Duración estimada: {propuesta.EstimatedDuration} días");
                    Console.WriteLine($"Estado general: {propuesta.Status?.Name ?? "No especificada"}");

                    Console.WriteLine("\nFlujo de aprobación:");

                    foreach (var paso in stepsForProposal)
                    {
                        string aprobador = paso.ApproverUser?.Name ?? $"Rol: {paso.ApproverRole?.Name}";
                        Console.WriteLine($"{paso.Status?.Name} - {aprobador}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en VerMisPropuestas: {ex.Message}");
                Console.ResetColor();
            }
        }
        private async Task CrearPropuesta(User usuario)
        {
            try 
            {
                Console.WriteLine("\n\n===== Nueva Propuesta =====\n\n");

                string titulo;
                do
                {
                    Console.Write("Título: ");
                    titulo = Console.ReadLine()?.Trim() ?? "";
                } while (string.IsNullOrWhiteSpace(titulo));

                string descripcion;
                do
                {
                    Console.Write("Descripción: ");
                    descripcion = Console.ReadLine()?.Trim() ?? "";
                } while (string.IsNullOrWhiteSpace(descripcion));

                var areas = await _areaCS.ObternerArea();

                if (areas.Count == 0)
                {
                    Console.WriteLine("No hay áreas cargadas. No se puede continuar.");
                    return;
                }

                Console.WriteLine("Seleccione un área:");
                for (int i = 0; i < areas.Count; i++)
                    Console.WriteLine($"{i + 1}. {areas[i].Name}");

                int areaId = ElegirOpcion(areas.Count);
                var areaSeleccionada = areas[areaId - 1];

                var tipos = await _typeCS.ObternerTipos();

                if (tipos.Count == 0)
                {
                    Console.WriteLine("No hay Tipos de Proyectos cargados. No se puede continuar.");
                    return;
                }

                Console.WriteLine("Seleccione un tipo de proyecto:");
                for (int i = 0; i < tipos.Count; i++)
                    Console.WriteLine($"{i + 1}. {tipos[i].Name}");

                int tipoId = ElegirOpcion(tipos.Count);
                var tipoSeleccionado = tipos[tipoId - 1];

                decimal monto;
                while (true)
                {
                    Console.Write("Monto estimado: ");
                    if (decimal.TryParse(Console.ReadLine(), out monto) && monto >= 0)
                        break;
                    Console.WriteLine("Monto inválido. Intente nuevamente.");
                }

                int duracion;
                while (true)
                {
                    Console.Write("Duración estimada (días): ");
                    if (int.TryParse(Console.ReadLine(), out duracion) && duracion > 0)
                        break;
                    Console.WriteLine("Duración inválida. Intente nuevamente.");
                }

                var Project = new ProjectCreate
                {
                    title = titulo,
                    description = descripcion,
                    amount = (double) monto,
                    duration = duracion,
                    area = areaSeleccionada.Id,
                    type = tipoSeleccionado.Id,
                    user = usuario.Id
                };

                try
                {
                    var P = await _projectCreateS.CrearPropuesta(Project);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n Propuesta creada exitosamente.");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n Error al crear la propuesta: {ex.Message}");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en CrearPropuesta: {ex.Message}");
                Console.ResetColor();
            }
        }
        private async Task AprobarPropuestasPendientes(User usuario)
        {
            try
            {

                var pasosPendientes = await _approvalCS.ObtenerPasosPendientesFiltrados(usuario);


                if (pasosPendientes.Count == 0)
                {
                    Console.WriteLine("No tenés pasos de aprobación pendientes.");
                    return;
                }

                Console.WriteLine("===== Pasos de aprobación asignados a tu rol =====");

                foreach (var paso in pasosPendientes)
                {
                    var propuesta = paso.ProjectProposal!;
                    Console.WriteLine($"\nProyecto: {propuesta.Title}");
                    Console.WriteLine($"Área: {propuesta.Area?.Name}");
                    Console.WriteLine($"Tipo: {propuesta.Type?.Name}");
                    Console.WriteLine($"Monto estimado: ${propuesta.EstimatedAmount}");
                    Console.WriteLine($"Paso #{paso.StepOrder} - Rol: {paso.ApproverRole?.Name}");

                    string opcion;
                    while (true)
                    {
                        Console.Write("¿Deseás aprobar (A), rechazar (R) este paso? ");
                        opcion = Console.ReadLine()?.Trim().ToUpper() ?? "";

                        if (opcion == "A" || opcion == "R")
                            break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción inválida. Ingresá A, R.");
                        Console.ResetColor();
                    }

                    Console.Write("Ingresá una observación (opcional, presioná Enter para omitir): ");
                    string observacion = Console.ReadLine()?.Trim() ?? "";

                    bool exito = false;

                    switch (opcion)
                    {
                        case "A":
                            exito = await _approvalDS.AprobarPaso(paso.Id, usuario.Id, observacion);
                            break;
                        case "R":
                            exito = await _approvalDS.RechazarPaso(paso.Id, usuario.Id, observacion);
                            break;
                        default:
                            Console.WriteLine("Opción inválida. Se omitió este paso.");
                            continue;
                    }
                    if (exito)
                    {
                        Console.WriteLine("\n Cambios guardados correctamente.");
                        await _projectFS.EvaluarEstadoPropuesta(paso.ProjectProposalId);
                    }
                    else
                    {
                        Console.WriteLine("\n Ya aprobaste un paso para esta propuesta. No podés aprobar más de uno.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en AprobarPropuestasPendientes: {ex.Message}");
                Console.ResetColor();
            }
        }
        private int ElegirOpcion(int max)
        {
            int opcion;
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Opción: ");
                if (int.TryParse(Console.ReadLine(), out opcion) && opcion >= 1 && opcion <= max)
                    return opcion;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entrada inválida. Intente de nuevo.");
                Console.ResetColor();

            }
        }

    }
}
