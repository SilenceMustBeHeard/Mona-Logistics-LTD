using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Implementations.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Loads;

public class CargoRepository : RepositoryAsync<Cargo, Guid>, ICargoRepository
{
    private readonly AppDbContext _context;

    public CargoRepository(AppDbContext context)
        : base(context)
    {
        _context = context;
    }
}