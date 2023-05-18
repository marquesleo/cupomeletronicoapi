using System;
using MediatR;

namespace Dominio.Queries
{
	public class AutenticacaoQuery : IRequest
	{
		public Models.Usuario Usuario { get; set; }
	}
}

