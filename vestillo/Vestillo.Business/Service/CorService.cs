﻿using System;
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
    public class CorService: GenericService<Cor, CorRepository, CorController>
    {
        public CorService()
        {
            base.RequestUri = "Cor";
        }

        public new ICorService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new CorServiceWeb(this.RequestUri);
            }
            else
            {
                return new CorServiceAPP();
            }
        }   

    }
}
