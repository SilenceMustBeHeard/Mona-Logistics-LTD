using Mona_Logistics_LTD.Data.Models.Loads;

using Mona_Logistics_LTD.Web.ViewModels.User.Loads;

namespace Mona_Logistics_LTD.Services.User.Interfaces.Loads;

public interface ILoadRequestClientService
{
    Task<LoadRequest> CreateRequestAsync(LoadRequestCreateViewModel model, string clientId);

    Task<LoadRequest?> GetRequestByIdAsync(Guid id, string clientId);

    Task<IEnumerable<LoadRequest>> GetClientRequestsAsync(string clientId);

    Task<bool> UpdateRequestAsync(Guid id, LoadRequestUpdateViewModel model, string clientId);

    Task<bool> CancelRequestAsync(Guid id, string clientId);

    Task<int> GetPendingCountAsync();

    LoadRequestCreateViewModel PreparePreview(LoadRequestCreateViewModel model);

    bool CanEditRequest(LoadRequest request);
}