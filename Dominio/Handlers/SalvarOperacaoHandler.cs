using System;
using Dominio.Commands;
using MediatR;

namespace Dominio.Handlers
{
	public class SalvarOperacaoHandler : IRequestHandler<SalvarOperacaoCommand,Unit>
	{
        private readonly Dominio.Services.VestilloRotinas.OperacaoService operacaoService;
        public SalvarOperacaoHandler(Dominio.Services.VestilloRotinas.OperacaoService operacaoService)
        {
            this.operacaoService = operacaoService;
        }

        public async Task<Unit> Handle(SalvarOperacaoCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}

