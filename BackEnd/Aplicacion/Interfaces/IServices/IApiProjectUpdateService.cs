using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _3_Aplicacion.Dto.Request;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface IApiProjectUpdateService
    {
        Task<bool> Actualizar(Guid id, ProjectUpdate update);
    }
}
