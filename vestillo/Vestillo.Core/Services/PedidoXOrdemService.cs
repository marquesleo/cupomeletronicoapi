﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class PedidoXOrdemService : GenericService<PedidoXOrdemView, PedidoXOrdemRepository>
    {
        
        public IEnumerable<PedidoXOrdemView> GetDadosDaProducao(bool TestaAlmoxarifado)
        {
            return _repository.GetDadosDaProducao(TestaAlmoxarifado);
        }        

    }
}

