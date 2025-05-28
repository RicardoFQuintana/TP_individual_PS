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
    public class ProyectoValidacionService : IProyectoValidacionService
    {
        private readonly IProyectoQuerys _proyectoQ;

        public ProyectoValidacionService(IProyectoQuerys proyectoQ)
        {
            _proyectoQ = proyectoQ;
        }

        public async Task<bool> ExisteTitulo(string Title)
        {
            return await _proyectoQ.ExisteTituloAsync(Title);
        }
    }   
}
