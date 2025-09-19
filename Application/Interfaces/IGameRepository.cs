using Domain.Models;

namespace Core.Interfaces
{
    public interface IGameRepository : IEFRepository<Game>
    {
        void CadastrarEmMassa();
    }
}
 