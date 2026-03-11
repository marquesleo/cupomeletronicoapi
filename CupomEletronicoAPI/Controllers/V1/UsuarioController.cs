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
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(Dominio.Services.Interface.IUsuario usuarioService,
                                 IConfiguration configuration,
                                 ILogger<UsuarioController> logger):base(configuration)
        {
            this._usuarioService = usuarioService;
            this._logger = logger;  
        }


        [HttpGet, Route("version")]
        [AllowAnonymous]
        public ActionResult<string> Version()
        {
            return Ok("1.0");
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
                    _logger.LogInformation("[UsuarioController][Autenticar] Begin");
                    _logger.LogInformation("[UsuarioController][Autenticar] Retornando usuario");
                    var usuario = _usuarioService.ObterUsuario(usuarioViewModel.QrCode);

                    if (usuario == null || usuario.Id == 0)
                    {
                        _logger.LogWarning("[UsuarioController][Autenticar] Usuario nao encontrado ou invalido");
                        return BadRequest(new { message = "Usuário ou senha inválidos" });
                    }

                    _logger.LogInformation("[UsuarioController][Autenticar] Usuario retornado");
                    _logger.LogInformation("[UsuarioController][Autenticar] Retornando token ");
                    var response = _usuarioService.Authenticate(usuario);
                    _logger.LogInformation("[UsuarioController][Autenticar] Token Retornado ");
                    return Ok(response);

                }
                else
                {
                    _logger.LogWarning("[UsuarioController][Autenticar] Usuario nao encontrado ou invalido");
                    return BadRequest(new { message = "Usuário ou senha inválidos" });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return StatusCode(500, new { message = ex.Message });
            }

        }


        [AllowAnonymous]
        [Route("refresh-token")]
        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenView refreshTokenView)
        {
            /* if (refreshTokenView != null && !string.IsNullOrEmpty(refreshTokenView.refreshtoken))
             {
                 var response = await _usuarioService.RefreshToken(refreshTokenView);

                 if (response == null)
                     return Unauthorized(new { message = "Invalid token" });

                 return Ok(response);
             }
             else
                 return Unauthorized(new { message = "Invalid token" });*/
            return Ok();
        }

    }
}
