using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CupomEletronicoAPI.Controllers.V1
{
    [Route("api/v{version:apiVersion}/operacao")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class OperacaoController : BaseController
    {
        private readonly IConfiguration configuration;
        private readonly ISender sender;

        public OperacaoController(IConfiguration configuration, ISender sender):base(configuration) => this.sender = sender;

        [HttpGet()]
        [Route("ObterPorFuncionario/{idFuncionario}")]
        public async Task<IActionResult> ObterOperacaoPorIdFuncionario(int idFuncionario)
        {
            try
            {
                var retorno = await sender.Send(new Dominio.Queries.OperacaoQuery { IdFuncionario = idFuncionario });
                if (retorno != null && retorno.Any())
                    return Ok(retorno);
                else
                    return BadRequest(new { message = "Nenhuma Operação encontrada!" });
            }
            catch (Exception ex)
            {
                return StatusCode(401,"Erro ao retornar operacao por Id Funcionario " + ex.Message);
            }
          

        }

        [HttpGet()]
        [Route("ObterPorPacote/{idPacote}")]
        public async Task<IActionResult> ObterOperacaoPorIdDoPacote(int idPacote)
        {
            try
            {
                var retorno = await sender.Send(new Dominio.Queries.NumeroDoPacoteQuery { NumeroDoPacote = idPacote });
                if (retorno != null && retorno .Any())
                return Ok(retorno);
                else
                    return BadRequest(new { message = "Nenhum Pacote encontrado!" });


            }
            catch (Exception ex)
            {
                return StatusCode(401, "Erro ao retornar operacao por Id do Pacote " + ex.Message);
            }


        }

        [HttpGet()]
        [Route("ObterTempoDeProducao/{idUsuario}")]
        public async Task<IActionResult> ObterTempoDeProducao(int idUsuario)
        {
            try
            {
                var retorno = await sender.Send(new Dominio.Queries.IdDoFuncionarioQuery {  idDoFuncionario = idUsuario });
               
                    return Ok(retorno?.Tempo);
              


            }
            catch (Exception ex)
            {
                return StatusCode(401, "Erro ao retornar ObterTempoDeProducao " + ex.Message);
            }


        }



        [HttpPost]
        public async Task<IActionResult> SalvarOperacao([FromBody] List<Dominio.Models.DTO.Operacao> operacoes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (operacoes.All(p=> !p.concluido ))
                        return BadRequest(new { message = "Operações não estao concluidas! Pois precisam ser concluídas" });

                    if (operacoes.All(p=> p.DataConclusao != null ))
                        return BadRequest(new { message = "Todas as Operações Já foram concluídas!" });

                    var operacoesConcluidas = operacoes.Where(p => p.concluido).ToList();
                    await sender.Send(new Dominio.Commands.SalvarOperacaoCommand(operacoesConcluidas));
                }
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao Salvar operacoes " + ex.Message });
            }
           
            
        }
    }
}

