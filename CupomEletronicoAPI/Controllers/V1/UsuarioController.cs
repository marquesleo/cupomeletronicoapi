using Dominio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace CupomEletronicoAPI.Controllers.V1
{
    [Route("api/v{version:apiVersion}/usuario")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class UsuarioController : BaseController
    {
        private Dominio.Services.Interface.IUsuario _usuarioService;
        public UsuarioController(Dominio.Services.Interface.IUsuario usuarioService,
                                 IConfiguration configuration):base(configuration)
        {
            this._usuarioService = usuarioService;
          
        }


        [HttpPost]
        [Route("autenticar")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Autenticar([FromBody] Usuario usuarioViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _usuarioService.ObterUsuario(usuarioViewModel.QrCode);
                   
                    if (usuario == null || usuario.Id == 0)
                        return BadRequest(new { message = "Usuário ou senha inválidos" });

                    var response = _usuarioService.Authenticate(usuario);

                    return Ok(response);

                }
                else
                    return BadRequest(new { message = "Usuário ou senha inválidos" });

            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { message = ex.Message });
            }

        }


        [AllowAnonymous]
        [Route("refresh-token")]
        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenView refreshTokenView)
        {
            if (refreshTokenView != null && !string.IsNullOrEmpty(refreshTokenView.refreshtoken))
            {
                var response = await _usuarioService.RefreshToken(refreshTokenView);

                if (response == null)
                    return Unauthorized(new { message = "Invalid token" });

                return Ok(response);
            }
            else
                return Unauthorized(new { message = "Invalid token" });
        }

    }
}
