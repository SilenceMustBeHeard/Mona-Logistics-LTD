using Mona_Logistics_LTD.Data.Models.Messages;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.Messages;

public interface ISystemMessageRepository
    : IFullRepositoryAsync<SystemMessage, Guid>
{
}