using System;
using Dominio.Models;

namespace Dominio.Services.Interface
{
	public interface IUsuario
	{
        AuthenticateResponse Authenticate(Usuario usuario);
        Usuario ObterUsuario(string QrCode);

    }
}

