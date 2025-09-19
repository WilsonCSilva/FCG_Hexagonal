using Core.Interfaces;
using Domain.Models;
using Moq;

namespace TEST_FCG._Tests_.Repository
{
    [TestClass]
    public class GameRepositoryTest
    {
        private Mock<IGameRepository> _gameRepoMock;
        private List<Game> _games;

        [TestInitialize]
        public void Setup()
        {
            _games = new List<Game>
            {
                new Game { Id = 1, Nome = "Game1", Produtora = "Prod1", Descricao = "Desc1", DataLancamento = DateTime.Today, Preco = 10 },
                new Game { Id = 2, Nome = "Game2", Produtora = "Prod2", Descricao = "Desc2", DataLancamento = DateTime.Today, Preco = 20 }
            };

            _gameRepoMock = new Mock<IGameRepository>();
        }

        [TestMethod]
        public void ObterTodos_ReturnsAllGames()
        {
            _gameRepoMock.Setup(r => r.ObterTodos()).Returns(_games);

            var result = _gameRepoMock.Object.ObterTodos();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(g => g.Nome == "Game1"));
            Assert.IsTrue(result.Any(g => g.Nome == "Game2"));
        }

        [TestMethod]
        public void ObterPorID_ReturnsCorrectGame()
        {
            _gameRepoMock.Setup(r => r.ObterPorID(1)).Returns(_games[0]);

            var result = _gameRepoMock.Object.ObterPorID(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Game1", result.Nome);
        }

        [TestMethod]
        public void ObterPorID_ReturnsNullIfNotFound()
        {
            _gameRepoMock.Setup(r => r.ObterPorID(99)).Returns((Game)null);

            var result = _gameRepoMock.Object.ObterPorID(99);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ObterPorNome_ReturnsCorrectGame()
        {
            _gameRepoMock.Setup(r => r.ObterPorNome("Game2")).Returns(_games[1]);

            var result = _gameRepoMock.Object.ObterPorNome("Game2");

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Id);
        }

        [TestMethod]
        public void ObterPorNome_ReturnsNullIfNotFound()
        {
            _gameRepoMock.Setup(r => r.ObterPorNome("NotFound")).Returns((Game)null);

            var result = _gameRepoMock.Object.ObterPorNome("NotFound");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ObterPorEmail_ReturnsCorrectGame()
        {
            var gameWithEmail = new Game
            {
                Id = 3,
                Nome = "Game3",
                Produtora = "Prod3",
                Descricao = "Desc3",
                DataLancamento = DateTime.Today,
                Preco = 30
            };
            typeof(Game).GetProperty("Email")?.SetValue(gameWithEmail, "test@email.com");
            _gameRepoMock.Setup(r => r.ObterPorEmail("test@email.com")).Returns(gameWithEmail);

            var result = _gameRepoMock.Object.ObterPorEmail("test@email.com");

            Assert.IsNotNull(result);
            Assert.AreEqual("Game3", result.Nome);
        }

        [TestMethod]
        public void ObterPorEmail_ReturnsNullIfNotFound()
        {
            _gameRepoMock.Setup(r => r.ObterPorEmail("notfound@email.com")).Returns((Game)null);

            var result = _gameRepoMock.Object.ObterPorEmail("notfound@email.com");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Cadastrar_AddsGame()
        {
            var newGame = new Game
            {
                Id = 4,
                Nome = "Game4",
                Produtora = "Prod4",
                Descricao = "Desc4",
                DataLancamento = DateTime.Today,
                Preco = 40
            };

            _gameRepoMock.Setup(r => r.Cadastrar(newGame)).Callback<Game>(g => _games.Add(g));

            _gameRepoMock.Object.Cadastrar(newGame);

            Assert.IsTrue(_games.Any(g => g.Id == 4));
        }

        [TestMethod]
        public void Alterar_UpdatesGame()
        {
            var updatedGame = new Game
            {
                Id = 1,
                Nome = "Game1Updated",
                Produtora = "Prod1",
                Descricao = "Desc1",
                DataLancamento = DateTime.Today,
                Preco = 15
            };

            _gameRepoMock.Setup(r => r.Alterar(updatedGame)).Callback<Game>(g =>
            {
                var idx = _games.FindIndex(x => x.Id == g.Id);
                if (idx >= 0) _games[idx] = g;
            });

            _gameRepoMock.Object.Alterar(updatedGame);

            var game = _games.FirstOrDefault(g => g.Id == 1);
            Assert.IsNotNull(game);
            Assert.AreEqual("Game1Updated", game.Nome);
            Assert.AreEqual(15, game.Preco);
        }

        [TestMethod]
        public void Deletar_RemovesGame()
        {
            _gameRepoMock.Setup(r => r.Deletar(2)).Callback<int>(id =>
            {
                var game = _games.FirstOrDefault(g => g.Id == id);
                if (game != null) _games.Remove(game);
            });

            _gameRepoMock.Object.Deletar(2);

            Assert.IsFalse(_games.Any(g => g.Id == 2));
        }

        [TestMethod]
        public void CadastrarEmMassa_CallsMethod()
        {
            var called = false;
            _gameRepoMock.Setup(r => r.CadastrarEmMassa()).Callback(() => called = true);

            _gameRepoMock.Object.CadastrarEmMassa();

            Assert.IsTrue(called);
        }
    }
}
