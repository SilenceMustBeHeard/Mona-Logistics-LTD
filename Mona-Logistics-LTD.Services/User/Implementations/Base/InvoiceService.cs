using Mona_Logistics_LTD.Data.Repositories.Interfaces.Base;
using Mona_Logistics_LTD.Services.User.Interfaces.Base;

namespace Mona_Logistics_LTD.Services.User.Implementations.Base;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceService(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }
}