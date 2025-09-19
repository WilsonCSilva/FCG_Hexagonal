using Domain.Models;
using Core.DTOs;

namespace Core.Interfaces
{
    public interface IUsuarioRepository : IEFRepository<Usuario>
    {
        void CadastrarEmMassa();
        UsuarioDto ObterPedidosTodos(int id);
        UsuarioDto ObterPedidosSeisMeses(int id);
    }
}
 