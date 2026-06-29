using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;

public interface ILoadRequestRepository : IFullRepositoryAsync<LoadRequest, Guid>
{
    Task<IEnumerable<LoadRequest>> GetClientRequestsAsync(string clientId);

    Task<IEnumerable<LoadRequest>> GetPendingRequestsAsync();

    Task<IEnumerable<LoadRequest>> GetRequestsByStatusAsync(LoadRequestStatus status);

    Task<int> GetPendingCountAsync();
}