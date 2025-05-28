using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Request;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;

namespace _3_Aplicacion.UseCase
{
    public class ProyectoCreacionService : IProyectoCreacionService
    {
        private readonly IProyectoCommands _proyectoC;
        private readonly IAprobacionCommands _aprobacionC;
        private readonly IFlujoAprobacionGenerator _flujoGenerator;

        public ProyectoCreacionService(IProyectoCommands proyectoC, IFlujoAprobacionGenerator flujoGenerator, IAprobacionCommands aprobacionC)
        {
            _proyectoC = proyectoC;
            _flujoGenerator = flujoGenerator;
            _aprobacionC = aprobacionC;
        }

        
        public async Task<ProjectProposal> CrearPropuesta(ProjectCreate dto)
        {
            var propuesta = new ProjectProposal
            {
                Id = Guid.NewGuid(),
                Title = dto.title,
                Description = dto.description,
                AreaId = dto.area,
                TypeId = dto.type,
                EstimatedAmount = dto.amount,
                EstimatedDuration = dto.duration,
                CreateAt = DateTime.Now,
                StatusId = 1,// Pendiente
                CreateById = dto.createdBy,
            };

            await _proyectoC.CrearPropuestaAsync(propuesta);

            var pasos = await _flujoGenerator.GenerarFlujoAsync(propuesta);

            await _aprobacionC.GuardarPasosAsync(pasos);

            return propuesta;

        }
        
    }
}
