using Mona_Logistics_LTD.Data.Repositories.Interfaces.Routes;
using Mona_Logistics_LTD.Services.User.Interfaces.Routes;

namespace Mona_Logistics_LTD.Services.User.Implementations.Routes;

public class RouteService : IRouteService

{
    private readonly IRouteRepository _routeRepository;

    public RouteService(IRouteRepository routeRepository)
    {
        _routeRepository = routeRepository;
    }
}