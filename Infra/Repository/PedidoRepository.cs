using Infra.Infrastructure;
using Core.Interfaces;
using Domain.Models;

namespace Infra.Repository
{
    public class PedidoRepository : EFRepository<Pedido>, IPedidoRepository
    {
        public PedidoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public void CadastrarEmMassa()
        {
            var _pedidos = new List<Pedido>()
            {
                new Pedido() { UsuarioId = 1, GameId = 1 },
                new Pedido() { UsuarioId = 2, GameId = 2 },
                new Pedido() { UsuarioId = 2, GameId = 3 },
                new Pedido() { UsuarioId = 2, GameId = 4 },
                new Pedido() { UsuarioId = 5, GameId = 5 },
                new Pedido() { UsuarioId = 6, GameId = 6 },
                new Pedido() { UsuarioId = 7, GameId = 7 },
                new Pedido() { UsuarioId = 8, GameId = 8 },
                new Pedido() { UsuarioId = 9, GameId = 9 },
                new Pedido() { UsuarioId = 9, GameId = 9 },
                new Pedido() { UsuarioId = 9, GameId = 10 },
                new Pedido() { UsuarioId = 9, GameId = 19 },
                new Pedido() { UsuarioId = 9, GameId = 29 },
                new Pedido() { UsuarioId = 10, GameId = 9 },
                new Pedido() { UsuarioId = 11, GameId = 9 },
                new Pedido() { UsuarioId = 12, GameId = 15 },
                new Pedido() { UsuarioId = 13, GameId = 9 },
                new Pedido() { UsuarioId = 14, GameId = 16 },
                new Pedido() { UsuarioId = 15, GameId = 9 },
                new Pedido() { UsuarioId = 16, GameId = 20 },
                new Pedido() { UsuarioId = 17, GameId = 21 },
                new Pedido() { UsuarioId = 18, GameId = 22 },
                new Pedido() { UsuarioId = 19, GameId = 23 },
                new Pedido() { UsuarioId = 20, GameId = 9 },
            };
            _context.BulkInsert(_pedidos);
        }
    }
}
