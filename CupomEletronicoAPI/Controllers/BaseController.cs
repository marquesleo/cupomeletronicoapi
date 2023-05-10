using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupomEletronicoAPI.Controllers
{
    public abstract class BaseController : Controller
    {
        //private readonly INotificador _notificador;
        //public BaseController(INotificador notificador)
        //{
        //    _notificador = notificador;
        //}
        protected  IConfiguration config;
        public BaseController(IConfiguration configuration)
        {
            this.config = configuration;
            Dominio.ConfigVestillo.Iniciar(config.GetConnectionString("db"),
                                            Convert.ToInt32(config.GetSection("parametros").GetSection("empresa").Value));

            EncodingProvider ppp = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);
        }
        //protected bool OperacacaoValida()
        //{
        //    return !_notificador.TemNotificacao();
        //}
        protected bool OperacacaoValida()
        {
            return true;
        }
    }
}
