using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Account;

public class AppUserRepository : RepositoryAsync<AppUser, string>, IAppUserRepository
{
    public AppUserRepository(AppDbContext context) : base(context)
    {
    }


    public async Task<AppUser?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
}
