using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;

public interface ICategoryRepository<TEntity, TKey> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetCategoriesAsync();
}
