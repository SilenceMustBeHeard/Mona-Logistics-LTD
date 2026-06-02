using Mona_Logistics_LTD.Data.Models.Routes;
using Mona_Logistics_LTD.Data.Repositories.Implementations.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Routes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Routes;

public class RouteRepository : RepositoryAsync<Route, Guid>, IRouteRepository
{
    private readonly AppDbContext _context;
    public RouteRepository(AppDbContext context)
        : base(context)
    {
        _context = context;
    }
}
