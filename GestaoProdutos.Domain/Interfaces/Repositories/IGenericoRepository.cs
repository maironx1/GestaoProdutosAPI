using GestaoProdutos.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Interfaces.Repositories
{
    public interface IGenericoRepository<TEntity>
        where TEntity : EntityBase
    {
        Task<TEntity> RecuperarPorId(long id);
        Task<IEnumerable<TEntity>> ListarTodos();
        Task Inserir(TEntity entity);
        Task Atualizar(TEntity entity);
    }
}
