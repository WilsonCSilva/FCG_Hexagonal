using Infra.Infrastructure;
using Domain.Models;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TEST_FCG._Tests_.Repository
{
    [TestClass]
    public class EFRepositoryTest
    {
        private Mock<ApplicationDbContext> _contextMock;
        private Mock<DbSet<Game>> _dbSetMock;
        private EFRepository<Game> _repository;
        private List<Game> _games;

        [TestInitialize]
        public void Setup()
        {
            _games = new List<Game>
            {
                new Game { Id = 1, Nome = "Game1", Produtora = "Prod1", Descricao = "Desc1", DataLancamento = DateTime.Today, Preco = 10 },
                new Game { Id = 2, Nome = "Game2", Produtora = "Prod2", Descricao = "Desc2", DataLancamento = DateTime.Today, Preco = 20 }
            };

            var queryable = _games.AsQueryable();

            _dbSetMock = new Mock<DbSet<Game>>();
            _dbSetMock.As<IQueryable<Game>>().Setup(m => m.Provider).Returns(queryable.Provider);
            _dbSetMock.As<IQueryable<Game>>().Setup(m => m.Expression).Returns(queryable.Expression);
            _dbSetMock.As<IQueryable<Game>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            _dbSetMock.As<IQueryable<Game>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            _dbSetMock.Setup(d => d.Add(It.IsAny<Game>())).Callback<Game>(g => _games.Add(g));
            _dbSetMock.Setup(d => d.Update(It.IsAny<Game>())).Callback<Game>(g =>
            {
                var idx = _games.FindIndex(x => x.Id == g.Id);
                if (idx >= 0) _games[idx] = g;
            });
            _dbSetMock.Setup(d => d.Remove(It.IsAny<Game>())).Callback<Game>(g => _games.Remove(g));

            _contextMock = new Mock<ApplicationDbContext>();
            _contextMock.Setup(c => c.Set<Game>()).Returns(_dbSetMock.Object);
            _contextMock.Setup(c => c.SaveChanges()).Verifiable();

            _repository = new EFRepository<Game>(_contextMock.Object);
        }

        [TestMethod]
        public void GetAllReturnsAllGames()
        {
            var result = _repository.ObterTodos();
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Any(g => g.Nome == "Game1"));
            Assert.IsTrue(result.Any(g => g.Nome == "Game2"));
        }

        [TestMethod]
        public void GetByIdReturnsCorrectGame()
        {
            var result = _repository.ObterPorID(1);
            Assert.IsNotNull(result);
            Assert.AreEqual("Game1", result.Nome);
        }

        [TestMethod]
        public void GetByIdReturnsNullIfNotFound()
        {
            var result = _repository.ObterPorID(99);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByNameReturnsCorrectGame()
        {
            var result = _repository.ObterPorNome("Game2");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Id);
        }

        [TestMethod]
        public void GetByNameReturnsNullIfNotFound()
        {
            var result = _repository.ObterPorNome("NotFound");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetByEmail_ReturnsCorrectGame()
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

            var emailProp = typeof(Game).GetProperty("Email");
            if (emailProp == null)
            {
                Assert.ThrowsException<NotSupportedException>(() => _repository.ObterPorEmail("test@email.com"));
                return;
            }
            emailProp.SetValue(gameWithEmail, "test@email.com");
            _games.Add(gameWithEmail);

            var result = _repository.ObterPorEmail("test@email.com");
            Assert.IsNotNull(result);
            Assert.AreEqual("Game3", result.Nome);
        }

        [TestMethod]
        public void GetByEmailReturnsNullIfNotFound()
        {
            var emailProp = typeof(Game).GetProperty("Email");
            if (emailProp == null)
            {
                Assert.ThrowsException<NotSupportedException>(() => _repository.ObterPorEmail("notfound@email.com"));
                return;
            }
            var result = _repository.ObterPorEmail("notfound@email.com");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void RegisterAddsGameAndSetsCreationDate()
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

            _repository.Cadastrar(newGame);

            Assert.IsTrue(_games.Any(g => g.Id == 4));
            Assert.IsTrue(newGame.DataCriacao <= DateTime.Now && newGame.DataCriacao > DateTime.Now.AddMinutes(-1));
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void UpdateUpdatesGame()
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

            _repository.Alterar(updatedGame);

            var game = _games.FirstOrDefault(g => g.Id == 1);
            Assert.IsNotNull(game);
            Assert.AreEqual("Game1Updated", game.Nome);
            Assert.AreEqual(15, game.Preco);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteRemovesGame()
        {
            _repository.Deletar(2);

            Assert.IsFalse(_games.Any(g => g.Id == 2));
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }
    }
}
