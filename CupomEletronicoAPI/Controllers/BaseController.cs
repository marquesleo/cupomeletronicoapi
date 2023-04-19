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
        public BaseController()
        {
           
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
