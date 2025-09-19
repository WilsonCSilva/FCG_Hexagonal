using Domain.Models;

namespace Core.Interfaces
{
    public interface IPedidoRepository : IEFRepository<Pedido>
    {
        void CadastrarEmMassa();
    }
}
 