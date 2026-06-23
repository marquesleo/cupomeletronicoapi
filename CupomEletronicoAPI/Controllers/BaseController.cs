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
            var password = s
            .Split(';')
            .FirstOrDefault(p => p.StartsWith("pwd=", StringComparison.OrdinalIgnoreCase))
            ?.Substring(4);

            if (string.IsNullOrEmpty(password))
                return s;

            var valorSemCriptografia = cript.Decrypt(password);

            return s.Replace($"pwd={password}", $"pwd={valorSemCriptografia}");
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
