using System;
using MediatR;

namespace Dominio.Queries
{
	public class OperacaoQuery: IRequest<List<Dominio.Models.DTO.Operacao>>
	{
		public int IdFuncionario { get; set; }
	}
}

