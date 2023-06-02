using System;
using Vestillo.Business.Models;

namespace Dominio.Services.VestilloRotinas.Interface
{
	public interface IPacoteProducaoService
	{
		Task<PacoteProducaoView> ObterPacotePorId(int idPacote);
		Task Salvar(List<Models.DTO.Operacao> operacoes);
    }
}

