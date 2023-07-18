using System;
using MediatR;
using Vestillo.Business.Models;

namespace Dominio.Queries
{
	public class IdDoFuncionarioQuery: IRequest<Produtividade>
    {
		public int idDoFuncionario { get; set; }
	}
}

