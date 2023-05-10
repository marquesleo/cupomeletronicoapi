using System;
using MediatR;

namespace Dominio.Commands
{
	public record SalvarOperacaoCommand(List<Dominio.Models.DTO.Operacao> operacoes) : IRequest;

}

