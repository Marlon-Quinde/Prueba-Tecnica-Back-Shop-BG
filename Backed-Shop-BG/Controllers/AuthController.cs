using Business.Usuario;
using Microsoft.AspNetCore.Mvc;
using Models.Queries;
using Models.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backed_Shop_BG.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioBusiness _usuarioBusiness;
        public AuthController(IUsuarioBusiness usuarioBusiness)
        {
            this._usuarioBusiness = usuarioBusiness;
        }



        [HttpPost("sign-up")]
        public async Task<IActionResult> RegistrarUsuarioController(PersonaUsuarioQuery query)
        {
            Response response = new Response();
            try
            {
                response = await _usuarioBusiness.RegistarUsuarioBusiness(query);
            }
            catch (Exception ex)
            {
                response.Code = "01";
                response.Data = null;
                response.Message = " POST register/ " + ex.Message;
            }
            return Ok(response);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> LoginUsuarioController(Login query)
        {
            Response response = new Response();
            try
            {
                response = await _usuarioBusiness.LoginUsuarioBusiness(query);
            }
            catch (Exception ex)
            {
                response.Code = "01";
                response.Data = null;
                response.Message = " POST register/ " + ex.Message;
            }
            return Ok(response);
        }

    }
}
