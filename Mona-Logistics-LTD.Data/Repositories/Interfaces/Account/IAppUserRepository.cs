using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.Account;

public interface IAppUserRepository
    : IFullRepositoryAsync<AppUser, string>
{
}