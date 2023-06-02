﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class ContadorRemessaService : GenericService<ContadorRemessa, ContadorRemessaRepository, ContadorRemessaController>
    {
        public ContadorRemessaService()
        {
            base.RequestUri = "ContadorRemessa";
        }

        public new IContadorRemessaService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ContadorRemessaServiceWeb(this.RequestUri);
            }
            else
            {
                return new ContadorRemessaServiceAPP();
            }
        }
    }
}
