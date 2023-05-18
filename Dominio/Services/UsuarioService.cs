using System;
using System.Net;
using System.Security.Cryptography;
using Dominio.Models;
using Vestillo.Business.Models;

namespace Dominio.Services
{
	public class UsuarioService : Interface.IUsuario
	{
        private VestilloRotinas.Interface.IFuncionarioService _funcionarioService;
        private IJwtUtils _jwtUtils;
        public UsuarioService(VestilloRotinas.Interface.IFuncionarioService funcionarioService,
            IJwtUtils jwtUtils)
        {
            _funcionarioService = funcionarioService;
            _jwtUtils = jwtUtils;
        }
        public AuthenticateResponse Authenticate(Models.Usuario usuario)
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

        public Models.Usuario ObterUsuario(string QrCode)
        {
            int id = 1;
            var funcionario = _funcionarioService.ObterPorId(id);
            if (funcionario != null && funcionario.Id > 0)
            {
                return new Models.Usuario
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    QrCode = QrCode,
                    UtilizaCupom = Convert.ToBoolean(funcionario.UsaCupom)
                };
            }
            return null;
        }

        public Models.Usuario ObterUsuario(int idUsuario)
        {
            int id = 1;
            var funcionario = _funcionarioService.ObterPorId(id);
            if (funcionario != null && funcionario.Id > 0)
            {
                return new Models. Usuario
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome
                    
                };
            }
            return null;
        }

        public async Task<AuthenticateResponse> RefreshToken(RefreshTokenView refreshTokenView)
        {
            try
            {
                RefreshToken refresh = new RefreshToken();
                var usuario = ObterUsuario(refreshTokenView.idusuario);
                // generate new jwt
                var jwtToken = TokenService.GenerateToken(usuario);
               
                var newRefreshToken = rotateRefreshToken(refresh);
                return new AuthenticateResponse(usuario, jwtToken, newRefreshToken.Token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private RefreshToken rotateRefreshToken(RefreshToken refreshToken)
        {

            var newRefreshToken = _jwtUtils.GenerateRefreshToken();
            revokeRefreshToken(refreshToken, "Replaced by new token", newRefreshToken.Token);

            return newRefreshToken;
        }

        private void revokeRefreshToken(RefreshToken token,  string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.ReplacedByToken = replacedByToken;
        }
    }
}

