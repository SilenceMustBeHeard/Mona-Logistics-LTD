using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Repositories.Interfaces.Base;

public interface IInvoiceRepository : IFullRepositoryAsync<Invoice, Guid>
{
}
