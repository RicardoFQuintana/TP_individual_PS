using _3_Aplicacion.Dto.Response;

namespace _3_Aplicacion.Interfaces.IServices
{
     public class ApiPendingStepsService : IApiPendingStepsService
 {
     private readonly IUsuarioAutenticacionService _usuario;
     private readonly IAprobacionConsultaService _consulta;

     public ApiPendingStepsService(IUsuarioAutenticacionService usuario, IAprobacionConsultaService consulta)
     {
         _usuario = usuario;
         _consulta = consulta;
     }

     public async Task<List<ApprovalStep>> ObtenerPendientes(int userId)
     {
         var usuario = await _usuario.Login(userId);
         if (usuario == null) return new List<ApprovalStep>();

         var pasosFiltrados = await _consulta.ObtenerPasosPendientesFiltrados(usuario);

         return pasosFiltrados.Select(p => new ApprovalStep
         {
             id = p.Id,
             stepOrder = p.StepOrder,
             decisionDate = p.DecisionDate,
             observations = p.Observations,
             approverUser = p.ApproverUser != null ? new Users
             {
                 id = p.ApproverUser.Id,
                 name = p.ApproverUser.Name,
                 email = p.ApproverUser.Email,
                 role = p.ApproverUser.Role != null ? new GenericResponse
                 {
                     id = p.ApproverUser.Role.Id,
                     name = p.ApproverUser.Role.Name
                 } : null
             } : null,
             approverRole = p.ApproverRole != null ? new GenericResponse
             {
                 id = p.ApproverRole.Id,
                 name = p.ApproverRole.Name
             } : null,
             status = p.Status != null ? new GenericResponse
             {
                 id = p.Status.Id,
                 name = p.Status.Name
             } : null,
             projectId = p.ProjectProposalId
         }).ToList();
     }
 }
}