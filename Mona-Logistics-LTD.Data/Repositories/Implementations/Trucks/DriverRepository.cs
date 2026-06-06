using Mona_Logistics_LTD.Data.Models.Trucks;
using Mona_Logistics_LTD.Data.Repositories.Implementations.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Trucks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Trucks;

public class DriverRepository : RepositoryAsync<Driver, Guid>, IDriverRepository
{
    private readonly AppDbContext _context;
    public DriverRepository(AppDbContext context)
        : base(context)
    {
        _context = context;
    }

}
