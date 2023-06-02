using System;
using MediatR;

namespace Dominio.Queries
{
	public class NumeroDoPacoteQuery: IRequest<List<Dominio.Models.DTO.Operacao>>
    {
		public int NumeroDoPacote { get; set; }
	}
}

