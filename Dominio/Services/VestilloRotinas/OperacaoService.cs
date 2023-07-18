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


        private ProdutividadeRepository _produtividadeRepository;
        private ProdutividadeRepository ProdutividadeRepository
        {
            get
            {
                if (_produtividadeRepository == null)
                    _produtividadeRepository = new ProdutividadeRepository();
                return _produtividadeRepository;
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

        public async Task<IEnumerable<OperacoesPorOperacaoCupom>> ObterOperacoes(int idFuncionario)
        {
            return GrupoOperacoesRepository.GetbyOperacoesPorOperador(idFuncionario,true);
        }

        public async Task<IEnumerable<GrupoOperacoesView>> ObterOperacoesDoPacote(int grupoDoPacote, int pacoteId)
        {
            return GrupoOperacoesRepository.GetListByGrupoPacoteVisualizar(grupoDoPacote,pacoteId);
        }

        public async Task<Produtividade> ObterTempoDeOperacao(int idUsuario)
        {
            return ProdutividadeRepository.GetByFuncionarioIdEData(idUsuario,DateTime.Now.Date);
        }



    }
}

