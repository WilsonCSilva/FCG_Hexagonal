using Xunit;
using Moq;
using FCG.Controllers;
using FCG.Interfaces;
using FCG.Helpers;
using FCG.Inputs;
using FCG.Models;
using Microsoft.AspNetCore.Mvc;
using FCG.DTOs;
using FCG.Middlewares;
using Microsoft.AspNetCore.Http;
using System;

namespace FCG.Tests.Controllers
{
    public class UsuarioControllerTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepoMock = new();
        private readonly Mock<CriptografiaHelper> _criptografiaHelperMock = new();
        private readonly Mock<TextoHelper> _textoHelperMock = new();
        private readonly Mock<BaseLogger<UsuarioController>> _loggerMock = new();

        private UsuarioController CriarController() =>
            new UsuarioController(
                _usuarioRepoMock.Object,
                _criptografiaHelperMock.Object,
                _textoHelperMock.Object,
                _loggerMock.Object
            );

        //[Fact]
        //public void Post_DeveRetornarConflict_QuandoNomeJaExistir()
        //{
        //    var input = new UsuarioInput { Nome = "Joao" };
        //    _usuarioRepoMock.Setup(r => r.ObterPorNome("Joao")).Returns(new Usuario());

        //    var controller = CriarController();
        //    var result = controller.Post(input) as ObjectResult;

        //    Assert.Equal(StatusCodes.Status409Conflict, result?.StatusCode);
        //}

        //[Fact]
        //public void Post_DeveRetornarConflict_QuandoEmailJaExistir()
        //{
        //    var input = new UsuarioInput { Nome = "Joao", Email = "joao@email.com" };
        //    _usuarioRepoMock.Setup(r => r.ObterPorNome("Joao")).Returns((Usuario)null);
        //    _usuarioRepoMock.Setup(r => r.ObterPorEmail("joao@email.com")).Returns(new Usuario());

        //    var controller = CriarController();
        //    var result = controller.Post(input) as ObjectResult;

        //    Assert.Equal(StatusCodes.Status409Conflict, result?.StatusCode);
        //}

        [Fact]
        public void Post_DeveRetornarBadRequest_QuandoEmailInvalido()
        {
            var input = new UsuarioInput { Nome = "Joao", Email = "email", Senha = "123456", TipoUsuario = 1 };
            _usuarioRepoMock.Setup(r => r.ObterPorNome("Joao")).Returns((Usuario)null);
            _usuarioRepoMock.Setup(r => r.ObterPorEmail("email")).Returns((Usuario)null);

            _criptografiaHelperMock.Setup(c => c.Criptografar("123456")).Returns("senhaCripto");
            _criptografiaHelperMock.Setup(c => c.Descriptografar("senhaCripto")).Returns("123456");

            _textoHelperMock.Setup(t => t.EmailValido("email")).Returns(false);

            var controller = CriarController();
            var result = controller.Post(input) as BadRequestObjectResult;

            Assert.Equal(StatusCodes.Status400BadRequest, result?.StatusCode);
            Assert.Equal("Email inválido.", result?.Value);
        }

        //[Fact]
        //public void Post_DeveRetornarBadRequest_QuandoSenhaInvalida()
        //{
        //    var input = new UsuarioInput { Nome = "Joao", Email = "joao@email.com", Senha = "123", TipoUsuario = "Admin" };
        //    _usuarioRepoMock.Setup(r => r.ObterPorNome("Joao")).Returns((Usuario)null);
        //    _usuarioRepoMock.Setup(r => r.ObterPorEmail("joao@email.com")).Returns((Usuario)null);

        //    _criptografiaHelperMock.Setup(c => c.Criptografar("123")).Returns("cripto123");
        //    _criptografiaHelperMock.Setup(c => c.Descriptografar("cripto123")).Returns("123");

        //    _textoHelperMock.Setup(t => t.EmailValido("joao@email.com")).Returns(true);
        //    _textoHelperMock.Setup(t => t.SenhaValida("123")).Returns(false);

        //    var controller = CriarController();
        //    var result = controller.Post(input) as BadRequestObjectResult;

        //    Assert.Equal(StatusCodes.Status400BadRequest, result?.StatusCode);
        //    Assert.Equal("Senha inválida.", result?.Value);
        //}

        //[Fact]
        //public void Post_DeveRetornarBadRequest_QuandoTipoUsuarioInvalido()
        //{
        //    var input = new UsuarioInput { Nome = "Joao", Email = "joao@email.com", Senha = "123456", TipoUsuario = "Invalido" };
        //    _usuarioRepoMock.Setup(r => r.ObterPorNome("Joao")).Returns((Usuario)null);
        //    _usuarioRepoMock.Setup(r => r.ObterPorEmail("joao@email.com")).Returns((Usuario)null);

        //    _criptografiaHelperMock.Setup(c => c.Criptografar("123456")).Returns("cripto");
        //    _criptografiaHelperMock.Setup(c => c.Descriptografar("cripto")).Returns("123456");

        //    _textoHelperMock.Setup(t => t.EmailValido("joao@email.com")).Returns(true);
        //    _textoHelperMock.Setup(t => t.SenhaValida("123456")).Returns(true);
        //    _textoHelperMock.Setup(t => t.TipoUsuarioValido("Invalido")).Returns(false);

        //    var controller = CriarController();
        //    var result = controller.Post(input) as BadRequestObjectResult;

        //    Assert.Equal(StatusCodes.Status400BadRequest, result?.StatusCode);
        //    Assert.Equal("Tipo de usuário inválida.", result?.Value);
        //}

        //[Fact]
        //public void Post_DeveRetornarOk_QuandoCadastroForValido()
        //{
        //    var input = new UsuarioInput
        //    {
        //        Nome = "Joao",
        //        Email = "joao@email.com",
        //        Senha = "123456",
        //        DataNascimento = DateTime.Today,
        //        Endereco = "Rua A",
        //        TipoUsuario = "Admin"
        //    };

        //    _usuarioRepoMock.Setup(r => r.ObterPorNome(input.Nome)).Returns((Usuario)null);
        //    _usuarioRepoMock.Setup(r => r.ObterPorEmail(input.Email)).Returns((Usuario)null);

        //    _criptografiaHelperMock.Setup(c => c.Criptografar("123456")).Returns("cripto");
        //    _criptografiaHelperMock.Setup(c => c.Descriptografar("cripto")).Returns("123456");

        //    _textoHelperMock.Setup(t => t.EmailValido(input.Email)).Returns(true);
        //    _textoHelperMock.Setup(t => t.SenhaValida("123456")).Returns(true);
        //    _textoHelperMock.Setup(t => t.TipoUsuarioValido(input.TipoUsuario)).Returns(true);

        //    _usuarioRepoMock.Setup(r => r.Cadastrar(It.IsAny<Usuario>()))
        //                    .Callback<Usuario>(u => u.Id = 123);

        //    var controller = CriarController();
        //    var result = controller.Post(input) as OkObjectResult;

        //    Assert.Equal(StatusCodes.Status200OK, result?.StatusCode);
        //    Assert.Equal("Usuário 123 cadastrado com sucesso.", result?.Value);
        //}
    }
}
