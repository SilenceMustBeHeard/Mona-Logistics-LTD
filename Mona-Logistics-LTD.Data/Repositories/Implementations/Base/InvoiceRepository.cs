using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Base;

public class InvoiceRepository : RepositoryAsync<Invoice, Guid>, IInvoiceRepository
{
    private readonly AppDbContext _context;
    public InvoiceRepository(AppDbContext context) 
        : base(context)
    {
        _context = context;
    }
}
