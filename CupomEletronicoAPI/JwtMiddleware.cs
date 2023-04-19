using Dominio.Services.Interface;
using Dominio.Services;
using Microsoft.Extensions.Options;

namespace CupomEletronicoAPI
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUsuario _IUsuario;
        public JwtMiddleware(RequestDelegate next,
            IUsuario IUsuario)
        {
            _next = next;
            _IUsuario = IUsuario;
        }

        public async Task Invoke(HttpContext context, IUsuario userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] =  userService.ObterUsuario(userId);
            }

            await _next(context);
        }
    }
}
