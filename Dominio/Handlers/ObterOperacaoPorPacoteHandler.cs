using System;
using AutoMapper;
using Dominio.Models.DTO;
using Dominio.Queries;
using MediatR;

namespace Dominio.Handlers
{
	public class ObterOperacaoPorPacoteHandler : IRequestHandler<Queries.NumeroDoPacoteQuery, List<Dominio.Models.DTO.Operacao>>
    {
        private readonly Dominio.Services.VestilloRotinas.OperacaoService operacaoService;
        private readonly Dominio.Services.VestilloRotinas.PacoteProducaoService pacoteProducaoService;
        private IMapper _mapper;
        public ObterOperacaoPorPacoteHandler(IMapper mapper)
		{
            this.operacaoService = new Services.VestilloRotinas.OperacaoService();
            this.pacoteProducaoService = new Services.VestilloRotinas.PacoteProducaoService();
            this._mapper = mapper;
        }

        public async Task<List<Operacao>> Handle(NumeroDoPacoteQuery request, CancellationToken cancellationToken)
        {
            var lstOperacoes = new List<Operacao>();
            var pacote = await pacoteProducaoService.ObterPacotePorId(request.NumeroDoPacote);
            if (pacote != null)
            {
                var operacoes = await operacaoService.ObterOperacoesDoPacote(pacote.GrupoPacoteId, pacote.Id);
                lstOperacoes = _mapper.Map<List<Dominio.Models.DTO.Operacao>>(operacoes);
                
            }
            return lstOperacoes;
        }
    }
}

