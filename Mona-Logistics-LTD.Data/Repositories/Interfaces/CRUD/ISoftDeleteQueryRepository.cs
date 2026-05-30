using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;

public interface ISoftDeleteQueryRepository<TEntity, TKey> where TEntity : class
{
    IQueryable<TEntity> GetAllIncludingDeleted();
    Task<bool> ToggleStatusAsync(TEntity entity);
}
