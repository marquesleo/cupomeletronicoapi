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

        [HttpGet("{idFuncionario:int}")]
        public async Task<IActionResult> ObterOperacaoPorIdFuncionario(int idFuncionario)
        {
            try
            {
                var retorno = await sender.Send(new Dominio.Queries.OperacaoQuery { IdFuncionario = idFuncionario });
                return Ok(retorno);
               
            }
            catch (Exception ex)
            {
                return StatusCode(401,"Erro ao retornar operacao por Id Funcionario " + ex.Message);
            }
          

        }


        [HttpPost]
        public async Task<IActionResult> SalvarOperacao([FromBody] List<Dominio.Models.DTO.Operacao> operacoes)
        {
            try
            {
                await sender.Send(new Dominio.Commands.SalvarOperacaoCommand(operacoes));
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao Salvar operacoes " + ex.Message);
            }
           
            
        }
    }
}

