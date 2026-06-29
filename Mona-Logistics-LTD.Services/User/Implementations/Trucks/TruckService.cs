using Mona_Logistics_LTD.Data.Repositories.Interfaces.Trucks;
using Mona_Logistics_LTD.Services.User.Interfaces.Trucks;

namespace Mona_Logistics_LTD.Services.User.Implementations.Trucks;

public class TruckService : ITruckService
{
    private readonly ITruckRepository _truckRepository;

    public TruckService(ITruckRepository truckRepository)
    {
        _truckRepository = truckRepository;
    }
}