using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Base;

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