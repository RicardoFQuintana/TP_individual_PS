using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _4_Dominio;

namespace _3_Aplicacion
{
    public class Servicio_Proyectos
    {
        private readonly ProyectosContext context;

        public Servicio_Proyectos(ProyectosContext context)
        {
            this.context = context;
        }

        public void CrearPropuesta(User usuario)
        {
            Console.WriteLine("\n\n===== Nueva Propuesta =====\n\n");
            Console.Write("Título: ");
            string titulo = Console.ReadLine();
            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine();

            var areas = context.Areas.ToList();
            Console.WriteLine("Seleccione un área:");
            for (int i = 0; i < areas.Count; i++)
                Console.WriteLine($"{i + 1}. {areas[i].Name}");

            int areaId = ElegirOpcion(areas.Count);
            var areaSeleccionada = areas[areaId - 1];

            var tipos = context.ProjectTypes.ToList();
            Console.WriteLine("Seleccione un tipo de proyecto:");
            for (int i = 0; i < tipos.Count; i++)
                Console.WriteLine($"{i + 1}. {tipos[i].Name}");

            int tipoId = ElegirOpcion(tipos.Count);
            var tipoSeleccionado = tipos[tipoId - 1];

            Console.Write("Monto estimado: ");
            decimal monto = decimal.Parse(Console.ReadLine());

            Console.Write("Duración estimada (días): ");
            int duracion = int.Parse(Console.ReadLine());

            var propuesta = new ProjectProposal
            {
                Id = Guid.NewGuid(),
                Title = titulo,
                Description = descripcion,
                Area_ID = areaSeleccionada.id,
                Type_ID = tipoSeleccionado.id,
                EstimatedAmount = monto,
                EstimatedDuration = duracion,
                CreateAt = DateTime.Now,
                Status_ID = 1, // Pendiente
                CreateBy_ID = usuario.id
            };

            context.ProjectProposals.Add(propuesta);
            context.SaveChanges();

            GenerarFlujoAprobacion(propuesta);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Propuesta creada con éxito.");
        }

        private void GenerarFlujoAprobacion(ProjectProposal propuesta)
        {
            var reglasAplicables = context.ApprovalRules
                .Where(r =>
                    (r.Area_ID == null || r.Area_ID == propuesta.Area_ID) &&
                    (r.Type_ID == null || r.Type_ID == propuesta.Type_ID) &&
                    propuesta.EstimatedAmount >= r.MinAmount &&
                    propuesta.EstimatedAmount <= r.MaxAmount)
                .ToList();

            // Agrupar por StepOrder
            var reglasPorOrden = reglasAplicables
                .GroupBy(r => r.StepOrder)
                .Select(g =>
                    g.OrderByDescending(r => (r.Area_ID.HasValue ? 1 : 0) + (r.Type_ID.HasValue ? 1 : 0)) // más específicos primero
                     .First()
                )
                .OrderBy(r => r.StepOrder)
                .ToList();

            foreach (var regla in reglasPorOrden)
            {
                var paso = new ProjectApprovalStep
                {
                    ProjectProposal_ID = propuesta.Id,
                    ApproverRole_ID = regla.ApproverRole_ID,
                    StepOrder = regla.StepOrder,
                    Status_ID = 1 // Pendiente
                };
                context.ProjectApprovalSteps.Add(paso);
            }

            context.SaveChanges();
        }

        public void VerMisPropuestas(User usuario)
        {
            var propuestas = context.ProjectProposals
                .Include(p => p.Area)
                .Include(p => p.Type)
                .Include(p => p.Status)
                .Where(p => p.CreateBy_ID == usuario.id)
                .ToList();

            var approvalSteps = context.ProjectApprovalSteps
                .Include(pas => pas.ApproverUser)
                .Include(pas => pas.ApproverRole)
                .Include(pas => pas.Status)
                .Where(pas => propuestas.Select(p => p.Id).Contains(pas.ProjectProposal_ID))
                .OrderBy(pas => pas.StepOrder)
                .ToList();

            if (approvalSteps.Count == 0)
            {
                Console.WriteLine("No se encontraron pasos de aprobación.");
                return; 
            }


            foreach (var propuesta in propuestas)
            {
                
                var stepsForProposal = approvalSteps
                    .Where(pas => pas.ProjectProposal_ID == propuesta.Id)
                    .ToList();

                Console.WriteLine($"\n================= Proyecto {propuesta.Id} =================");
                Console.WriteLine($"Título: {propuesta.Title}");
                Console.WriteLine($"Descripción: {propuesta.Description}");
                Console.WriteLine($"Área: {propuesta.Area.Name}");
                Console.WriteLine($"Tipo: {propuesta.Type.Name}");
                Console.WriteLine($"Monto estimado: ${propuesta.EstimatedAmount}");
                Console.WriteLine($"Duración estimada: {propuesta.EstimatedDuration} días");
                Console.WriteLine($"Estado general: {propuesta.Status.Name}");

                Console.WriteLine("\nFlujo de aprobación:");

                foreach (var paso in stepsForProposal)
                {
                    string aprobador = paso.ApproverUser?.Name ?? $"Rol: {paso.ApproverRole?.Name}";
                    Console.WriteLine($"{paso.Status?.Name} - {aprobador}");
                }
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

            }
        }
    }
}
