using System;
using Dominio.Models;

namespace Dominio.Services.Interface
{
	public interface IUsuario
	{
        AuthenticateResponse Authenticate(Usuario usuario);
        Task<AuthenticateResponse> RefreshToken(RefreshTokenView refreshToken);
        Models.Usuario ObterUsuario(string QrCode);
        Models.Usuario ObterUsuario(int idUsuario);

    }
}

