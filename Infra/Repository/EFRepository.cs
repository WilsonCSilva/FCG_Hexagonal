using Infra.Infrastructure;
using Core.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repository
{
    public class EFRepository<T> : IEFRepository<T> where T : EntityBase
    {
        //Traz o context do banco de dados
        protected ApplicationDbContext _context;

        //Protected = somente as classes que herdam podem acessar
        protected DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); //Binding direto da tabela com a entidade
        }

        public void Alterar(T entidade)
        {
            _dbSet.Update(entidade);
            _context.SaveChanges();
        }

        public void Cadastrar(T entidade)
        {
            entidade.DataCriacao = DateTime.Now; 
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            _dbSet.Remove(ObterPorID(id));
            _context.SaveChanges();
        }

        public T ObterPorID(int id)
            => _dbSet.FirstOrDefault(entidade => entidade.Id == id);

        public T ObterPorNome(string nome)
            => _dbSet.FirstOrDefault(entidade => entidade.Nome == nome);

        public T ObterPorEmail(string email)
        {
            if (typeof(IEmail).IsAssignableFrom(typeof(T)))
            {
                return _dbSet.Cast<IEmail>()
                             .FirstOrDefault(e => e.Email == email) as T;
            }
            throw new NotSupportedException("A entidade não possui campo Email.");
        }

        public IList<T> ObterTodos()
            => _dbSet.ToList();
    }
}
