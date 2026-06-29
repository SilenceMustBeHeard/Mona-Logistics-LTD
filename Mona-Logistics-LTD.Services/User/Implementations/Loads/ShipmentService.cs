using Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;
using Mona_Logistics_LTD.Services.User.Interfaces.Loads;

namespace Mona_Logistics_LTD.Services.User.Implementations.Loads;

public class ShipmentService : IShipmentService
{
    private readonly IShipmentRepository _shipmentRepository;

    public ShipmentService(IShipmentRepository shipmentRepository)
    {
        _shipmentRepository = shipmentRepository;
    }
}