using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace CupomEletronicoAPI.Controllers
{
    [Route("api/v{version:apiVersion}/usuario")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class UsuarioController : BaseController
    {
        private Dominio.Services.Interface.IUsuario _usuarioService;
        public UsuarioController(Dominio.Services.Interface.IUsuario usuarioService, IConfiguration configuration)
        {
            this._usuarioService = usuarioService;
            this.config = configuration;
            Dominio.ConfigVestillo.Iniciar(config.GetConnectionString("db"),
                                            Convert.ToInt32(config.GetSection("parametros").GetSection("empresa").Value));

            System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);
        }


        [HttpPost]
        [Route("autenticar")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Autenticar([FromBody] Dominio.Models.Usuario usuarioViewModel)
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

    }
}
