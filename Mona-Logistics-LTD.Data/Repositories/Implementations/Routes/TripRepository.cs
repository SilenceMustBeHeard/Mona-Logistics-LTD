using Mona_Logistics_LTD.Data.Models.Routes;
using Mona_Logistics_LTD.Data.Repositories.Implementations.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Routes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Routes;

public class TripRepository : RepositoryAsync<Trip, Guid>, ITripRepository
{
    private readonly AppDbContext _context;
    public TripRepository(AppDbContext context)
        : base(context)
    {
        _context = context;
    }
}
