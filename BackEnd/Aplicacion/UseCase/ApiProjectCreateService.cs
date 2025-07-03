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
    public class ApiProjectCreateService : IApiProjectCreateService
    {
        private readonly IProyectoValidacionService _validacion;
        private readonly IUsuarioAutenticacionService _usuarios;
        private readonly IProyectoCreacionService _creacion;

        public ApiProjectCreateService(IProyectoValidacionService validacion, IUsuarioAutenticacionService usuarios, IProyectoCreacionService creacion)
        {
            _validacion = validacion;
            _usuarios = usuarios;
            _creacion = creacion;
        }

        public async Task<Guid?> Crear(ProjectCreate project)
        {
            var usuario = await _usuarios.Login(project.user);
            if (usuario == null) 
                return null;

            var propuestaYaExiste = await _validacion.ExisteTitulo(project.title);
            if (propuestaYaExiste)
                return null;

            var propuesta = await _creacion.CrearPropuesta(project);
            return propuesta.Id;
        }
    }
}
