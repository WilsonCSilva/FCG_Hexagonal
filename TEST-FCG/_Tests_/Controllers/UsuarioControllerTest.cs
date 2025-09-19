using Moq;
using FCG.Controllers;
using Core.Interfaces;
using Domain.Models;
using Core.DTOs;
using FCG.Middlewares;
using Infra.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace TEST_FCG._Tests_.Controllers
{
    [TestClass]
    public class UsuarioControllerTest
    {
        private Mock<IUsuarioRepository> _usuarioRepoMock = null!;
        private Mock<CriptografiaHelper> _criptografiaHelperMock = null!;
        private Mock<TextoHelper> _textoHelperMock = null!;
        private Mock<BaseLogger<UsuarioController>> _loggerMock = null!;
        private UsuarioController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _usuarioRepoMock = new Mock<IUsuarioRepository>();
            _criptografiaHelperMock = new Mock<CriptografiaHelper>();
            _textoHelperMock = new Mock<TextoHelper>();
            _loggerMock = new Mock<BaseLogger<UsuarioController>>(MockBehavior.Loose, new object[] { Mock.Of<Microsoft.Extensions.Logging.ILogger<UsuarioController>>() });
            _controller = new UsuarioController(
                _usuarioRepoMock.Object,
                _criptografiaHelperMock.Object,
                _textoHelperMock.Object,
                _loggerMock.Object
            );
        }

        [TestMethod]
        public void ReturnsOkWhenGettingAllUsuarios()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Nome = "User1", Email = "user1@email.com", Senha = "senha", DataNascimento = DateTime.Today, TipoUsuario = 1 }
            };
            _usuarioRepoMock.Setup(r => r.ObterTodos()).Returns(usuarios);

            var result = _controller.Get();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<UsuarioDto>));
            var value = okResult.Value as List<UsuarioDto>;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value.Count);
        }

        [TestMethod]
        public void ReturnsOkWhenUsuarioFoundById()
        {
            var usuario = new Usuario { Id = 4, Nome = "User4", Email = "user4@email.com", Senha = "senha", DataNascimento = DateTime.Today, TipoUsuario = 1 };
            _usuarioRepoMock.Setup(r => r.ObterPorID(4)).Returns(usuario);

            var result = _controller.Get(4);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(UsuarioDto));
            var dto = okResult.Value as UsuarioDto;
            Assert.IsNotNull(dto);
            Assert.AreEqual(4, dto.Id);
        }

        [TestMethod]
        public void ReturnsOkWhenDeletingUsuario()
        {
            _usuarioRepoMock.Setup(r => r.Deletar(15));

            var result = _controller.Delete(15);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Value);
            Assert.IsTrue(okResult.Value.ToString()!.Contains("excluído com sucesso"));
        }
    }
}
