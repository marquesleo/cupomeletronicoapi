﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Connection;
using System.Data;

namespace Vestillo.Core.Repositories
{
    public class AcompanhamentoColecaoRepository : GenericRepository<AcompanhamentoColecaoView>
    {
        public IEnumerable<AcompanhamentoColecaoView> ListRelatorioAcompanhamentoColecao(List<int> colecaoId, int AlmoxarifadoId, decimal? percentualEstoqueIdeal, DateTime DataInicio, DateTime DataFim, int[] corIds, bool exibirCotacao)
        {
            string Valor = String.Empty;
            if (DataInicio.ToString("yyyy-MM-dd") != "0001-01-01")
            {               
                Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";
            }

            /*
            DateTime DataInicio, DateTime DataFim,
             var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";

            if (filtro.tamanhosIds != null && filtro.tamanhosIds.Length > 0)
                SQL.AppendLine("        AND pd.idTamanho IN (" + string.Join(", ", filtro.tamanhosIds) + ")");

            GC.Collect();
            */
            AtualizaTotalOps(colecaoId, corIds);


            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT ");
            sql.AppendLine(" ExibirCotacao, ");
            sql.AppendLine(" PercentualEstoqueIdeal, ");
            sql.AppendLine(" Entrega, ");
            sql.AppendLine(" Colecao, ");
            sql.AppendLine(" Referencia, ");
            sql.AppendLine(" Cor, ");
            sql.AppendLine(" Tamanho, ");
            sql.AppendLine(" SUM(IFNULL(Preco, 0)) AS Preco, ");
            sql.AppendLine(" SUM(IFNULL(OP, 0)) AS OP, ");
            sql.AppendLine(" SUM(IFNULL(PlanoProducao, 0)) AS PlanoProducao, ");
            sql.AppendLine(" SUM(IFNULL(Empenho, 0)) AS Empenho, ");
            sql.AppendLine(" SUM(IFNULL(Fabricado, 0)) AS Fabricado, ");
            sql.AppendLine(" EntradaDeTroca, ");
            sql.AppendLine(" SUM(IFNULL(Devolucao, 0)) AS Devolucao, ");
            sql.AppendLine(" SUM(IFNULL(Mostruarios, 0)) AS Mostruarios, ");
            sql.AppendLine(" SUM(IFNULL(Remessa, 0)) as Remessa, ");
            sql.AppendLine(" SUM(IFNULL(Marketing, 0)) AS Marketing, ");
            sql.AppendLine(" sum(IFNULL(TotalSaidaSemVendas, 0)) as TotalSaidaSemVendas, ");
            sql.AppendLine(" sum(IFNULL(NotaFiscalSaida, 0)) as NotaFiscalSaida, ");
            sql.AppendLine(" SUM(IFNULL(Especial, 0)) as Especial, ");
            sql.AppendLine(" sum(IFNULL(DCI, 0)) AS DCI, ");
            sql.AppendLine(" SUM(IFNULL(CaboFrio, 0)) as CaboFrio, ");
            sql.AppendLine(" sum(IFNULL(AmoMuito, 0)) as AmoMuito, ");
            sql.AppendLine(" SUM(IFNULL(TotalSaidaVendas, 0)) as TotalSaidaVendas, ");
            sql.AppendLine(" SUM(IFNULL(Faturado, 0)) as Faturado, ");
            sql.AppendLine(" sum(IFNULL(Cotacao, 0)) as Cotacao, ");
            sql.AppendLine(" SUM(IFNULL(Pedido, 0)) as Pedido, ");
            sql.AppendLine(" sum(IFNULL(NaoAtendido, 0)) AS NaoAtendido, ");
            sql.AppendLine(" SUM(IFNULL(SaldoAtual, 0)) AS SaldoAtual ");
            sql.AppendLine(" FROM ( ");
            sql.AppendLine(" SELECT P.Id as ProdutoId, TAM.Id as TamanhoId, COR.Id AS CorId, P.IdAlmoxarifado, ");
            sql.AppendLine(" " + (exibirCotacao ? 1 : 0).ToString() + " AS ExibirCotacao, ");
            sql.AppendLine(" CAST('" + percentualEstoqueIdeal.GetValueOrDefault().ToString() + "' AS DECIMAL(14,4))  AS PercentualEstoqueIdeal, ");
            sql.AppendLine(" EN.Descricao AS Entrega, ");
            sql.AppendLine(" CO.Descricao AS Colecao,  ");
            sql.AppendLine(" P.Referencia AS Referencia,  ");
            sql.AppendLine(" COR.Abreviatura AS Cor, ");
            sql.AppendLine(" TAM.Abreviatura AS Tamanho, ");
            sql.AppendLine(" TP.PrecoVenda AS Preco, ");
            sql.AppendLine(" PD.TotalOp AS OP, ");
            sql.AppendLine(" PD.EstimativaProducao AS PlanoProducao, ");
            sql.AppendLine(" E.Empenhado AS Empenho, ");
            sql.AppendLine(" SUM(E.Saldo + E.Empenhado) as Fabricado, ");
            sql.AppendLine(" NULL AS EntradaDeTroca, ");
            sql.AppendLine(" 0 as Devolucao, ");
            sql.AppendLine(" 0 AS Mostruarios, ");
            sql.AppendLine(" 0 AS Remessa, ");
            sql.AppendLine(" 0 AS Marketing, ");
            sql.AppendLine(" 0 AS TotalSaidaSemVendas, ");
            sql.AppendLine(" 0 as NotaFiscalSaida, ");
            sql.AppendLine(" 0 AS Especial, ");
            sql.AppendLine(" 0 AS DCI, ");
            sql.AppendLine(" 0 AS CaboFrio, ");
            sql.AppendLine(" 0 AS AmoMuito, ");
            sql.AppendLine(" 0 AS TotalSaidaVendas, ");
            sql.AppendLine(" 0 as Faturado, ");
            sql.AppendLine(" 0 as Cotacao, ");
            sql.AppendLine(" 0 as Pedido,  ");
            sql.AppendLine(" 0 as NaoAtendido, ");
            sql.AppendLine(" SUM(E.Saldo) AS SaldoAtual ");
            sql.AppendLine(" FROM produtos                 P ");
            sql.AppendLine(" LEFT JOIN catalogo         CA ON P.IdCatalogo = CA.Id ");
            sql.AppendLine(" INNER JOIN produtoDetalhes PD ON PD.IdProduto = P.Id ");
            sql.AppendLine(" INNER JOIN almoxarifados A ON A.id = P.IdAlmoxarifado ");
            sql.AppendLine(" INNER JOIN colecoes CO ON CO.Id = P.Idcolecao ");
            if (corIds != null && corIds.Count() > 0)
            {
                sql.AppendLine(" INNER JOIN Cores COR ON COR.Id = PD.Idcor ");
            }
            else
            {
                sql.AppendLine(" LEFT JOIN Cores COR ON COR.Id = PD.Idcor ");
            }

            sql.AppendLine(" LEFT JOIN Tamanhos TAM ON TAM.Id = PD.IdTamanho ");
            sql.AppendLine(" LEFT JOIN itenstabelapreco TP ON TP.TabelaPrecoId = 1 AND TP.ProdutoId = P.Id ");
            sql.AppendLine(" LEFT JOIN estoque E ON E.ProdutoId = P.Id AND E.CorId = PD.Idcor AND E.TamanhoId = PD.IdTamanho AND E.AlmoxarifadoId = " + AlmoxarifadoId.ToString());
            sql.AppendLine(" LEFT JOIN entrega EN ON EN.Id = P.IdEmtrega ");
            sql.AppendLine(" WHERE CO.Id IN (" + string.Join(",", colecaoId) + ")");
            sql.AppendLine(" AND PD.Inutilizado = 0 ");
            if (corIds != null &&  corIds.Count() > 0)
            {
                
                sql.AppendLine("        AND PD.idcor IN (" + string.Join(", ", corIds) + ")");

            }
            sql.AppendLine(" AND (A.idempresa IS NULL OR A.idempresa = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + ")");
            sql.AppendLine(" GROUP BY P.Id, TAM.Id, COR.Id, P.IdAlmoxarifado ");
            sql.AppendLine("  ");
            sql.AppendLine(" UNION ALL ");
            sql.AppendLine(" ");
            sql.AppendLine(" SELECT P.Id as ProdutoId, TAM.Id as TamanhoId, COR.Id AS CorId, P.IdAlmoxarifado, ");
            sql.AppendLine(" " + (exibirCotacao ? 1 : 0).ToString() + " AS ExibirCotacao, ");
            sql.AppendLine(" CAST('" + percentualEstoqueIdeal.GetValueOrDefault().ToString() + "' AS DECIMAL(14,4))  AS PercentualEstoqueIdeal, ");
            sql.AppendLine(" NULL AS Entrega, ");
            sql.AppendLine(" NULL AS Colecao, ");
            sql.AppendLine(" P.Referencia AS Referencia, ");
            sql.AppendLine(" COR.Abreviatura AS Cor, ");
            sql.AppendLine(" TAM.Abreviatura AS Tamanho, ");
            sql.AppendLine(" 0 AS Preco, ");
            sql.AppendLine(" 0 AS OP, ");
            sql.AppendLine(" 0 AS PlanoProducao, ");
            sql.AppendLine(" 0 AS Empenho, ");
            sql.AppendLine(" 0 as Fabricado, ");
            sql.AppendLine(" NULL AS EntradaDeTroca, ");
            sql.AppendLine(" 0 as Devolucao, ");
            sql.AppendLine(" SUM(CASE when TN.Nome = 'Mostruarios' THEN I2.quantidade ELSE 0 END) AS Mostruarios, ");
            sql.AppendLine(" SUM(CASE when TN.Nome = 'Remessa' THEN I2.quantidade ELSE 0 END) AS Remessa, ");
            sql.AppendLine(" SUM(CASE when TN.Nome = 'Marketing' THEN I2.quantidade ELSE 0 END) AS Marketing, ");
            sql.AppendLine(" SUM(CASE when TN.Tipo = 1 THEN I2.quantidade ELSE 0 END) AS TotalSaidaSemVendas, ");
            sql.AppendLine("  0 as NotaFiscalSaida, ");
            sql.AppendLine(" SUM(CASE when TN.Nome = 'Especial' THEN I2.quantidade ELSE 0 END) AS Especial, ");
            sql.AppendLine(" SUM(CASE when TN.Nome = 'DCI' THEN I2.quantidade ELSE 0 END) AS DCI, ");
            sql.AppendLine(" SUM(CASE when TN.Nome = 'Cabo Frio' THEN I2.quantidade ELSE 0 END) AS CaboFrio, ");
            sql.AppendLine(" SUM(CASE when TN.Nome = 'Amo Muito' THEN I2.quantidade ELSE 0 END) AS AmoMuito, ");
            sql.AppendLine(" SUM(CASE when TN.Tipo = 2 THEN I2.quantidade ELSE 0 END) AS TotalSaidaVendas, ");
            sql.AppendLine(" 0 as Faturado, ");
            sql.AppendLine(" 0 AS Cotacao, ");
            sql.AppendLine(" 0 AS Pedido, ");
            sql.AppendLine("  0 AS NaoAtendido, ");
            sql.AppendLine("  0 AS SaldoAtual  ");
            sql.AppendLine(" FROM    produtos                 P ");
            sql.AppendLine(" LEFT JOIN catalogo         CA ON P.IdCatalogo = CA.Id ");
            sql.AppendLine(" INNER JOIN colecoes CO ON CO.Id = P.Idcolecao ");
            sql.AppendLine(" INNER  JOIN produtoDetalhes  PD ON PD.IdProduto = P.Id ");
            sql.AppendLine(" INNER JOIN almoxarifados A ON A.id = P.IdAlmoxarifado ");
            if (corIds != null && corIds.Count() > 0)
            {
                sql.AppendLine(" INNER JOIN Cores COR ON COR.Id = PD.Idcor ");
            }
            else
            {
                sql.AppendLine(" LEFT JOIN Cores COR ON COR.Id = PD.Idcor ");
            }
            sql.AppendLine(" LEFT  JOIN Tamanhos        TAM ON TAM.Id = PD.IdTamanho ");
            sql.AppendLine(" INNER JOIN NFeItens I2 ON I2.IdItem = P.Id AND I2.IdCor = COR.Id AND I2.IdTamanho = TAM.Id ");
            sql.AppendLine(" INNER JOIN NFE ON I2.IdNFE = NFE.ID AND Nfe.idAlmoxarifado = " + AlmoxarifadoId.ToString());
            sql.AppendLine(" INNER JOIN TIpoNegocio TN ON NFE.TipoNegocioId = TN.ID ");
            sql.AppendLine(" INNER JOIN TipoMovimentacoes on TipoMovimentacoes.Id = I2.IdTipoMov");
            sql.AppendLine(" WHERE CO.Id IN (" + string.Join(",", colecaoId) + ")");
            if (corIds != null && corIds.Count() > 0)
            {

                sql.AppendLine("        AND PD.idcor IN (" + string.Join(", ", corIds) + ")");

            }
            if (!String.IsNullOrEmpty(Valor))
            {
                sql.AppendLine("  AND  SUBSTRING(NFE.datainclusao,1,10) BETWEEN " + Valor);
            }


            sql.AppendLine(" AND PD.Inutilizado = 0 ");
            sql.AppendLine(" AND (A.idempresa IS NULL OR A.idempresa = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + ")");
            sql.AppendLine(" AND TipoMovimentacoes.AtualizaEstoque = 1");
            sql.AppendLine(" GROUP BY P.Id, TAM.Id, COR.Id, P.IdAlmoxarifado ");
            sql.AppendLine("  ");
            sql.AppendLine(" UNION ALL ");
            sql.AppendLine("  ");
            sql.AppendLine("SELECT P.Id as ProdutoId, TAM.Id as TamanhoId, COR.Id AS CorId, P.IdAlmoxarifado, ");
            sql.AppendLine("       " + (exibirCotacao ? 1 : 0).ToString() + " AS ExibirCotacao,");
            sql.AppendLine("		CAST('" + percentualEstoqueIdeal.GetValueOrDefault().ToString() + "' AS DECIMAL(14,4))  AS PercentualEstoqueIdeal, ");
            sql.AppendLine(" NULL AS Entrega, ");
            sql.AppendLine(" NULL AS Colecao, ");
            sql.AppendLine(" P.Referencia AS Referencia, ");
            sql.AppendLine(" COR.Abreviatura AS Cor, ");
            sql.AppendLine(" TAM.Abreviatura AS Tamanho, ");
            sql.AppendLine(" 0 AS Preco, ");
            sql.AppendLine(" 0 AS OP,  ");
            sql.AppendLine(" 0 AS PlanoProducao,  ");
            sql.AppendLine(" 0 AS Empenho, ");
            sql.AppendLine(" 0 as Fabricado, ");
            sql.AppendLine(" null AS EntradaDeTroca, ");
            sql.AppendLine(" 0 as Devolucao, ");
            sql.AppendLine(" 0 AS Mostruarios, ");
            sql.AppendLine(" 0 AS Remessa, ");
            sql.AppendLine(" 0 AS Marketing, ");
            sql.AppendLine(" 0 AS TotalSaidaSemVendas, ");
            sql.AppendLine(" 0 as NotaFiscalSaida, ");
            sql.AppendLine(" 0 AS Especial, ");
            sql.AppendLine(" 0 AS DCI, ");
            sql.AppendLine(" 0 AS CaboFrio, ");
            sql.AppendLine(" 0 AS AmoMuito, ");
            sql.AppendLine(" 0 AS TotalSaidaVendas, ");
            sql.AppendLine(" 0 as Faturado, ");
            sql.AppendLine(" SUM(CASE WHEN PV.Status = 1 THEN IPV.Qtd ELSE 0 END) as Cotacao, ");
            sql.AppendLine(" (SUM(LI.QtdEmpenhada) + SUM(LI.QtdNaoAtendida)) as Pedido, ");
            sql.AppendLine(" SUM(LI.QtdNaoAtendida) as NaoAtendido, ");
            sql.AppendLine(" 0 AS SaldoAtual            ");
            sql.AppendLine(" FROM produtos                 P ");
            sql.AppendLine(" LEFT JOIN catalogo         CA ON P.IdCatalogo = CA.Id ");
            sql.AppendLine(" INNER JOIN colecoes CO ON CO.Id = P.Idcolecao ");
            sql.AppendLine(" INNER JOIN produtoDetalhes PD ON PD.IdProduto = P.Id ");
            sql.AppendLine(" INNER JOIN almoxarifados A ON A.id = P.IdAlmoxarifado ");

            if (corIds != null && corIds.Count() > 0)
            {
                sql.AppendLine(" INNER JOIN Cores COR ON COR.Id = PD.Idcor ");
            }
            else
            {
                sql.AppendLine(" LEFT JOIN Cores COR ON COR.Id = PD.Idcor ");
            }

            sql.AppendLine(" LEFT JOIN Tamanhos TAM ON TAM.Id = PD.IdTamanho ");
            sql.AppendLine(" INNER JOIN ItensPedidoVenda IPV ON IPV.ProdutoId = P.Id AND IPV.CorId = COR.Id AND IPV.TamanhoId = TAM.Id ");
            sql.AppendLine(" INNER JOIN pedidovenda PV ON PV.Id = IPV.PedidoVendaId AND PV.AlmoxarifadoId = " + AlmoxarifadoId.ToString());
            sql.AppendLine(" LEFT JOIN itensliberacaopedidovenda LI ON LI.ItemPedidoVendaID = IPV.Id ");
            sql.AppendLine(" WHERE CO.Id IN (" + string.Join(",", colecaoId) + ")");
            sql.AppendLine(" AND PD.Inutilizado = 0 ");
            if (corIds != null && corIds.Count() > 0)
            {

                sql.AppendLine("        AND PD.idcor IN (" + string.Join(", ", corIds) + ")");

            }
            if (!String.IsNullOrEmpty(Valor))
            {
                sql.AppendLine("  AND  SUBSTRING(PV.DataEmissao,1,10) BETWEEN " + Valor);
            }


            sql.AppendLine(" AND (A.idempresa IS NULL OR A.idempresa = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + ")");
            sql.AppendLine(" GROUP BY P.Id, TAM.Id, COR.Id, P.IdAlmoxarifado ");
            sql.AppendLine("  ");
            sql.AppendLine(" UNION ALL ");
            sql.AppendLine("  ");
            sql.AppendLine("SELECT P.Id as ProdutoId, TAM.Id as TamanhoId, COR.Id AS CorId, P.IdAlmoxarifado, ");
            sql.AppendLine("       " + (exibirCotacao ? 1 : 0).ToString() + " AS ExibirCotacao,");
            sql.AppendLine("		CAST('" + percentualEstoqueIdeal.GetValueOrDefault().ToString() + "' AS DECIMAL(14,4))  AS PercentualEstoqueIdeal, ");
            sql.AppendLine(" NULL AS Entrega, ");
            sql.AppendLine(" NULL AS Colecao, ");
            sql.AppendLine(" P.Referencia AS Referencia, ");
            sql.AppendLine(" COR.Abreviatura AS Cor, ");
            sql.AppendLine(" TAM.Abreviatura AS Tamanho, ");
            sql.AppendLine(" 0 AS Preco, ");
            sql.AppendLine(" 0 AS OP, ");
            sql.AppendLine(" 0 AS PlanoProducao, ");
            sql.AppendLine(" 0 AS Empenho, ");
            sql.AppendLine(" Sum(I2.QtdDevolvida) as Fabricado, ");
            sql.AppendLine(" NULL AS EntradaDeTroca, ");
            sql.AppendLine("  SUM(I2.QtdDevolvida) as Devolucao, ");
            sql.AppendLine(" 0 AS Mostruarios, ");
            sql.AppendLine(" 0 AS Remessa, ");
            sql.AppendLine(" 0 AS Marketing, ");
            sql.AppendLine(" 0 AS TotalSaidaSemVendas, ");
            sql.AppendLine(" 0 as NotaFiscalSaida, ");
            sql.AppendLine(" 0 AS Especial, ");
            sql.AppendLine(" 0 AS DCI, ");
            sql.AppendLine(" 0 AS CaboFrio, ");
            sql.AppendLine(" 0 AS AmoMuito, ");
            sql.AppendLine(" 0 AS TotalSaidaVendas, ");
            sql.AppendLine(" 0 as Faturado, ");
            sql.AppendLine(" 0 AS Cotacao, ");
            sql.AppendLine(" 0 AS Pedido, ");
            sql.AppendLine(" 0 AS NaoAtendido,  ");
            sql.AppendLine(" 0 AS SaldoAtual ");
            sql.AppendLine(" FROM produtos                 P ");
            sql.AppendLine(" LEFT JOIN catalogo         CA ON P.IdCatalogo = CA.Id ");
            sql.AppendLine(" INNER JOIN colecoes CO ON CO.Id = P.Idcolecao ");
            sql.AppendLine(" INNER JOIN produtoDetalhes PD ON PD.IdProduto = P.Id ");
            sql.AppendLine(" INNER JOIN almoxarifados A ON A.id = P.IdAlmoxarifado ");

            if (corIds != null && corIds.Count() > 0)
            {
                sql.AppendLine(" INNER JOIN Cores COR ON COR.Id = PD.Idcor ");
            }
            else
            {
                sql.AppendLine(" LEFT JOIN Cores COR ON COR.Id = PD.Idcor ");
            }

            sql.AppendLine(" LEFT JOIN Tamanhos TAM ON TAM.Id = PD.IdTamanho ");
            sql.AppendLine(" INNER JOIN NFeItens I2 ON I2.IdItem = P.Id AND I2.IdCor = COR.Id AND I2.IdTamanho = TAM.Id ");
            sql.AppendLine(" INNER JOIN NFE ON I2.IdNFE = NFE.ID AND Nfe.idAlmoxarifado = " + AlmoxarifadoId.ToString());
            sql.AppendLine(" INNER JOIN TipoMovimentacoes on TipoMovimentacoes.Id = I2.IdTipoMov");
            sql.AppendLine(" WHERE CO.Id IN (" + string.Join(",", colecaoId) + ")");
            sql.AppendLine(" AND PD.Inutilizado = 0 ");
            if (corIds != null && corIds.Count() > 0)
            {

                sql.AppendLine("        AND PD.idcor IN (" + string.Join(", ", corIds) + ")");

            }
            if (!String.IsNullOrEmpty(Valor))
            {
                sql.AppendLine("  AND  SUBSTRING(NFE.datainclusao,1,10) BETWEEN " + Valor);
            }


            sql.AppendLine(" AND (A.idempresa IS NULL OR A.idempresa = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + ")");
            sql.AppendLine(" AND Nfe.StatusNota <> 2 ");
            sql.AppendLine(" AND I2.QtdDevolvida > 0 ");
            sql.AppendLine(" AND TipoMovimentacoes.AtualizaEstoque = 1");
            sql.AppendLine(" GROUP BY P.Id, TAM.Id, COR.Id, P.IdAlmoxarifado ");
            sql.AppendLine(" ");
            sql.AppendLine(" UNION ALL ");
            sql.AppendLine("  ");
            sql.AppendLine("SELECT P.Id as ProdutoId, TAM.Id as TamanhoId, COR.Id AS CorId, P.IdAlmoxarifado, ");
            sql.AppendLine("       " + (exibirCotacao ? 1 : 0).ToString() + " AS ExibirCotacao,");
            sql.AppendLine("		CAST('" + percentualEstoqueIdeal.GetValueOrDefault().ToString() + "' AS DECIMAL(14,4))  AS PercentualEstoqueIdeal, ");
            sql.AppendLine(" NULL AS Entrega, ");
            sql.AppendLine(" NULL AS Colecao, ");
            sql.AppendLine(" P.Referencia AS Referencia, ");
            sql.AppendLine(" COR.Abreviatura AS Cor, ");
            sql.AppendLine(" TAM.Abreviatura AS Tamanho, ");
            sql.AppendLine(" 0 AS Preco, ");
            sql.AppendLine(" 0 AS OP, ");
            sql.AppendLine(" 0 AS PlanoProducao, ");
            sql.AppendLine(" 0 AS Empenho, ");
            sql.AppendLine(" 0 as Fabricado, ");
            sql.AppendLine(" NULL AS EntradaDeTroca, ");
            sql.AppendLine(" 0 as Devolucao, ");
            sql.AppendLine(" 0 AS Mostruarios, ");
            sql.AppendLine(" 0 AS Remessa, ");
            sql.AppendLine(" 0 AS Marketing, ");
            sql.AppendLine(" 0 AS TotalSaidaSemVendas, ");
            sql.AppendLine(" SUM(i2.quantidade) as NotaFiscalSaida, ");
            sql.AppendLine(" 0 AS Especial, ");
            sql.AppendLine(" 0 AS DCI, ");
            sql.AppendLine(" 0 AS CaboFrio, ");
            sql.AppendLine(" 0 AS AmoMuito, ");
            sql.AppendLine(" 0 AS TotalSaidaVendas, ");
            sql.AppendLine(" 0 as Faturado, ");
            sql.AppendLine(" 0 AS Cotacao, ");
            sql.AppendLine(" 0 AS Pedido, ");
            sql.AppendLine(" 0 AS NaoAtendido,  ");
            sql.AppendLine(" 0 AS SaldoAtual ");
            sql.AppendLine(" FROM produtos                 P ");
            sql.AppendLine(" LEFT JOIN catalogo         CA ON P.IdCatalogo = CA.Id ");
            sql.AppendLine(" INNER JOIN colecoes CO ON CO.Id = P.Idcolecao ");
            sql.AppendLine(" INNER JOIN produtoDetalhes PD ON PD.IdProduto = P.Id ");
            sql.AppendLine(" INNER JOIN almoxarifados A ON A.id = P.IdAlmoxarifado ");

            if (corIds != null && corIds.Count() > 0)
            {
                sql.AppendLine(" INNER JOIN Cores COR ON COR.Id = PD.Idcor ");
            }
            else
            {
                sql.AppendLine(" LEFT JOIN Cores COR ON COR.Id = PD.Idcor ");
            }

            sql.AppendLine(" LEFT JOIN Tamanhos TAM ON TAM.Id = PD.IdTamanho ");
            sql.AppendLine(" INNER JOIN NFeItens I2 ON I2.IdItem = P.Id AND I2.IdCor = COR.Id AND I2.IdTamanho = TAM.Id ");
            sql.AppendLine(" INNER JOIN NFE ON I2.IdNFE = NFE.ID AND Nfe.idAlmoxarifado = " + AlmoxarifadoId.ToString());
            sql.AppendLine(" INNER JOIN TIpoNegocio TN ON NFE.TipoNegocioId = TN.ID ");
            sql.AppendLine(" INNER JOIN TipoMovimentacoes on TipoMovimentacoes.Id = I2.IdTipoMov");
            sql.AppendLine(" WHERE CO.Id IN (" + string.Join(",", colecaoId) + ")");
            sql.AppendLine(" AND PD.Inutilizado = 0 ");
            if (corIds != null && corIds.Count() > 0)
            {

                sql.AppendLine("        AND PD.idcor IN (" + string.Join(", ", corIds) + ")");

            }
            if (!String.IsNullOrEmpty(Valor))
            {
                sql.AppendLine("  AND  SUBSTRING(NFE.datainclusao,1,10) BETWEEN " + Valor);
            }

            sql.AppendLine(" AND (A.idempresa IS NULL OR A.idempresa = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + ")");
            sql.AppendLine(" AND Nfe.StatusNota <> 2 ");
            sql.AppendLine(" AND Nfe.Tipo IN (0,2,5,7) ");
            sql.AppendLine(" AND(ISNULL(TN.nome) OR TN.nome = 'Nota Fiscal Saída') ");
            sql.AppendLine(" AND TipoMovimentacoes.AtualizaEstoque = 1");
            sql.AppendLine(" GROUP BY P.Id, TAM.Id, COR.Id, P.IdAlmoxarifado ");
            sql.AppendLine(" ");
            sql.AppendLine(" UNION ALL ");
            sql.AppendLine("  ");
            sql.AppendLine("SELECT P.Id as ProdutoId, TAM.Id as TamanhoId, COR.Id AS CorId, P.IdAlmoxarifado, ");
            sql.AppendLine("       " + (exibirCotacao ? 1 : 0).ToString() + " AS ExibirCotacao,");
            sql.AppendLine("		CAST('" + percentualEstoqueIdeal.GetValueOrDefault().ToString() + "' AS DECIMAL(14,4))  AS PercentualEstoqueIdeal, ");
            sql.AppendLine(" NULL AS Entrega, ");
            sql.AppendLine(" NULL AS Colecao, ");
            sql.AppendLine(" P.Referencia AS Referencia, ");
            sql.AppendLine(" COR.Abreviatura AS Cor, ");
            sql.AppendLine(" TAM.Abreviatura AS Tamanho, ");
            sql.AppendLine(" 0 AS Preco, ");
            sql.AppendLine(" 0 AS OP, ");
            sql.AppendLine(" 0 AS PlanoProducao, ");
            sql.AppendLine(" 0 AS Empenho, ");
            sql.AppendLine(" 0 as Fabricado, ");
            sql.AppendLine(" NULL AS EntradaDeTroca, ");
            sql.AppendLine(" 0 as Devolucao, ");
            sql.AppendLine(" 0 AS Mostruarios, ");
            sql.AppendLine(" 0 AS Remessa, ");
            sql.AppendLine(" 0 AS Marketing, ");
            sql.AppendLine(" 0 AS TotalSaidaSemVendas, ");
            sql.AppendLine(" 0 as NotaFiscalSaida, ");
            sql.AppendLine(" 0 AS Especial, ");
            sql.AppendLine(" 0 AS DCI, ");
            sql.AppendLine(" 0 AS CaboFrio, ");
            sql.AppendLine(" 0 AS AmoMuito, ");
            sql.AppendLine(" 0 AS TotalSaidaVendas, ");
            sql.AppendLine(" SUM(i2.quantidade) as Faturado, ");
            sql.AppendLine(" 0 AS Cotacao, ");
            sql.AppendLine(" 0 AS Pedido, ");
            sql.AppendLine(" 0 AS NaoAtendido,  ");
            sql.AppendLine(" 0 AS SaldoAtual ");
            sql.AppendLine(" FROM produtos                 P ");
            sql.AppendLine(" LEFT JOIN catalogo         CA ON P.IdCatalogo = CA.Id ");
            sql.AppendLine(" INNER JOIN colecoes CO ON CO.Id = P.Idcolecao ");
            sql.AppendLine(" INNER JOIN produtoDetalhes PD ON PD.IdProduto = P.Id ");
            sql.AppendLine(" INNER JOIN almoxarifados A ON A.id = P.IdAlmoxarifado ");

            if (corIds != null && corIds.Count() > 0)
            {
                sql.AppendLine(" INNER JOIN Cores COR ON COR.Id = PD.Idcor ");
            }
            else
            {
                sql.AppendLine(" LEFT JOIN Cores COR ON COR.Id = PD.Idcor ");
            }

            sql.AppendLine(" LEFT JOIN Tamanhos TAM ON TAM.Id = PD.IdTamanho ");
            sql.AppendLine(" INNER JOIN NFeItens I2 ON I2.IdItem = P.Id AND I2.IdCor = COR.Id AND I2.IdTamanho = TAM.Id ");
            sql.AppendLine(" INNER JOIN NFE ON I2.IdNFE = NFE.ID AND Nfe.idAlmoxarifado = " + AlmoxarifadoId.ToString());
            sql.AppendLine(" INNER JOIN TipoMovimentacoes on TipoMovimentacoes.Id = I2.IdTipoMov");
            sql.AppendLine(" WHERE CO.Id IN (" + string.Join(",", colecaoId) + ")");

            if (corIds != null && corIds.Count() > 0)
            {

                sql.AppendLine("        AND PD.idcor IN (" + string.Join(", ", corIds) + ")");

            }
            if (!String.IsNullOrEmpty(Valor))
            {
                sql.AppendLine("  AND  SUBSTRING(NFE.datainclusao,1,10) BETWEEN " + Valor);
            }

            sql.AppendLine(" AND PD.Inutilizado = 0 ");
            sql.AppendLine(" AND (A.idempresa IS NULL OR A.idempresa = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + ")");
            sql.AppendLine(" AND Nfe.StatusNota <> 2 ");
            sql.AppendLine(" AND Nfe.Tipo IN (0,2,5,7) ");
            sql.AppendLine(" AND Nfe.DataEmissao IS NOT NULL ");
            sql.AppendLine(" AND TipoMovimentacoes.AtualizaEstoque = 1");
            sql.AppendLine(" GROUP BY P.Id, TAM.Id, COR.Id, P.IdAlmoxarifado ");
            sql.AppendLine(" ");
            sql.AppendLine(" UNION ALL ");
            sql.AppendLine("  ");
            sql.AppendLine("SELECT P.Id as ProdutoId, TAM.Id as TamanhoId, COR.Id AS CorId, P.IdAlmoxarifado, ");
            sql.AppendLine("       " + (exibirCotacao ? 1 : 0).ToString() + " AS ExibirCotacao,");
            sql.AppendLine("		CAST('" + percentualEstoqueIdeal.GetValueOrDefault().ToString() + "' AS DECIMAL(14,4))  AS PercentualEstoqueIdeal, ");
            sql.AppendLine(" NULL AS Entrega, ");
            sql.AppendLine(" NULL AS Colecao, ");
            sql.AppendLine(" P.Referencia AS Referencia, ");
            sql.AppendLine(" COR.Abreviatura AS Cor, ");
            sql.AppendLine(" TAM.Abreviatura AS Tamanho, ");
            sql.AppendLine(" 0 AS Preco, ");
            sql.AppendLine(" 0 AS OP, ");
            sql.AppendLine(" 0 AS PlanoProducao, ");
            sql.AppendLine(" 0 AS Empenho, ");
            sql.AppendLine(" SUM(I2.quantidade) as Fabricado, ");
            sql.AppendLine(" NULL AS EntradaDeTroca, ");
            sql.AppendLine(" 0 as Devolucao, ");
            sql.AppendLine(" 0 AS Mostruarios, ");
            sql.AppendLine(" 0 AS Remessa, ");
            sql.AppendLine(" 0 AS Marketing, ");
            sql.AppendLine(" 0 AS TotalSaidaSemVendas, ");
            sql.AppendLine(" 0 as NotaFiscalSaida, ");
            sql.AppendLine(" 0 AS Especial, ");
            sql.AppendLine(" 0 AS DCI, ");
            sql.AppendLine(" 0 AS CaboFrio, ");
            sql.AppendLine(" 0 AS AmoMuito, ");
            sql.AppendLine(" 0 AS TotalSaidaVendas, ");
            sql.AppendLine(" 0 as Faturado, ");
            sql.AppendLine(" 0 AS Cotacao, ");
            sql.AppendLine(" 0 AS Pedido, ");
            sql.AppendLine(" 0 AS NaoAtendido,  ");
            sql.AppendLine(" 0 AS SaldoAtual ");
            sql.AppendLine(" FROM produtos                 P ");
            sql.AppendLine(" LEFT JOIN catalogo         CA ON P.IdCatalogo = CA.Id ");
            sql.AppendLine(" INNER JOIN colecoes CO ON CO.Id = P.Idcolecao ");
            sql.AppendLine(" INNER JOIN produtoDetalhes PD ON PD.IdProduto = P.Id ");
            sql.AppendLine(" INNER JOIN almoxarifados A ON A.id = P.IdAlmoxarifado ");
            if (corIds != null && corIds.Count() > 0)
            {
                sql.AppendLine(" INNER JOIN Cores COR ON COR.Id = PD.Idcor ");
            }
            else
            {
                sql.AppendLine(" LEFT JOIN Cores COR ON COR.Id = PD.Idcor ");
            }

            sql.AppendLine(" LEFT JOIN Tamanhos TAM ON TAM.Id = PD.IdTamanho ");
            sql.AppendLine(" INNER JOIN NFeItens I2 ON I2.IdItem = P.Id AND I2.IdCor = COR.Id AND I2.IdTamanho = TAM.Id ");
            sql.AppendLine(" INNER JOIN NFE ON I2.IdNFE = NFE.ID AND Nfe.idAlmoxarifado = " + AlmoxarifadoId.ToString());
            sql.AppendLine(" INNER JOIN TipoMovimentacoes on TipoMovimentacoes.Id = I2.IdTipoMov");
            sql.AppendLine(" WHERE CO.Id IN (" + string.Join(",", colecaoId) + ")");

            if (corIds != null && corIds.Count() > 0)
            {

                sql.AppendLine("        AND PD.idcor IN (" + string.Join(", ", corIds) + ")");

            }
            if (!String.IsNullOrEmpty(Valor))
            {
                sql.AppendLine("  AND  SUBSTRING(NFE.datainclusao,1,10) BETWEEN " + Valor);
            }


            sql.AppendLine(" AND PD.Inutilizado = 0 ");
            sql.AppendLine(" AND (A.idempresa IS NULL OR A.idempresa = " + Vestillo.Lib.Funcoes.GetIdEmpresaLogada.ToString() + ")");
            sql.AppendLine(" AND Nfe.StatusNota <> 2 ");
            sql.AppendLine(" AND Nfe.Tipo IN (0,2,5,7) ");
            sql.AppendLine(" AND TipoMovimentacoes.AtualizaEstoque = 1");
            sql.AppendLine(" GROUP BY P.Id, TAM.Id, COR.Id, P.IdAlmoxarifado ");
            sql.AppendLine(" ) AS T ");
            sql.AppendLine(" GROUP BY ProdutoId, TamanhoId, CorId, IdAlmoxarifado ");

            return VestilloConnection.ExecSQLToListWithNewConnection<AcompanhamentoColecaoView>(sql.ToString());
        }

        private void AtualizaTotalOps(List<int> colecaoId,int[] corIds)
        {


            try
            {
                string sqlPrd = "select id from produtos where Idcolecao IN(" + string.Join(",", colecaoId) + ")";
                DataTable dtPrd = new DataTable();
                dtPrd = VestilloConnection.ExecToDataTable(sqlPrd);

                for (int i = 0; i < dtPrd.Rows.Count; i++)
                {
                    string sqlUpdateProdutos = "update produtodetalhes set TotalOp = 0 WHERE produtodetalhes.IdProduto =  " + dtPrd.Rows[i]["id"];
                    VestilloConnection.ExecNonQuery(sqlUpdateProdutos);

                }

            

                string sql = "select * from itensordemproducao WHERE itensordemproducao.ProdutoId IN(" + sqlPrd + ")" + " order by itensordemproducao.id ";
                DataTable dt2 = new DataTable();

                dt2 = VestilloConnection.ExecToDataTable(sql);

                for (int l = 0; l < dt2.Rows.Count; l++)
                {
                    string UpdateGradeProdutos = "UPDATE produtodetalhes set TotalOp = TotalOp + " + dt2.Rows[l]["Quantidade"] + " WHERE produtodetalhes.IdProduto = " + dt2.Rows[l]["ProdutoId"] + " AND " +
                                                         " produtodetalhes.Idcor = " + dt2.Rows[l]["CorId"] + " AND " + " produtodetalhes.IdTamanho = " + dt2.Rows[l]["TamanhoId"];

                    VestilloConnection.ExecNonQuery(UpdateGradeProdutos);

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
