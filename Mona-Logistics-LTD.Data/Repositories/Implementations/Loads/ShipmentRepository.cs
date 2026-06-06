using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Repositories.Implementations.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Loads;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Loads;

public class ShipmentRepository : RepositoryAsync<Shipment, Guid>, IShipmentRepository
{
    private readonly AppDbContext _context;
    public ShipmentRepository(AppDbContext context)
        : base(context)
    {
        _context = context;
    }

}
