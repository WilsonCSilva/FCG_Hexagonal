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
    public class GameController(IGameRepository gameRepository, BaseLogger<GameController> Logger) : ControllerBase
    {
        private readonly IGameRepository _gameRepository = gameRepository;
        private readonly BaseLogger<GameController> _logger = Logger;

        #region [Get]

        [HttpGet("cadastro-todos")]
        public IActionResult get()
        {
            try
            {
                var _gamesDto = new List<GameDto>();
                var _games = _gameRepository.ObterTodos();

                foreach (var game in _games)
                {
                    _gamesDto.Add(new GameDto()
                    {
                        Id = game.Id,
                        Nome = game.Nome,
                        Produtora = game.Produtora,
                        Descricao = game.Descricao,
                        DataLancamento = game.DataLancamento.ToString("yyyy-MM-dd"),
                        Preco = game.Preco.ToString("F2"),
                    });
                }
                _logger.LogInformation("Games exibidos com sucesso.");
                return Ok(_gamesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{nome}")]
        public IActionResult Get([FromRoute] string nome)
        {
            try
            {
                var _game = _gameRepository.ObterPorNome(nome);
                if (_game == null)
                {
                    var erroResponse = "Game não encontrado.";
                    _logger.LogError(erroResponse.ToString());
                    return NotFound(erroResponse);
                }
                
                var _gameDto = new GameDto()
                {
                    Id = _game.Id,
                    Nome = _game.Nome,
                    Produtora = _game.Produtora,
                    Descricao = _game.Descricao,
                    DataLancamento = _game.DataLancamento.ToString("yyyy-MM-dd"),
                    Preco = _game.Preco.ToString("F2"),
                };
                _logger.LogInformation($"Game {_game.Nome} exibido com sucesso.");
                return Ok(_gameDto);
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
                var _game = _gameRepository.ObterPorID(id);
                if (_game == null)
                {
                    var erroResponse = "Game não encontrado.";
                    _logger.LogError(erroResponse.ToString());
                    return NotFound(erroResponse);
                }
                
                var _gameDto = new GameDto()
                {
                    Id = _game.Id,
                    Nome = _game.Nome,
                    Produtora = _game.Produtora,
                    Descricao = _game.Descricao,
                    DataLancamento = _game.DataLancamento.ToString("yyyy-MM-dd"),
                    Preco = _game.Preco.ToString("F2"),
                };
                _logger.LogInformation($"Game {_game.Nome} exibido com sucesso.");
                return Ok(_gameDto);
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
        [Authorize(Policy = "Admin")]
        public IActionResult Post([FromBody] GameInput gameInput)
        {
            try
            {
                var _game = new Game()
                {
                    Nome = gameInput.Nome,
                    Produtora = gameInput.Produtora,
                    Descricao = gameInput.Descricao,
                    DataLancamento = gameInput.DataLancamento,
                    Preco = gameInput.Preco
                };
                _gameRepository.Cadastrar(_game);

                string okResponse = $"Game {_game.Id} cadastrado com sucesso.";
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
        [Authorize(Policy = "Admin")]
        public IActionResult CadastroEmMassa()
        {
            try
            {
                _gameRepository.CadastrarEmMassa();

                string okResponse = $"Games cadastrados com sucesso.";
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
        [Authorize(Policy = "Admin")]
        public IActionResult Put([FromBody] GameUpdateInput gameUpdateInput)
        {
            try
            {
                var _game = _gameRepository.ObterPorID(gameUpdateInput.Id);
                {
                    _game.Nome = gameUpdateInput.Nome;
                    _game.Produtora = gameUpdateInput.Produtora;
                    _game.Descricao = gameUpdateInput.Descricao;
                    _game.DataLancamento = gameUpdateInput.DataLancamento;
                    _game.Preco = gameUpdateInput.Preco;
                };
                _gameRepository.Alterar(_game);

                string okResponse = $"Game {_game.Id} alterado com sucesso.";
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
                _gameRepository.Deletar(Id);

                string okResponse = $"Game {Id} deletado com sucesso.";
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
