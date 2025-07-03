using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Request;
using _3_Aplicacion.Dto.Response;
using _3_Aplicacion.Interfaces.IServices;

namespace _3_Aplicacion.UseCase
{
    public class ApiProjectUpdateService : IApiProjectUpdateService
    {
        private readonly IProyectoConsultaService _consulta;
        private readonly IProyectoValidacionService _validacion;
        private readonly IProyectoActualizacionService _actualizacion;
        private readonly IProyectoCreacionService _creacion;

        public ApiProjectUpdateService(IProyectoConsultaService consulta, IProyectoValidacionService validacion,
            IProyectoActualizacionService actualizacion, IProyectoCreacionService creacion)
        {
            _consulta = consulta;
            _validacion = validacion;
            _actualizacion = actualizacion;
            _creacion = creacion;
        }

        public async Task<bool> Actualizar(Guid id, ProjectUpdate update)
        {
            var propuesta = await _consulta.ObtenerPropuestaPorId(id);

            if (!string.Equals(propuesta.Title.Trim(), update.title.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                if (await _validacion.ExisteTitulo(update.title)) return false;
            }

            propuesta.Title = update.title;
            propuesta.Description = update.description;
            propuesta.EstimatedDuration = update.duration;

            await _actualizacion.ActualizarPropuesta(propuesta);
            return true;
        }
    }
}
