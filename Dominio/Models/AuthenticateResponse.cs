using System;
namespace Dominio.Models
{
	public class AuthenticateResponse
	{
        public int Id { get; set; }
        public string Nome { get; set; }
        public string token { get; set; }
        public string RefreshToken { get; set; }
        public bool UtilizaCupom { get; set; }

        public AuthenticateResponse(Usuario user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            Nome = user.Nome;
            token = jwtToken;
            RefreshToken = refreshToken;
            UtilizaCupom = user.UtilizaCupom;
        }
    }
}

