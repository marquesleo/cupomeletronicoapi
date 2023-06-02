using System;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Dominio.Services.VestilloRotinas
{
	public class PacoteProducaoService : Interface.IPacoteProducaoService
	{

        private PacoteProducaoRepository? _PacoteProducaoRepository;
        private PacoteProducaoRepository PacoteProducaoRepository
        {
            get
            {
                if (_PacoteProducaoRepository == null)
                    _PacoteProducaoRepository = new PacoteProducaoRepository();
                return _PacoteProducaoRepository;
            }
        }
              

        public async Task<PacoteProducaoView> ObterPacotePorId(int idPacote)
        {
            var pacote = PacoteProducaoRepository.GetByIdView(idPacote);
            return pacote;
        }

        public async Task Salvar(List<Models.DTO.Operacao> operacoes)
        {
            try
            {
                foreach (var item in operacoes)
                {
                    PacoteProducaoRepository.InsertOperacaoCupom(item.PacoteId.Value, item.OperacaoPadraoId.Value,
                                                                 item.Sequencia, item.IdFuncionario.Value,
                                                                 Convert.ToDouble(item.TempoTotal));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

