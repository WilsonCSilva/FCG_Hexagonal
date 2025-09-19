using Domain.Models;

namespace Core.Interfaces
{
    public interface IEFRepository<T> where T : EntityBase
    {
        IList<T> ObterTodos();
        T ObterPorID(int id);
        T ObterPorNome(string nome);
        T ObterPorEmail(string email);
        void Cadastrar(T entity);
        void Alterar(T entity);
        void Deletar(int id);
    }
}
 