using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class RefreshTokenView
    {
        public int idusuario { get; set; }      
        public string refreshtoken { get; set; }
        public string accesstoken { get; set; }
    }
}
