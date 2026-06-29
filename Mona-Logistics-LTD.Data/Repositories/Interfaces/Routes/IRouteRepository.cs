using Mona_Logistics_LTD.Data.Models.Routes;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.Routes;

public interface IRouteRepository : IFullRepositoryAsync<Route, Guid>
{
}