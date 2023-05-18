using System;
using MediatR;

namespace CupomEletronicoAPI.Queries
{
	public class OperacaoQuery: IRequest<List<Dominio.Models.DTO.Operacao>>
	{
		public OperacaoQuery()
		{

		}
		public int IdFuncionario { get; set; }
	}
}

