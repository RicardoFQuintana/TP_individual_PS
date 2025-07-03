using _3_Aplicacion.Dto.Response;

namespace _3_Aplicacion.Interfaces.IServices
{
    public interface IApiPendingStepsService
    {
        Task<List<ApprovalStep>> ObtenerPendientes(int userId);
    }
}