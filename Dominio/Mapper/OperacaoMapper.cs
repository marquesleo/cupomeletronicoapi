using System;
using AutoMapper;
using Vestillo.Business.Models;

namespace Dominio.Mapper
{
	public class OperacaoMapper : Profile
	{
		public OperacaoMapper()
        {

            CreateMap<GrupoOperacoesView, Models.DTO.Operacao>();
            CreateMap<Models.DTO.Operacao, GrupoOperacoesView>();
            
        }
	}
}

