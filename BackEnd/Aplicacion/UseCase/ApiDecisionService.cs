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
    public class ApiDecisionService : IApiDecisionService
    {
        private readonly IProyectoConsultaService _consulta;
        private readonly IProyectoPasoConsultaService _pasoConsulta;
        private readonly IAprobacionDecisionService _decision;
        private readonly IProyectoFlujoService _flujo;
        private readonly IProyectoCreacionService _creacion;

        public ApiDecisionService(IProyectoConsultaService consulta, IProyectoPasoConsultaService pasoConsulta,
            IAprobacionDecisionService decision, IProyectoFlujoService flujo, IProyectoCreacionService creacion)
        {
            _consulta = consulta;
            _pasoConsulta = pasoConsulta;
            _decision = decision;
            _flujo = flujo;
            _creacion = creacion;
        }

        public async Task<bool> TomarDecision(Guid id, DecisionStep dto)
        {
            var proyect = await _consulta.ObtenerPropuestaPorId(id);

            var paso = await _pasoConsulta.ObtenerPasoPorId(dto.id);
            if (paso == null || paso.StatusId != 1) return false;

            var pasos = await _pasoConsulta.ObtenerPasosDePropuesta(id);
            if (pasos.Any(p => p.StepOrder < paso.StepOrder && p.StatusId == 1)) return false;

            bool resultado = dto.status switch
            {
                2 => await _decision.AprobarPaso(dto.id, dto.user, dto.observation ?? ""),
                3 => await _decision.RechazarPaso(dto.id, dto.user, dto.observation ?? ""),
                4 => await _decision.ObservarPaso(dto.id, dto.user, dto.observation ?? ""),
                _ => false
            };

            if (!resultado) return false;

            await _flujo.EvaluarEstadoPropuesta(id);
            return true;
        }
    }
}
