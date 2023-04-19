using System;
using System.Security.Cryptography;
using Dominio.Models;

namespace Dominio.Services
{
	public class UsuarioService : Interface.IUsuario
	{
        private VestilloRotinas.Interface.IFuncionarioService _funcionarioService;
        public UsuarioService(VestilloRotinas.Interface.IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }
        public AuthenticateResponse Authenticate(Usuario usuario)
        {
            try
            {
                var jwtToken = TokenService.GenerateToken(usuario);
                var refreshToken = TokenService.GenerateRefreshToken();
                      
                return new AuthenticateResponse(usuario, jwtToken, refreshToken);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public Usuario ObterUsuario(string QrCode)
        {
            int id = 1;
            var funcionario = _funcionarioService.ObterPorId(id);
            if (funcionario != null && funcionario.Id > 0)
            {
                return new Usuario
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    QrCode = QrCode
                };
            }
            return null;
        }
    }
}

