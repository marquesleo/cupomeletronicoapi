using System;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Dominio.Services.VestilloRotinas
{
	public class OperacaoService : Interface.IOperacaoService
    {

        private GrupoOperacoesRepository? _GrupoOperacoesRepository;
        private GrupoOperacoesRepository GrupoOperacoesRepository
        {
            get
            {
                if (_GrupoOperacoesRepository == null)
                    _GrupoOperacoesRepository = new GrupoOperacoesRepository();
                return _GrupoOperacoesRepository;
            }
        }


       

        private TempoFuncionarioRepository _TempoFuncionarioRepository;
        private TempoFuncionarioRepository TempoFuncionarioRepository
        {
            get
            {
                if (_TempoFuncionarioRepository == null)
                    _TempoFuncionarioRepository = new TempoFuncionarioRepository();
                return _TempoFuncionarioRepository;
            }
        }

        public async Task<IEnumerable<GrupoOperacoesView>> ObterOperacoes(int idFuncionario)
        {
            return GrupoOperacoesRepository.GetbyOperacoesPorOperador(idFuncionario,true);
        }

        public async Task<IEnumerable<GrupoOperacoesView>> ObterOperacoesDoPacote(int grupoDoPacote, int pacoteId)
        {
            return GrupoOperacoesRepository.GetListByGrupoPacoteVisualizar(grupoDoPacote,pacoteId);
        }

       
    }
}

