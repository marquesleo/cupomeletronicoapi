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
        
        string GetNewConnectionString()
        {
            var cript = new Vestillo.Lib.Cripto();
            var s = config.GetConnectionString("db");
            string password = s
                .Split(';')
                .FirstOrDefault(part => part.StartsWith("pwd="))
                ?.Split('=')[1];
            
            var valorSemCriptografia =  cript.Decrypt(password);
            s = s.Replace(password, valorSemCriptografia);
            return s;
        }
        public BaseController(IConfiguration configuration)
        {
            config = configuration;
            var connectionString = GetNewConnectionString();

            Dominio.ConfigVestillo.Iniciar(connectionString,
                                            Convert.ToInt32(config.GetSection("parametros").GetSection("empresa").Value));

            EncodingProvider ppp = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);

           
        }
   
    }
}
