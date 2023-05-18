using System;
using Dominio.Models.DTO;
using MediatR;

namespace CupomEletronicoAPI.Handlers
{
	public class ObterOperacaoHandler : IRequestHandler<Queries.OperacaoQuery, List<Dominio.Models.DTO.Operacao>>
	{
		private readonly Dominio.Services.VestilloRotinas.OperacaoService operacaoService;
		public ObterOperacaoHandler() 
		{
			this.operacaoService = new Dominio.Services.VestilloRotinas.OperacaoService();
		}


        public async Task<List<Operacao>> Handle(Queries.OperacaoQuery request, CancellationToken cancellationToken)
        {
			var lstOperacoes = new List<Operacao>();
            var operacoes = await operacaoService.ObterOperacoes(request.IdFuncionario);
			if (operacoes != null && operacoes.Any())
			{
				foreach (var item in operacoes)
				{
					var op = new Operacao();
					op.Descricao = item.OperacaoDescricao;
					lstOperacoes.Add(op);
				}
			}

			return lstOperacoes;
        }
		
    }
}

