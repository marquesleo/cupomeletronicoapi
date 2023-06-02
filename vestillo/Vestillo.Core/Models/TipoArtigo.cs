﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{

    [Tabela("TipoArtigo")]
    public class TipoArtigo : IModel
    {
        [Chave]
        public int Id { get; set; }       
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
