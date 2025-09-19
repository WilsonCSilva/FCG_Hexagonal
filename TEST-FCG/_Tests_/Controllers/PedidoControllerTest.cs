using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FCG.Controllers;
using Core.Interfaces;
using Domain.Models;
using Core.DTOs;
using Core.Inputs;
using FCG.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace TEST_FCG._Tests_.Controllers
{
    [TestClass]
    public class PedidoControllerTest
    {
        private Mock<IPedidoRepository> _pedidoRepoMock = null!;
        private Mock<BaseLogger<GameController>> _loggerMock = null!;
        private PedidoController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _pedidoRepoMock = new Mock<IPedidoRepository>();
            _loggerMock = new Mock<BaseLogger<GameController>>(MockBehavior.Loose, new object[] { null! });
            _controller = new PedidoController(_pedidoRepoMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public void ReturnsOkWhenGettingAllPedidos()
        {
            var pedidos = new List<Pedido>
            {
                new Pedido { Id = 1, UsuarioId = 10, GameId = 20, DataCriacao = DateTime.Today }
            };
            _pedidoRepoMock.Setup(r => r.ObterTodos()).Returns(pedidos);

            var result = _controller.Get();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult!.Value, typeof(List<PedidoDto>));
            var value = okResult.Value as List<PedidoDto>;
            Assert.IsNotNull(value);
            Assert.AreEqual(1, value!.Count);
        }

        [TestMethod]
        public void ReturnsOkWhenPedidoFoundById()
        {
            var pedido = new Pedido { Id = 2, UsuarioId = 11, GameId = 21, DataCriacao = DateTime.Today };
            _pedidoRepoMock.Setup(r => r.ObterPorID(2)).Returns(pedido);

            var result = _controller.Get(2);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult!.Value, typeof(PedidoDto));
            var dto = okResult.Value as PedidoDto;
            Assert.IsNotNull(dto);
            Assert.AreEqual(2, dto!.Id);
        }

        [TestMethod]
        public void ReturnsOkWhenPostingNewPedido()
        {
            var input = new PedidoInput
            {
                UsuarioId = 12,
                GameId = 22
            };

            _pedidoRepoMock.Setup(r => r.Cadastrar(It.IsAny<Pedido>()));

            var result = _controller.Post(input);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult!.Value);
            Assert.IsTrue(okResult.Value!.ToString()!.Contains("cadastrado com sucesso"));
        }

        [TestMethod]
        public void ReturnsOkWhenUpdatingPedido()
        {
            var updateInput = new PedidoUpdateInput
            {
                Id = 14,
                GameId = 24
            };

            var pedido = new Pedido
            {
                Id = 14,
                UsuarioId = 15,
                GameId = 99,
                DataCriacao = DateTime.Today
            };

            _pedidoRepoMock.Setup(r => r.ObterPorID(14)).Returns(pedido);
            _pedidoRepoMock.Setup(r => r.Alterar(It.IsAny<Pedido>()));

            var result = _controller.Put(updateInput);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult!.Value);
            Assert.IsTrue(okResult.Value!.ToString()!.Contains("alterado com sucesso"));
        }

        [TestMethod]
        public void ReturnsOkWhenDeletingPedido()
        {
            _pedidoRepoMock.Setup(r => r.Deletar(16));

            var result = _controller.Delete(16);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult!.Value);
            Assert.IsTrue(okResult.Value!.ToString()!.Contains("deletado com sucesso"));
        }
    }
}
