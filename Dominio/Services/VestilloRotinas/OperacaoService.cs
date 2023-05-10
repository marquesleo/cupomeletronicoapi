using System;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Dominio.Services.VestilloRotinas
{
	public class OperacaoService : Interface.IOperacaoService
    {

        private OperacaoFaccaoRepository? _OperacaoFaccaoRepository;
        private OperacaoFaccaoRepository OperacaoFaccaoRepository
        {
            get
            {
                if (_OperacaoFaccaoRepository == null)
                    _OperacaoFaccaoRepository = new OperacaoFaccaoRepository();
                return _OperacaoFaccaoRepository;
            }
        }
              

        public async  Task<IEnumerable<OperacaoFaccaoView>> ObterOperacoes(int idFuncionario)
        {
            return OperacaoFaccaoRepository.GetByFuncionario(idFuncionario);
        }
    }
}

