using System;
using MediatR;

namespace CupomEletronicoAPI.Commands
{
	public record SalvarOperacaoCommand(List<Dominio.Models.DTO.Operacao> operacoes) : IRequest;

}

