using Domain.Models;

namespace TEST_FCG._Tests_.Models
{
    [TestClass]
    public class PedidoTest
    {
        [TestMethod]
        public void CanSetAndGetUsuarioId()
        {
            var pedido = new Pedido
            {
                UsuarioId = 10,
                GameId = 20
            };
            Assert.AreEqual(10, pedido.UsuarioId);
        }

        [TestMethod]
        public void CanSetAndGetGameId()
        {
            var pedido = new Pedido
            {
                UsuarioId = 10,
                GameId = 99
            };
            Assert.AreEqual(99, pedido.GameId);
        }

        [TestMethod]
        public void CanSetAndGetUsuario()
        {
            var usuario = new Usuario
            {
                Nome = "User1",
                Email = "user1@email.com",
                DataNascimento = new DateTime(2000, 1, 1),
                TipoUsuario = 1,
                Pedidos = [],
                Endereco = "Rua 1",
                Senha = "senha"
            };
            var pedido = new Pedido
            {
                UsuarioId = 1,
                GameId = 2,
                Usuario = usuario
            };
            Assert.AreEqual(usuario, pedido.Usuario);
            Assert.AreEqual("User1", pedido.Usuario.Nome);
        }

        [TestMethod]
        public void CanSetAndGetGame()
        {
            var game = new Game
            {
                Produtora = "Prod",
                Descricao = "Desc",
                Preco = 10,
                DataLancamento = DateTime.Today,
                Pedidos = []
            };
            var pedido = new Pedido
            {
                UsuarioId = 1,
                GameId = 2,
                Game = game
            };
            Assert.AreEqual(game, pedido.Game);
            Assert.AreEqual("Prod", pedido.Game.Produtora);
        }

        [TestMethod]
        public void InheritsFromEntityBase()
        {
            var pedido = new Pedido
            {
                UsuarioId = 1,
                GameId = 2
            };
            pedido.Nome = "PedidoNome";
            pedido.Id = 123;
            pedido.DataCriacao = new DateTime(2023, 1, 1);

            Assert.AreEqual("PedidoNome", pedido.Nome);
            Assert.AreEqual(123, pedido.Id);
            Assert.AreEqual(new DateTime(2023, 1, 1), pedido.DataCriacao);
        }
    }
}
