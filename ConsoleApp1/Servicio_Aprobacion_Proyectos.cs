using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BD;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class Servicio_Aprobacion_Proyectos
    {
        private readonly ProyectosContext context;

        public Servicio_Aprobacion_Proyectos(ProyectosContext context)
        {
            this.context = context;
        }

        public void AprobarPropuestasPendientes(User usuario)
        {

            var pasosPendientes = context.ProjectApprovalSteps
                .Include(p => p.ProjectProposal)
                    .ThenInclude(p => p.Area)
                .Include(p => p.ProjectProposal)
                    .ThenInclude(p => p.Type)
                .Include(p => p.ApproverRole)
                .Include(p => p.Status)
                .Where(p =>
                    p.Status_ID == 1 &&                        
                    (p.ApproverUser_ID == null || p.ApproverUser_ID == usuario.id) && 
                    usuario.Role_ID == p.ApproverRole_ID)     
                .ToList()
                .Where(p => p.ApproverUser_ID == null)         
                .GroupBy(p => p.ProjectProposal_ID)           
                .Select(g => g.OrderBy(p => p.StepOrder).First()) 
                .ToList();


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
                Console.Write("¿Deseás aprobar (A), rechazar (R) o dejarlo en observacion (O) este paso? ");
                string? opcion = Console.ReadLine()?.Trim().ToUpper();

                if (opcion == "A")
                {
                    paso.Status_ID = 2;
                    paso.ApproverUser_ID = usuario.id;
                    paso.DecisionDate = DateTime.Now;

                    propuesta.Status_ID = 2;
                }
                else if (opcion == "R")
                {
                    paso.Status_ID = 3; 
                    paso.ApproverUser_ID = usuario.id;
                    paso.DecisionDate = DateTime.Now;

                    
                    propuesta.Status_ID = 3;
                }
                else if (opcion == "O")
                {
                    paso.Status_ID = 4;
                    paso.ApproverUser_ID = usuario.id;
                    paso.DecisionDate = DateTime.Now;

                    propuesta.Status_ID = 4;
                }
            }

            context.SaveChanges();
            Console.WriteLine("\n Cambios guardados correctamente.");
        }
    }
}
