using Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;
using Mona_Logistics_LTD.Services.User.Interfaces.Loads;

namespace Mona_Logistics_LTD.Services.User.Implementations.Loads;

public class CargoService : ICargoService
{
    private readonly ICargoRepository _cargoRepository;

    public CargoService(ICargoRepository cargoRepository)
    {
        _cargoRepository = cargoRepository;
    }
}