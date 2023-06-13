using System;
namespace Dominio.Services.VestilloRotinas.Interface
{
	public interface IOperacaoService
	{
       Task <IEnumerable<Vestillo.Business.Models.OperacoesPorOperacaoCupom>> ObterOperacoes(int idFuncionario);
       Task<IEnumerable<Vestillo.Business.Models.GrupoOperacoesView>> ObterOperacoesDoPacote(int grupoDoPacote, int pacoteId);



    }

}

