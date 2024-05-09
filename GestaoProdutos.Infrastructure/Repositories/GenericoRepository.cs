using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Interfaces;
using GestaoProdutos.Infrastructure.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoProdutos.Infrastructure.Repositories
{
    public class GenericoRepository<TEntity> : IGenericoRepository<TEntity> where TEntity : EntityBase
    {
        public readonly GestaoProdutosContext _dbContext;

        public GenericoRepository(GestaoProdutosContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task Atualizar(TEntity produto)
        {
            throw new System.NotImplementedException();
        }

        public Task Excluir(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task Inserir(TEntity produto)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> ListarTodos()
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> RecuperarPorId(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
