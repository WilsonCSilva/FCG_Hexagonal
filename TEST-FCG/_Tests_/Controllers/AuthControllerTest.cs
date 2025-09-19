using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FCG.Controllers;
using Core.Interfaces;
using Domain.Models;
using FCG.Middlewares;
using Infra.Helpers;

namespace TEST_FCG._Tests_.Controllers
{
    [TestClass]
    public class AuthControllerTest
    {
        private Mock<IConfiguration> _configMock = null!;
        private Mock<IUsuarioRepository> _usuarioRepoMock = null!;
        private Mock<CriptografiaHelper> _criptografiaHelperMock = null!;
        private Mock<LoginHelper> _loginHelperMock = null!;
        private Mock<BaseLogger<AuthController>> _loggerMock = null!;
        private AuthController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _configMock = new Mock<IConfiguration>();
            _usuarioRepoMock = new Mock<IUsuarioRepository>();
            _criptografiaHelperMock = new Mock<CriptografiaHelper>();
            _loginHelperMock = new Mock<LoginHelper>();
            _loggerMock = new Mock<BaseLogger<AuthController>>(MockBehavior.Loose, new object[] { null! });

            _configMock.Setup(c => c["Jwt:ExpirationMinutes"]).Returns("60");
            _configMock.Setup(c => c["Jwt:Key"]).Returns("supersecretkey12345678901234567890");
            _configMock.Setup(c => c["Jwt:Issuer"]).Returns("issuer");
            _configMock.Setup(c => c["Jwt:Audience"]).Returns("audience");

            _controller = new AuthController(
                _configMock.Object,
                _usuarioRepoMock.Object,
                _criptografiaHelperMock.Object,
                _loginHelperMock.Object,
                _loggerMock.Object
            );
        }

        [TestMethod]
        public void ReturnsUnauthorizedWhenUserNotFound()
        {
            _usuarioRepoMock.Setup(r => r.ObterPorNome("notfound")).Returns((Usuario?)null);

            var result = _controller.Logar("notfound", "any@email.com", "any");

            var unauthorized = result as UnauthorizedObjectResult;
            Assert.IsNotNull(unauthorized);
            Assert.IsNotNull(unauthorized.Value);
            Assert.IsInstanceOfType(unauthorized.Value, typeof(ErroResponse));
            var error = unauthorized.Value as ErroResponse;
            Assert.IsNotNull(error);
            Assert.AreEqual("Unauthorized", error.Erro);
            Assert.AreEqual("Usuário ou senha inválidos.", error.Detalhe);
        }

        [TestMethod]
        public void ReturnsBadRequestIfExceptionWhenLogin()
        {
            _usuarioRepoMock.Setup(r => r.ObterPorNome(It.IsAny<string>())).Throws(new Exception("fail"));

            try
            {
                var result = _controller.Logar("user", "email", "senha");
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("fail", ex.Message);
            }
        }
    }
}
