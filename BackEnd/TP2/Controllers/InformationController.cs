using _3_Aplicacion.Dto.Response;
using _3_Aplicacion.Interfaces.IServices;
using _4_Dominio;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api")]
    public class InformationController : ControllerBase
    {
        private readonly IUsuarioConsultaService _userCS;
        private readonly IRolConsultaService _rolCS;
        private readonly IAreaConsultaService _areaCS;
        private readonly ITypeConsultaService _typeCS;
        private readonly IStatusConsultaServices _statusCS;

        public InformationController(IAreaConsultaService areaCS, ITypeConsultaService typeCS, 
                                        IRolConsultaService rolCS, IStatusConsultaServices statusCS, IUsuarioConsultaService userCS)
        {
            _areaCS = areaCS;
            _typeCS = typeCS;
            _rolCS = rolCS;
            _statusCS = statusCS;
            _userCS = userCS;
        }



        [HttpGet("Area")]
        public async Task<ActionResult<List<Area>>> GetAreas()
        {
            try
            {
                var result = await _areaCS.ObtenerAreasApi();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Error al obtener las áreas: {ex.Message}" });
            }
        }

        [HttpGet("ProjectType")]
        public async Task<ActionResult<List<ProjectType>>> GetProjectTypes()
        {
            try
            {
                var result = await _typeCS.ObtenerTiposApi();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Error al obtener los tipos: {ex.Message}" });
            }
        }

        [HttpGet("Role")]
        public async Task<ActionResult<List<ApproverRole>>> GetRoles()
        {
            try
            {
                var result = await _rolCS.ObtenerRolesApi();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Error al obtener los roles: {ex.Message}" });
            }
        }

        [HttpGet("ApprovalStatus")]
        public async Task<ActionResult<List<ApprovalStatus>>> GetStatuses()
        {
            try
            {
                var result = await _statusCS.ObtenerStatusApi();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Error al obtener los estados: {ex.Message}" });
            }
        }

        [HttpGet("User")]
        public async Task<ActionResult<List<Users>>> GetUsers()
        {
            try
            {
                var result = await _userCS.ObtenerUsuariosApi();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiError { message = $"Error al obtener los usuarios: {ex.Message}" });
            }
        }
    }
}
