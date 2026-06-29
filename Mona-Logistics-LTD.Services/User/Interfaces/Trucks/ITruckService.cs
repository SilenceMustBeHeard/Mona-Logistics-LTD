using Mona_Logistics_LTD.Data.Models.Trucks;
using Microsoft.EntityFrameworkCore;

namespace Mona_Logistics_LTD.Services.User.Interfaces.Trucks;

public interface ITruckService
{
    Task<IEnumerable<Truck>> GetAllAsync();
}