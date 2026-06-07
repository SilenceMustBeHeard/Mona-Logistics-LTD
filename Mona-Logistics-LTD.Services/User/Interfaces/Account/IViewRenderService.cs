using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Mona_Logistics_LTD.Services.User.Interfaces.Account;

public interface IViewRenderService
{
    Task<string> RenderToStringAsync(string viewName, object model, ViewDataDictionary? viewData = null);
}
