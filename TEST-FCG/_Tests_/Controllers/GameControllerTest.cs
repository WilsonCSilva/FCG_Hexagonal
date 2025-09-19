using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FCG.Controllers;
using Core.Interfaces;
using Domain.Models;
using Core.DTOs;
using Core.Inputs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using FCG.Middlewares;

namespace TEST_FCG._Tests_.Controllers
{
    [TestClass]
    public class GameControllerTest
    {
        private Mock<IGameRepository> _gameRepoMock;
        private Mock<BaseLogger<GameController>> _loggerMock;
        private GameController _controller;

        [TestInitialize]
        public void Setup()
        {
            _gameRepoMock = new Mock<IGameRepository>();
            _loggerMock = new Mock<BaseLogger<GameController>>(MockBehavior.Loose, null as Microsoft.Extensions.Logging.ILogger<GameController>);
            _controller = new GameController(_gameRepoMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public void ReturnsOkWhenGettingAllGames()
        {
            var games = new List<Game>
            {
                new Game { Id = 1, Nome = "Game1", Produtora = "Prod1", Descricao = "Desc1", DataLancamento = DateTime.Today, Preco = 10 }
            };
            _gameRepoMock.Setup(r => r.ObterTodos()).Returns(games);

            var result = _controller.get();

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<GameDto>));
            var value = okResult.Value as List<GameDto>;
            Assert.AreEqual(1, value.Count);
        }

        [TestMethod]
        public void ReturnsOkWhenGameFoundByName()
        {
            var game = new Game { Id = 2, Nome = "Game2", Produtora = "Prod2", Descricao = "Desc2", DataLancamento = DateTime.Today, Preco = 20 };
            _gameRepoMock.Setup(r => r.ObterPorNome("Game2")).Returns(game);

            var result = _controller.Get("Game2");

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(GameDto));
            var dto = okResult.Value as GameDto;
            Assert.AreEqual("Game2", dto.Nome);
        }

        [TestMethod]
        public void ReturnsOkWhenGameFoundById()
        {
            var game = new Game { Id = 3, Nome = "Game3", Produtora = "Prod3", Descricao = "Desc3", DataLancamento = DateTime.Today, Preco = 30 };
            _gameRepoMock.Setup(r => r.ObterPorID(3)).Returns(game);

            var result = _controller.Get(3);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(GameDto));
            var dto = okResult.Value as GameDto;
            Assert.AreEqual(3, dto.Id);
        }

        [TestMethod]
        public void ReturnsOkWhenPostingNewGame()
        {
            var input = new GameInput
            {
                Nome = "Game4",
                Produtora = "Prod4",
                Descricao = "Desc4",
                DataLancamento = DateTime.Today,
                Preco = 40
            };

            _gameRepoMock.Setup(r => r.Cadastrar(It.IsAny<Game>()));

            var result = _controller.Post(input);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsTrue(okResult.Value.ToString().Contains("cadastrado com sucesso"));
        }

        [TestMethod]
        public void ReturnsOkWhenUpdatingGame()
        {
            var updateInput = new GameUpdateInput
            {
                Id = 5,
                Nome = "Game5",
                Produtora = "Prod5",
                Descricao = "Desc5",
                DataLancamento = DateTime.Today,
                Preco = 50
            };

            var game = new Game
            {
                Id = 5,
                Nome = "OldName",
                Produtora = "OldProd",
                Descricao = "OldDesc",
                DataLancamento = DateTime.Today.AddDays(-1),
                Preco = 10
            };

            _gameRepoMock.Setup(r => r.ObterPorID(5)).Returns(game);
            _gameRepoMock.Setup(r => r.Alterar(It.IsAny<Game>()));

            var result = _controller.Put(updateInput);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsTrue(okResult.Value.ToString().Contains("alterado com sucesso"));
        }

        [TestMethod]
        public void ReturnsOkWhenDeletingGame()
        {
            _gameRepoMock.Setup(r => r.Deletar(6));

            var result = _controller.Delete(6);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.IsTrue(okResult.Value.ToString().Contains("deletado com sucesso"));
        }
    }
}
