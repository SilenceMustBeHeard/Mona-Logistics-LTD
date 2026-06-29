using Mona_Logistics_LTD.Data.Models.Trucks;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.Trucks;

public interface ITruckRepository : IFullRepositoryAsync<Truck, Guid>
{
    Task<IEnumerable<Truck>> GetAllAsync();
}