using Mona_Logistics_LTD.Data.Repositories.Interfaces.Trucks;
using Mona_Logistics_LTD.Services.User.Interfaces.Trucks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Services.User.Implementations.Trucks;

public class DriverService : IDriverService
{
    private readonly IDriverRepository _driverRepository;

    public DriverService(IDriverRepository driverRepository)
    {
        _driverRepository = driverRepository;
    }
}
