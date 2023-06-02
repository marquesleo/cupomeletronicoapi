﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IMovimentacaoBancoService : IService<MovimentacaoBanco, MovimentacaoBancoRepository, MovimentacaoBancoController>
    {
        IEnumerable<MovimentacaoBancoView> GetCamposBrowse();
        IEnumerable<MovimentacaoBancoView> GetRelExtratoBancarioBrowse(FiltroExtratoBancarioRel filtro);
    }

}