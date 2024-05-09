using GestaoProdutos.Domain.Entities;
using GestaoProdutos.Domain.Interfaces.Repositories;
using GestaoProdutos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
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


        public async Task Atualizar(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Inserir(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> ListarTodos()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> RecuperarPorId(long id)
        {
            return await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
