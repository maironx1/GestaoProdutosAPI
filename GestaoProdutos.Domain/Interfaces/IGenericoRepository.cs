using GestaoProdutos.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoProdutos.Domain.Interfaces
{
    public interface IGenericoRepository<TEntity>
        where TEntity : EntityBase
    {
        Task<TEntity> RecuperarPorId(long id);
        Task<IEnumerable<TEntity>> ListarTodos();
        Task Inserir(TEntity produto);
        Task Atualizar(TEntity produto);
        Task Excluir(long id);
    }
}
