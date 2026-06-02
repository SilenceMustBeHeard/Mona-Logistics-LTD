using Mona_Logistics_LTD.Data.Models.Trucks;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.Trucks;

public interface IDriverRepository : IFullRepositoryAsync<Driver, Guid>
{
}
