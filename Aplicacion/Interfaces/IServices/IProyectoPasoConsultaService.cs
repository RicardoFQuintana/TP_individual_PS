using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4_Dominio;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface IProyectoPasoConsultaService
    {
        Task<List<ProjectApprovalStep>> MisPasos(List<ProjectProposal> propuestas);
        Task<List<ProjectApprovalStep>> ObtenerPasosDePropuesta(Guid id);
    }
}
