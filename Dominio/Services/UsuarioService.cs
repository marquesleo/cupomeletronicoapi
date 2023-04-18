using System;
using System.Security.Cryptography;
using Dominio.Models;

namespace Dominio.Services
{
	public class UsuarioService
	{
		public UsuarioService()
		{
		}

        public AuthenticateResponse Authenticate(Usuario usuario, string ipAddress)
        {
            try
            {
                var jwtToken = TokenService.GenerateToken(usuario);
                var refreshToken = generateRefreshToken();
               
                refreshToken.Id = Guid.NewGuid();
                refreshToken.UsuarioId = usuario.Id;

                
                return new AuthenticateResponse(usuario, jwtToken, refreshToken.Token);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private RefreshToken generateRefreshToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    
                };
            }
        }
    }
}

