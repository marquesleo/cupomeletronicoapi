using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Services.VestilloRotinas.Interface
{
    public interface IFuncionarioService
    {
       Vestillo.Business.Models.Funcionario ObterPorId(int id);
    }
}
