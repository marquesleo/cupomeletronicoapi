using System;
namespace Dominio.Services.VestilloRotinas.Interface
{
	public interface IOperacaoService
	{
       Task <IEnumerable<Vestillo.Business.Models.GrupoOperacoesView>> ObterOperacoes(int idFuncionario);
       Task<IEnumerable<Vestillo.Business.Models.GrupoOperacoesView>> ObterOperacoesDoPacote(int grupoDoPacote, int pacoteId);



    }

}

