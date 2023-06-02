﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Service.Web;
using Vestillo.Lib;
using Vestillo.Connection;

namespace Vestillo.Business.Service
{
    public class CreditosClientesService : GenericService<CreditosClientes, CreditosClientesRepository, CreditosClientesController>
    {
        public CreditosClientesService()
        {
            base.RequestUri = "creditoscliente";
        }

        public new ICreditosClientesService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new CreditosClientesServiceWeb(this.RequestUri);
            }
            else
            {
                return new CreditosClientesServiceAPP();
            }
        }  
    }
}
