using System.Linq.Expressions;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;

public interface IFindRepository<TEntity, TKey> where TEntity : class
{
    Task<TEntity?> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate);
}