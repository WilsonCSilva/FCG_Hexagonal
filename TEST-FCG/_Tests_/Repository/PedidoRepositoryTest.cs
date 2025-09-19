using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TEST_FCG._Tests_.Repository
{
    [TestClass]
    public class PedidoRepositoryTest
    {
        private Mock<IPedidoRepository> _pedidoRepoMock;
        private List<Pedido> _pedidos;

        [TestInitialize]
        public void Setup()
        {
            _pedidos = new List<Pedido>
            {
                new Pedido { Id = 1, UsuarioId = 1, GameId = 1 },
                new Pedido { Id = 2, UsuarioId = 2, GameId = 2 }
            };

            _pedidoRepoMock = new Mock<IPedidoRepository>();
        }

        [TestMethod]
        public void ObterTodosReturnsAllPedidos()
        {
            _pedidoRepoMock.Setup(r => r.ObterTodos()).Returns(_pedidos);

            var result = _pedidoRepoMock.Object.ObterTodos();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(p => p.UsuarioId == 1));
            Assert.IsTrue(result.Any(p => p.UsuarioId == 2));
        }

        [TestMethod]
        public void ObterPorIDReturnsCorrectPedido()
        {
            _pedidoRepoMock.Setup(r => r.ObterPorID(1)).Returns(_pedidos[0]);

            var result = _pedidoRepoMock.Object.ObterPorID(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.UsuarioId);
        }

        [TestMethod]
        public void ObterPorIDReturnsNullIfNotFound()
        {
            _pedidoRepoMock.Setup(r => r.ObterPorID(99)).Returns((Pedido)null);

            var result = _pedidoRepoMock.Object.ObterPorID(99);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ObterPorNomeReturnsCorrectPedido()
        {
            _pedidos[1].Nome = "PedidoCamila";
            _pedidoRepoMock.Setup(r => r.ObterPorNome("PedidoCamila")).Returns(_pedidos[1]);

            var result = _pedidoRepoMock.Object.ObterPorNome("PedidoCamila");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Id);
        }

        [TestMethod]
        public void ObterPorNomeReturnsNullIfNotFound()
        {
            _pedidoRepoMock.Setup(r => r.ObterPorNome("NotFound")).Returns((Pedido)null);

            var result = _pedidoRepoMock.Object.ObterPorNome("NotFound");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ObterPorEmailReturnsCorrectPedido()
        {
            var pedidoWithEmail = new Pedido { Id = 3, UsuarioId = 3, GameId = 3 };
            typeof(Pedido).GetProperty("Email")?.SetValue(pedidoWithEmail, "pedido@email.com");
            _pedidoRepoMock.Setup(r => r.ObterPorEmail("pedido@email.com")).Returns(pedidoWithEmail);

            var result = _pedidoRepoMock.Object.ObterPorEmail("pedido@email.com");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Id);
        }

        [TestMethod]
        public void ObterPorEmailReturnsNullIfNotFound()
        {
            _pedidoRepoMock.Setup(r => r.ObterPorEmail("notfound@email.com")).Returns((Pedido)null);

            var result = _pedidoRepoMock.Object.ObterPorEmail("notfound@email.com");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Cadastrar_AddsPedido()
        {
            var newPedido = new Pedido { Id = 4, UsuarioId = 4, GameId = 4 };

            _pedidoRepoMock.Setup(r => r.Cadastrar(newPedido)).Callback<Pedido>(p => _pedidos.Add(p));

            _pedidoRepoMock.Object.Cadastrar(newPedido);

            Assert.IsTrue(_pedidos.Any(p => p.Id == 4));
        }

        [TestMethod]
        public void AlterarUpdatesPedido()
        {
            var updatedPedido = new Pedido { Id = 1, UsuarioId = 1, GameId = 99 };

            _pedidoRepoMock.Setup(r => r.Alterar(updatedPedido)).Callback<Pedido>(p =>
            {
                var idx = _pedidos.FindIndex(x => x.Id == p.Id);
                if (idx >= 0) _pedidos[idx] = p;
            });

            _pedidoRepoMock.Object.Alterar(updatedPedido);

            var pedido = _pedidos.FirstOrDefault(p => p.Id == 1);
            Assert.IsNotNull(pedido);
            Assert.AreEqual(99, pedido.GameId);
        }

        [TestMethod]
        public void DeletarRemovesPedido()
        {
            _pedidoRepoMock.Setup(r => r.Deletar(2)).Callback<int>(id =>
            {
                var pedido = _pedidos.FirstOrDefault(p => p.Id == id);
                if (pedido != null) _pedidos.Remove(pedido);
            });

            _pedidoRepoMock.Object.Deletar(2);

            Assert.IsFalse(_pedidos.Any(p => p.Id == 2));
        }

        [TestMethod]
        public void CadastrarEmMassaCallsMethod()
        {
            var called = false;
            _pedidoRepoMock.Setup(r => r.CadastrarEmMassa()).Callback(() => called = true);

            _pedidoRepoMock.Object.CadastrarEmMassa();

            Assert.IsTrue(called);
        }
    }
}
