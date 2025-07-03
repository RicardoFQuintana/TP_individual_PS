using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface IProyectoFlujoService
    {
        Task EvaluarEstadoPropuesta(Guid propuestaId);
    }
}
