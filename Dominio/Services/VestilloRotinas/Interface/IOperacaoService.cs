using System;
using Vestillo.Business.Models;

namespace Dominio.Services.VestilloRotinas.Interface
{
	public interface IOperacaoService
	{
       Task <IEnumerable<Vestillo.Business.Models.OperacoesPorOperacaoCupom>> ObterOperacoes(int idFuncionario);
       Task<IEnumerable<Vestillo.Business.Models.GrupoOperacoesView>> ObterOperacoesDoPacote(int grupoDoPacote, int pacoteId);
        Task<Produtividade> ObterTempoDeOperacao(int idUsuario);



    }

}

