using System;
using Vestillo;

namespace Dominio.Models.DTO
{
	public class Operacao
	{
		public Operacao()
		{}

        public int? Id { get; set; }
        public int? SetorId { get; set; }
        public int? BalanceamentoId { get; set; }
        public int? GrupoPacoteId { get; set; }
        public string? TituloCupom { get; set; }
        public string? strTituloCupom
        {
            get
            {
                if (!string.IsNullOrEmpty(TituloCupom) && TituloCupom.Length > 26)
                    return TituloCupom.Substring(0, 26);
                else
                    return TituloCupom;
            }
        }
        public int? OperacaoPadraoId { get; set; }
        public decimal? Tempo { get; set; }
        public decimal? TempoCalculado { get; set; }
        public decimal? TempoCronometrado { get; set; }
        public string? Sequencia { get; set; }
        public string? Texto { get; set; }
        public int? IdOperadorCupomEletronico { get; set; }
        public string? Faccao { get; set; }
        public string? Funcionario { get; set; }
        public DateTime? DataConclusao { get; set; }
        public bool flag { get; set; } = true;
        public string nomeDoBotao { get; set; } = "Concluído";
        public bool FoiFeita
        {
            get
            {
                if (DataConclusao == null)
                    return false;

                return DataConclusao.Value.Year > 1900;
            }
        }
        public int? QtdOperacao { get; set; }
        public int? SequenciaQuebra { get; set; }
        public string? QuebraManual { get; set; }
        public bool? Quebra { get; set; }
        public int? PacoteId { get; set; }
        public int? PacoteStatus { get; set; }
        public string? FuncionarioCupom { get; set; }
        public string? PacoteReferencia { get; set; }
        public decimal? TempoPacote { get; set; }
        public decimal? TempoUnitario { get; set; }
        public decimal? TempoTotal { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string? ProdutoDescricao { get; set; }
        public string? ProdutoReferencia { get; set; }
        public string? OperacaoPadraoReferencia { get; set; }
        public string? OrdemProducaoReferencia { get; set; }
        public string? CorDescricao { get; set; }
        public string? TamanhoDescricao { get; set; }
        public decimal? Quantidade { get; set; }
        public string? Status { get; }
        public int? IdFuncionario { get; set; }
        public string? RefOperacao { get; set; }
        public bool concluido { get; set; }



    }
}

