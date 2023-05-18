﻿using System;
namespace Dominio.Models.DTO
{
	public class Operacao
	{
		public Operacao()
		{}
	    public string Faccao { get; set; }
        public string Funcionario { get; set; }
        public DateTime? DataConclusao { get; set; }
        public int QtdOperacao { get; set; }
        public int SequenciaQuebra { get; set; }
        public string QuebraManual { get; set; }
        public int SetorId { get; set; }
        public bool Quebra { get; set; }
        public int PacoteId { get; set; }
        public int PacoteStatus { get; set; }
        public string FuncionarioCupom { get; set; }
        public string PacoteReferencia { get; set; }
        public decimal TempoPacote { get; set; }
        public decimal TempoUnitario { get; set; }
        public decimal TempoTotal { get; set; }
        public DateTime DataEmissao { get; set; }
        public string ProdutoDescricao { get; set; }
        public string ProdutoReferencia { get; set; }
        public string OperacaoPadraoReferencia { get; set; }
        public string OrdemProducaoReferencia { get; set; }
        public string CorDescricao { get; set; }
        public string TamanhoDescricao { get; set; }
        public decimal Quantidade { get; set; }
        public string Status { get; }
	}
}

