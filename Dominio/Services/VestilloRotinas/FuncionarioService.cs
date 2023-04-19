using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Repositories;

namespace Dominio.Services.VestilloRotinas
{
    public class FuncionarioService : Interface.IFuncionarioService
    {
        private FuncionarioRepository? _funcionarioRepository;
        private FuncionarioRepository funcionarioRepository
        {
            get
            {
                if (_funcionarioRepository == null)
                    _funcionarioRepository = new FuncionarioRepository();
                return _funcionarioRepository;
            }
        }

        public Vestillo.Business.Models.Funcionario ObterPorId(int id)
        {
            return funcionarioRepository.GetById(id);
        }
    }
}

