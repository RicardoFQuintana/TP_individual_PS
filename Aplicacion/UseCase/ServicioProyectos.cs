using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _4_Dominio;
using _3_Aplicacion.Interfaces.ICommands;
using _3_Aplicacion.Interfaces.IQuerys;
using _3_Aplicacion.Interfaces.IServices;
using _3_Aplicacion.Dto;

namespace _3_Aplicacion.UseCase
{
    public class ServicioProyectos : IServicioProyectos
    {
        private readonly IProyectoQuerys _proyectoQ;
        private readonly IProyectoCommands _proyectoC;
        private readonly IAprobacionCommands _aprobacionC;
        private readonly IFlujoAprobacionGenerator _flujoGenerator;
        private readonly IAreaQuerys _areaQ;
        private readonly ITypeQuerys _typeQ;

        public ServicioProyectos(IProyectoQuerys proyectoQ, IProyectoCommands proyectoC, IFlujoAprobacionGenerator flujoGenerator,
            IAprobacionCommands aprobacionC, IAreaQuerys areaQ, ITypeQuerys typeQ)
        {
            _proyectoQ = proyectoQ;
            _proyectoC = proyectoC;
            _flujoGenerator = flujoGenerator;
            _aprobacionC = aprobacionC;
            _typeQ = typeQ;
        }

        public async Task<List<ProjectProposal>> MisPropuestas(User usuario)
        {
            return await _proyectoQ.ObtenerPropuestasDeUsuarioAsync(usuario.id);
        }
        public async Task<List<ProjectApprovalStep>> MisPasos(List<ProjectProposal> propuestas)
        {
            return await _proyectoQ.ObtenerPasosDePropuestasAsync(propuestas);
        }
        public async Task CrearPropuesta(CrearPropuestaDto dto, User usuario)
        {
            var propuesta = new ProjectProposal
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Area_ID = dto.Area_ID,
                Type_ID = dto.Type_ID,
                EstimatedAmount = dto.EstimatedAmount,
                EstimatedDuration = dto.EstimatedDuration,
                CreateAt = DateTime.Now,
                Status_ID = 1,// Pendiente
                CreateBy_ID = usuario.id
            };

            await _proyectoC.CrearPropuestaAsync(propuesta);

            var pasos = await _flujoGenerator.GenerarFlujoAsync(propuesta);

            await _aprobacionC.GuardarPasosAsync(pasos);
        }
        public async Task<List<Area>> ObternerArea()
        {
            return await _areaQ.ObtenerTodasAsync();

        }
        public async Task<List<ProjectType>> ObternerTipos()
        {
            return await _typeQ.ObtenerTodosAsync();
        }
        public async Task EvaluarEstadoPropuesta(Guid propuestaId)
        {
            var pasos = await _proyectoQ.ObtenerPasosPorPropuestaAsync(propuestaId);

            if (pasos.All(p => p.Status_ID == 2)) // Todos aprobados
            {
                await _proyectoC.CambiarEstadoPropuestaAsync(propuestaId, 2);
            }
            else if (pasos.Any(p => p.Status_ID == 3)) // Alguno rechazado
            {
                await _proyectoC.CambiarEstadoPropuestaAsync(propuestaId, 3);
            }
            else if (pasos.Any(p => p.Status_ID == 4)) // Alguno observado
            {
                await _proyectoC.CambiarEstadoPropuestaAsync(propuestaId, 4);
            }
        }
    }
}
