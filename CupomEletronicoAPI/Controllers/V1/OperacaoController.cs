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
        private readonly ILogger<OperacaoController> _logger;

        public OperacaoController(IConfiguration configuration,
            ISender sender,
            ILogger<OperacaoController> logger) : base(configuration)
        {
            this.sender = sender;
            this.configuration = configuration;
            this._logger = logger;
        } 

        [HttpGet()]
        [Route("ObterPorFuncionario/{idFuncionario}")]
        public async Task<IActionResult> ObterOperacaoPorIdFuncionario(int idFuncionario)
        {
            try
            {
                
                _logger.LogInformation("[OperacaoController][ObterOperacaoPorIdFuncionario] Iniciando");
                _logger.LogInformation($"[OperacaoController][ObterOperacaoPorIdFuncionario] Buscando operacao por IdFuncionario: {idFuncionario}");
                var retorno = await sender.Send(new Dominio.Queries.OperacaoQuery { IdFuncionario = idFuncionario });

                if (retorno != null && retorno.Any())
                {
                    _logger.LogInformation($"[OperacaoController][ObterOperacaoPorIdFuncionario] Operacao por IdFuncionario: {idFuncionario} encontrada");
                    if (retorno.Any(p => !string.IsNullOrEmpty(p.QuebraManual)))
                    {
                        _logger.LogInformation($"[OperacaoController][ObterOperacaoPorIdFuncionario] Iniciando tratamento de quebra");
                        RetornarTratamentoComQuebra(retorno);
                        _logger.LogInformation($"[OperacaoController][ObterOperacaoPorIdFuncionario] tratamento de quebra realizado");
                    }

                    return Ok(retorno);
                }
                else
                {
                    _logger.LogWarning($"[OperacaoController][ObterOperacaoPorIdFuncionario] Operacao por IdFuncionario nao encontrado");
                    return Ok(new List<Dominio.Models.DTO.Operacao>());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[OperacaoController][ObterOperacaoPorIdFuncionario] erro:" + ex.Message);
                return BadRequest("Erro ao retornar operacao por Id Funcionario " + ex.Message);
            }
          

        }

        private  void RetornarTratamentoComQuebra(List<Dominio.Models.DTO.Operacao> retorno)
        {
            var valorQuebraManual = retorno.Where(p=> !string.IsNullOrEmpty(p.QuebraManual)).FirstOrDefault().QuebraManual;

            if (valorQuebraManual != null)
            {
                string[] vetQuebraManual = valorQuebraManual.Split(';');
                if (vetQuebraManual != null && vetQuebraManual.Any())
                {
                    
                    string quebraManual = "Q";
                    for (int i = 0; i < vetQuebraManual.Length; i++)
                    {
                       
                        foreach (var item in retorno)
                        {
                            
                            var index = retorno.IndexOf(item);
                            if (index >= Convert.ToInt32(vetQuebraManual[i]) - 1)
                            {
                                item.QuebraManual = quebraManual + (i + 1);
                                
                            }
                            else if (!item.QuebraManual.Contains('Q'))
                            {
                                item.QuebraManual = "";
                            }
                        }
                    }
                }
                
            }
            
        }

        [HttpGet()]
        [Route("ObterPorPacote/{idPacote}")]
        public async Task<IActionResult> ObterOperacaoPorIdDoPacote(int idPacote)
        {
            try
            {
                _logger.LogInformation("[OperacaoController][ObterOperacaoPorIdDoPacote] Iniciando");
                _logger.LogInformation(
                    $"[OperacaoController][ObterOperacaoPorIdDoPacote] Buscando operacao por Id do Pacote: {idPacote}");


                var retorno = await sender.Send(new Dominio.Queries.NumeroDoPacoteQuery { NumeroDoPacote = idPacote });
                if (retorno != null && retorno.Any())
                {
                    _logger.LogInformation(
                        $"[OperacaoController][ObterOperacaoPorIdDoPacote] operacao por Id do Pacote encontrado: {idPacote}");
                    if (retorno.Any(p => !string.IsNullOrEmpty(p.QuebraManual)))
                    {
                        _logger.LogInformation(
                            $"[OperacaoController][ObterOperacaoPorIdDoPacote] Iniciando tratamento de quebra");
                        RetornarTratamentoComQuebra(retorno);
                        _logger.LogInformation(
                            $"[OperacaoController][ObterOperacaoPorIdDoPacote] tratamento de quebra realizado");
                    }

                    return Ok(retorno);
                }
                else
                {
                    _logger.LogWarning(
                        $"[OperacaoController][ObterOperacaoPorIdDoPacote] operacao por Id do Pacote nao encontrado");
                    return Ok(new List<Dominio.Models.DTO.Operacao>());
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[OperacaoController][ObterOperacaoPorIdDoPacote] erro:" + ex.Message);
                return BadRequest("Erro ao retornar operacao por Id do Pacote " + ex.Message);
            }


        }

        [HttpGet()]
        [Route("ObterOperacoesPorPacote/{idPacote}")]
        public async Task<IActionResult> ObterOperacoesPorIdDoPacote(string idPacote)
        {
            
            _logger.LogInformation("[OperacaoController][ObterOperacoesPorIdDoPacote] Iniciando");
           
            
            var listaOperacoes = new List<Dominio.Models.DTO.Operacao>();
            try
            {
                if (!string.IsNullOrEmpty(idPacote))
                {
                    _logger.LogInformation(
                        $"[OperacaoController][ObterOperacoesPorIdDoPacote] Buscando operacoes por Id do Pacote: {idPacote}");
                    
                    var vetor = idPacote.Split(',');
                    if (vetor != null && vetor.Length > 0)
                    {
                        var vetorDistinto = vetor.Distinct();

                        if (vetorDistinto != null && vetorDistinto.Count() > 0)
                        {
                           
                            
                            foreach (var item in vetorDistinto)
                            {
                                
                                _logger.LogInformation(
                                    $"[OperacaoController][ObterOperacoesPorIdDoPacote] Buscando operacao por Id do Pacote: {item}");
                                
                                var retorno = await sender.Send(new Dominio.Queries.NumeroDoPacoteQuery { NumeroDoPacote = Convert.ToInt32(item.Trim()) });
                               
                                
                                if (retorno.Any(p => !string.IsNullOrEmpty(p.QuebraManual)))
                                {
                                    _logger.LogInformation(
                                        $"[OperacaoController][ObterOperacoesPorIdDoPacote]  operacao por Id do Pacote: {item} encontrado");
                                    
                                    _logger.LogInformation(
                                        $"[OperacaoController][ObterOperacoesPorIdDoPacote]  Iniciando tratamento de quebra");
                                    RetornarTratamentoComQuebra(retorno);
                                    _logger.LogInformation(
                                        $"[OperacaoController][ObterOperacoesPorIdDoPacote]  Finalizado tratamento de quebra");
                                }

                                if (retorno != null)
                                {
                                    listaOperacoes.AddRange(retorno);
                                    _logger.LogInformation(
                                        $"[OperacaoController][ObterOperacoesPorIdDoPacote] operacoes agrupadas");
                                }

                            }
                         
                        }
                    }
                }
                else
                {
                    _logger.LogWarning(
                        $"[OperacaoController][ObterOperacoesPorIdDoPacote] operacoes nao encontradas");
                    return Ok(new List<Dominio.Models.DTO.Operacao>());
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[OperacaoController][ObterOperacoesPorIdDoPacote] error:{ex.Message}");
                return BadRequest("Erro ao retornar operacao por Id do Pacote " + ex.Message);
            }
      
            return Ok(listaOperacoes);

        }

        [HttpGet()]
        [Route("ObterTempoDeProducao/{idUsuario}")]
        public async Task<IActionResult> ObterTempoDeProducao(int idUsuario)
        {
            try
            {
                _logger.LogInformation(
                    $"[OperacaoController][ObterTempoDeProducao] Iniciando");
                
                _logger.LogInformation(
                    $"[OperacaoController][ObterTempoDeProducao] Opter tempo de producao por idUsuario: {idUsuario} ");
                
                var retorno = await sender.Send(new Dominio.Queries.IdDoFuncionarioQuery {  idDoFuncionario = idUsuario });

                if (retorno == null)
                {
                    _logger.LogWarning(
                        $"[OperacaoController][ObterTempoDeProducao] tempo de producao por idUsuario: {idUsuario} nao encontrado");
                    return Ok(0);

                }
                else
                    return Ok(retorno?.Tempo);
              


            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"[OperacaoController][ObterTempoDeProducao] Error:{ex.Message}");
                return BadRequest("Erro ao retornar ObterTempoDeProducao " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SalvarOperacao([FromBody] List<Dominio.Models.DTO.Operacao> operacoes)
        {
            try
            {
                _logger.LogInformation(
                    $"[OperacaoController][SalvarOperacao] Iniciando");
                
                if (ModelState.IsValid)
                {
                    if (operacoes.All(p=> !p.concluido ))
                        return BadRequest(new { message = "Nenhuma operação foi selecionada!" });

                    if (operacoes.All(p=> p.DataConclusao != null ))
                        return BadRequest(new { message = "Todas as Operações Já foram concluídas!" });

                    var operacoesConcluidas = operacoes.Where(p => p.concluido).ToList();
                    
                    _logger.LogInformation(
                        $"[OperacaoController][SalvarOperacao] Salvando");
                    await sender.Send(new Dominio.Commands.SalvarOperacaoCommand(operacoesConcluidas));
                    
                    _logger.LogInformation(
                        $"[OperacaoController][SalvarOperacao] Salvo");
                }
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"[OperacaoController][SalvarOperacao] Error:{ex.Message}");    
                
                return BadRequest(new { message = "Erro ao Salvar operacoes " + ex.Message });
            }
        }
    }
}

