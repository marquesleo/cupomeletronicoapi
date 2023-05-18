using System;
using Dominio.Commands;
using MediatR;

namespace Dominio.Handlers
{
	public class SalvarOperacaoHandler : IRequestHandler<SalvarOperacaoCommand,Unit>
	{
        private readonly Dominio.Services.VestilloRotinas.OperacaoService operacaoService;
        public SalvarOperacaoHandler()
        {
            this.operacaoService =  new Services.VestilloRotinas.OperacaoService();
        }

        public async Task<Unit> Handle(SalvarOperacaoCommand request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}

