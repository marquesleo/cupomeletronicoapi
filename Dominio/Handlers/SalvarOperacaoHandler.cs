using System;
using Dominio.Commands;
using MediatR;

namespace Dominio.Handlers
{
	public class SalvarOperacaoHandler : IRequestHandler<SalvarOperacaoCommand,Unit>
	{
        private readonly Dominio.Services.VestilloRotinas.PacoteProducaoService pacoteProducaoService;
        public SalvarOperacaoHandler()
        {
            this.pacoteProducaoService =  new Services.VestilloRotinas.PacoteProducaoService();
        }

        public async Task<Unit> Handle(SalvarOperacaoCommand request, CancellationToken cancellationToken)
        {
            await pacoteProducaoService.Salvar(request.operacoes);
            return Unit.Value;
        }
    }
}

