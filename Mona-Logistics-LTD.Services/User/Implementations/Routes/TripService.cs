using Mona_Logistics_LTD.Data.Repositories.Interfaces.Routes;
using Mona_Logistics_LTD.Services.User.Interfaces.Routes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Services.User.Implementations.Routes;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }
}
