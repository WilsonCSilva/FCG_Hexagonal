using Core.DTOs;
using Core.Inputs;
using Core.Interfaces;
using FCG.Middlewares;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController(IPedidoRepository pedidoRepository, BaseLogger<GameController> Logger) : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly BaseLogger<GameController> _logger = Logger;

        #region [Get]

        [HttpGet("todos")]
        [Authorize(Policy = "Admin")]
        public IActionResult Get()
        {
            try
            {
                var _pedidosDto = new List<PedidoDto>();
                var _pedidos = _pedidoRepository.ObterTodos();

                foreach (var pedido in _pedidos)
                {
                    _pedidosDto.Add(new PedidoDto()
                    {
                        Id = pedido.Id,
                        DataCriacao = pedido.DataCriacao.ToString("yyyy-MM-dd"),
                        UsuarioId = pedido.UsuarioId,
                        GameId = pedido.GameId,
                    });
                }
                _logger.LogInformation("Todos os pedidos exibidos com sucesso.");
                return Ok(_pedidosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                var _pedido = _pedidoRepository.ObterPorID(id);
                if (_pedido == null)
                {
                    var erroResponse = "Pedido não encontrado.";
                    _logger.LogError(erroResponse.ToString());
                    return NotFound(erroResponse);
                }

                var _pedidoDto = new PedidoDto()
                {
                    Id = _pedido.Id,
                    DataCriacao = _pedido.DataCriacao.ToString("yyyy-MM-dd"),
                    UsuarioId = _pedido.UsuarioId,
                    GameId = _pedido.GameId,
                };

                _logger.LogInformation($"Pedido exibido com sucesso.");
                return Ok(_pedidoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region [Post]

        [HttpPost]
        public IActionResult Post([FromBody] PedidoInput pedidoInput)
        {
            try
            {
                var _pedido = new Pedido()
                {
                    UsuarioId = pedidoInput.UsuarioId,
                    GameId = pedidoInput.GameId,
                };
                _pedidoRepository.Cadastrar(_pedido);

                string okResponse = $"Pedido {_pedido.Id} cadastrado com sucesso.";
                _logger.LogInformation(okResponse);
                return Ok(okResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Cadastro-em-massa")]
        public IActionResult CadastroEmMassa()
        {
            try
            {
                _pedidoRepository.CadastrarEmMassa();

                string okResponse = $"Pedidos cadastrados com sucesso.";
                _logger.LogInformation(okResponse);
                return Ok(okResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region [Put Delete]

        [HttpPut]
        public IActionResult Put([FromBody] PedidoUpdateInput pedidoUpdateInput)
        {
            try
            {
                var _pedido = _pedidoRepository.ObterPorID(pedidoUpdateInput.Id);
                {
                    if (_pedido == null)
                    {
                        var erroResponse = "Pedido não encontrado.";
                        _logger.LogError(erroResponse.ToString());
                        return NotFound(erroResponse);
                    }
                    else
                    {
                        _pedido.GameId = pedidoUpdateInput.GameId;
                    }
                };
                _pedidoRepository.Alterar(_pedido);

                string okResponse = $"Game {_pedido.Id} alterado com sucesso.";
                _logger.LogInformation(okResponse);
                return Ok(okResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "Admin")]
        [Route("{Id:int}")]
        public IActionResult Delete([FromRoute] int Id)
        {
            try
            {
                _pedidoRepository.Deletar(Id);

                string okResponse = $"Pedido {Id} deletado com sucesso.";
                _logger.LogInformation(okResponse);
                return Ok(okResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
