using System;
using Dominio.Queries;
using MediatR;
using Vestillo.Business.Models;

namespace Dominio.Handlers
{
	public class ObterTempoDeOperacaoHandler : IRequestHandler<Queries.IdDoFuncionarioQuery, Produtividade>
    {
        private readonly Dominio.Services.VestilloRotinas.OperacaoService operacaoService;
        public ObterTempoDeOperacaoHandler()
		{
            this.operacaoService = new Services.VestilloRotinas.OperacaoService();
        }

        public Task<Produtividade> Handle(IdDoFuncionarioQuery request, CancellationToken cancellationToken)
        {
            return operacaoService.ObterTempoDeOperacao(request.idDoFuncionario);
        }
    }
}

