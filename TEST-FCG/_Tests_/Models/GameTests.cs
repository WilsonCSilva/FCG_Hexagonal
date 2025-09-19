using System;
using System.Collections.Generic;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TEST_FCG._Tests_.Models
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void CanSetAndGetProdutora()
        {
            var game = new Game
            {
                Produtora = "TestProdutora",
                Descricao = "TestDescricao",
                Preco = 99.99m,
                DataLancamento = new DateTime(2024, 1, 1),
                Pedidos = new List<Pedido>()
            };
            Assert.AreEqual("TestProdutora", game.Produtora);
        }

        [TestMethod]
        public void CanSetAndGetDescricao()
        {
            var game = new Game
            {
                Produtora = "Prod",
                Descricao = "Descricao do jogo",
                Preco = 10,
                DataLancamento = DateTime.Today,
                Pedidos = new List<Pedido>()
            };
            Assert.AreEqual("Descricao do jogo", game.Descricao);
        }

        [TestMethod]
        public void CanSetAndGetPreco()
        {
            var game = new Game
            {
                Produtora = "Prod",
                Descricao = "Desc",
                Preco = 123.45m,
                DataLancamento = DateTime.Today,
                Pedidos = new List<Pedido>()
            };
            Assert.AreEqual(123.45m, game.Preco);
        }

        [TestMethod]
        public void CanSetAndGetDataLancamento()
        {
            var date = new DateTime(2023, 12, 31);
            var game = new Game
            {
                Produtora = "Prod",
                Descricao = "Desc",
                Preco = 1,
                DataLancamento = date,
                Pedidos = new List<Pedido>()
            };
            Assert.AreEqual(date, game.DataLancamento);
        }

        [TestMethod]
        public void CanSetAndGetPedidos()
        {
            var pedidos = new List<Pedido>
            {
                new Pedido { Id = 1, Nome = "Pedido1", GameId = 1, UsuarioId = 1, DataCriacao = DateTime.Today }
            };
            var game = new Game
            {
                Produtora = "Prod",
                Descricao = "Desc",
                Preco = 1,
                DataLancamento = DateTime.Today,
                Pedidos = pedidos
            };
            Assert.AreEqual(pedidos, game.Pedidos);
            Assert.AreEqual(1, game.Pedidos.Count);
        }

        [TestMethod]
        public void InheritsFromEntityBase()
        {
            var game = new Game
            {
                Produtora = "Prod",
                Descricao = "Desc",
                Preco = 1,
                DataLancamento = DateTime.Today,
                Pedidos = new List<Pedido>()
            };
            game.Nome = "GameName";
            game.Id = 42;
            game.DataCriacao = new DateTime(2022, 1, 1);

            Assert.AreEqual("GameName", game.Nome);
            Assert.AreEqual(42, game.Id);
            Assert.AreEqual(new DateTime(2022, 1, 1), game.DataCriacao);
        }
    }
}
