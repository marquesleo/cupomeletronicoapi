using System;
using System.Collections.Generic;
using AutoMapper;
using Dominio.Handlers;
using Dominio.Models.DTO;
using MediatR;

namespace Dominio.Handlers
{
	public class ObterOperacaoHandler : IRequestHandler<Queries.OperacaoQuery, List<Dominio.Models.DTO.Operacao>>
	{
		private readonly Dominio.Services.VestilloRotinas.OperacaoService operacaoService;
        private IMapper _mapper;
        public ObterOperacaoHandler(IMapper mapper) 
		{
			this.operacaoService = new Services.VestilloRotinas.OperacaoService();
            this._mapper = mapper;
		}


        public async Task<List<Operacao>> Handle(Queries.OperacaoQuery request, CancellationToken cancellationToken)
        {
			var lstOperacoes = new List<Operacao>();
            var operacoes = await operacaoService.ObterOperacoes(request.IdFuncionario);
            lstOperacoes = _mapper.Map<List<Dominio.Models.DTO.Operacao>>(operacoes);
            return lstOperacoes;
        }
		
    }
}

