using System;
namespace Dominio.Services.VestilloRotinas.Interface
{
	public interface IOperacaoService
	{
       Task <IEnumerable<Vestillo.Business.Models.OperacaoFaccaoView>> ObterOperacoes(int idFuncionario);
    }

}

