using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;

public interface IShipmentRepository : IFullRepositoryAsync<Shipment, Guid>
{
}