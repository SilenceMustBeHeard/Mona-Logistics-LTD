using Mona_Logistics_LTD.Data.Models.Messages;
using Mona_Logistics_LTD.Data.Repositories.Implementations.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Messages;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Messages;

public class SystemMessageRepository
    : RepositoryAsync<SystemMessage, Guid>, ISystemMessageRepository
{
    public SystemMessageRepository(AppDbContext context) : base(context)
    {
    }
}