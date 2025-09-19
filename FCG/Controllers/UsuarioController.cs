using Infra.Helpers;
using FCG.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.DTOs;
using Core.Inputs;
using Domain.Models;

namespace FCG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController(IUsuarioRepository usuarioRepository, CriptografiaHelper criptografiaHelper, TextoHelper textoHelper, BaseLogger<UsuarioController> logger) : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly CriptografiaHelper _criptografiaHelper = criptografiaHelper;
        private readonly TextoHelper _textoHelper = textoHelper;
        private readonly BaseLogger<UsuarioController> _logger = logger;

        #region [Get]

        [HttpGet("cadastro-todos")]
        [Authorize(Policy = "Admin")]
        public IActionResult Get()
        {
            try
            {
                var _usuariosDto = new List<UsuarioDto>();
                var _usuarios = _usuarioRepository.ObterTodos();

                foreach (var usuario in _usuarios)
                {
                    _usuariosDto.Add(new UsuarioDto()
                    {
                        Id = usuario.Id,
                        Nome = usuario.Nome,
                        Email = usuario.Email,
                        Senha = usuario.Senha,
                        DataNascimento = usuario.DataNascimento.ToString("yyyy-MM-dd"),
                        TipoUsuario = usuario.TipoUsuario
                    });
                }
                _logger.LogInformation("Todos os usuários exibidos com sucesso.");
                return Ok(_usuariosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pedidos-todos")]
        [Authorize(Policy = "Admin")]
        public IActionResult ObterPedidosTodos([FromQuery] int id)
        {
            try
            {
                var _usuarios = _usuarioRepository.ObterPorID(id);

                if (_usuarios == null)
                {
                    var erroResponse = "Não há pedido para o usuário.";
                    _logger.LogError(erroResponse.ToString());
                    return NotFound(erroResponse);
                }
                _logger.LogInformation("Todos os pedidos exibidos.");
                return Ok(_usuarioRepository.ObterPedidosTodos(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pedidos-seis-meses")]
        [Authorize(Policy = "Admin")]
        public IActionResult ObterPedidosSeisMeses([FromQuery] int id)
        {
            try
            {
                var _usuarios = _usuarioRepository.ObterPorID(id);
                if (_usuarios == null)
                {
                    var erroResponse = "Não há pedido para o usuário.";
                    _logger.LogError(erroResponse.ToString());
                    return NotFound(erroResponse);
                }
                _logger.LogInformation("Pedidos dos últimos seis meses exibidos.");
                return Ok(_usuarioRepository.ObterPedidosSeisMeses(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        [Route("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            try
            {
                var _usuarios = _usuarioRepository.ObterPorID(id);

                if (_usuarios == null)
                {
                    var erroResponse = "Usuário não encontrado.";
                    _logger.LogError(erroResponse.ToString());
                    return NotFound(erroResponse);
                }

                var _usuariosDto = new UsuarioDto()
                {
                    Id = _usuarios.Id,
                    Nome = _usuarios.Nome,
                    Email = _usuarios.Email,
                    DataNascimento = _usuarios.DataNascimento.ToString("yyyy-MM-dd"),
                    TipoUsuario = _usuarios.TipoUsuario
                };
                _logger.LogInformation($"Usuário {_usuarios.Nome} exibido com sucesso.");
                return Ok(_usuariosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        [Route("{nome}")]
        public IActionResult Get([FromRoute] string nome)
        {
            try
            {
                var _usuarios = _usuarioRepository.ObterPorNome(nome);
                if (_usuarios == null)
                {
                    var erroResponse = "Usuário não encontrado.";
                    _logger.LogError(erroResponse.ToString());
                    return NotFound(erroResponse);
                }

                var _usuariosDto = new UsuarioDto()
                {
                    Id = _usuarios.Id,
                    Nome = _usuarios.Nome,
                    Email = _usuarios.Email,
                    DataNascimento = _usuarios.DataNascimento.ToString("yyyy-MM-dd"),
                    TipoUsuario = _usuarios.TipoUsuario
                };
                _logger.LogInformation($"Usuário {_usuarios.Nome} exibido com sucesso.");
                return Ok(_usuariosDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region [Post]

        [HttpPost()]
        public IActionResult Post([FromBody] UsuarioInput usuarioInput)
        {
            try
            {
                var _usuarioTesteNome = _usuarioRepository.ObterPorNome(usuarioInput.Nome);
                if (_usuarioTesteNome != null)
                {
                    var erroResponse = "Usuário já cadastrado.";
                    _logger.LogError(erroResponse.ToString());
                    return Conflict(erroResponse);
                }

                var _usuarioTesteEmail = _usuarioRepository.ObterPorEmail(usuarioInput.Email);
                if (_usuarioTesteEmail != null)
                {
                    var erroResponse = "Email já cadastrado.";
                    _logger.LogError(erroResponse.ToString());
                    return Conflict(erroResponse);
                }

                var _usuario = new Usuario()
                {
                    Nome = usuarioInput.Nome,
                    Email = usuarioInput.Email,
                    Senha = _criptografiaHelper.Criptografar(usuarioInput.Senha),
                    DataNascimento = usuarioInput.DataNascimento,
                    Endereco = usuarioInput.Endereco,
                    TipoUsuario = usuarioInput.TipoUsuario
                };

                bool emailValido = _textoHelper.EmailValido(_usuario.Email);
                bool senhaValida = _textoHelper.SenhaValida(_criptografiaHelper.Descriptografar(_usuario.Senha));
                bool tipoUsuarioValido = _textoHelper.TipoUsuarioValido(_usuario.TipoUsuario);

                if (!emailValido)
                {
                    string erroResponse = "Email inválido.";
                    _logger.LogError(erroResponse);
                    return BadRequest(erroResponse);
                }
                else if (!senhaValida)
                {
                    string erroResponse = "Senha inválida: deve conter no mínimo 8 caracteres, letra maiúscula, número e caratcter especial.";
                    _logger.LogError(erroResponse);
                    return BadRequest(erroResponse);
                }
                else if (!tipoUsuarioValido)
                {
                    string erroResponse = "Tipo de usuário inválida.";
                    _logger.LogError(erroResponse);
                    return BadRequest(erroResponse);
                }
                else
                {
                    _usuarioRepository.Cadastrar(_usuario);

                    string okResponse = $"Usuário {_usuario.Id} cadastrado com sucesso.";
                    _logger.LogInformation(okResponse);
                    return Ok(okResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("cadastro-em-massa")]
        public IActionResult CadastroEmMassa()
        {
            try
            {
                _usuarioRepository.CadastrarEmMassa();

                string okResponse = $"Usuários cadastrados com sucesso.";
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

        #region [Put Detete]

        [HttpPut]
        [Authorize(Policy = "Admin")]
        public IActionResult Put([FromBody] UsuarioUpdateInput usuarioInput)
        {
            try
            {
                var _usuario = _usuarioRepository.ObterPorID(usuarioInput.Id);
                {
                    _usuario.Nome = usuarioInput.Nome;
                    _usuario.Email = usuarioInput.Email;
                    _usuario.Senha = _criptografiaHelper.Criptografar(usuarioInput.Senha);
                    _usuario.DataNascimento = usuarioInput.DataNascimento;
                    _usuario.TipoUsuario = usuarioInput.TipoUsuario;
                }
                ;

                bool emailValido = _textoHelper.EmailValido(_usuario.Email);
                bool senhaValida = _textoHelper.SenhaValida(_criptografiaHelper.Descriptografar(_usuario.Senha));
                bool tipoUsuarioValido = _textoHelper.TipoUsuarioValido(_usuario.TipoUsuario);

                if (!emailValido)
                {
                    string erroResponse = "Email inválido.";
                    _logger.LogError(erroResponse);
                    return BadRequest(erroResponse);
                }
                else if (!senhaValida)
                {
                    string erroResponse = "Senha inválida.";
                    _logger.LogError(erroResponse);
                    return BadRequest(erroResponse);
                }
                else if (!tipoUsuarioValido)
                {
                    string erroResponse = "Tipo de usuário inválida.";
                    _logger.LogError(erroResponse);
                    return BadRequest(erroResponse);
                }
                else
                {
                    _usuarioRepository.Alterar(_usuario);

                    string okResponse = $"Usuário {_usuario.Id} alterado com sucesso.";
                    _logger.LogInformation(okResponse);
                    return Ok(okResponse);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "Admin")]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);

                string okResponse = $"Usuário {id} excluído com sucesso.";
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
