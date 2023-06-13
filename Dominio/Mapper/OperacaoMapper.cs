using System;
using AutoMapper;
using Vestillo.Business.Models;

namespace Dominio.Mapper
{
	public class OperacaoMapper : Profile
	{
		public OperacaoMapper()
        {

            CreateMap<OperacoesPorOperacaoCupom, Models.DTO.Operacao>();
            CreateMap<Models.DTO.Operacao, OperacoesPorOperacaoCupom>();
            
        }
	}
}

