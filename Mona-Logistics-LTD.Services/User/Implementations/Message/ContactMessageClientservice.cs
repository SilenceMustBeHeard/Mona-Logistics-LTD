using Mona_Logistics_LTD.Data.Repositories.Interfaces.Messages;
using Mona_Logistics_LTD.Services.User.Interfaces.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Services.User.Implementations.Message;

public class ContactMessageClientservice : IContactMessageClientService
{
    private readonly IContactMessageRepository _contactMessageService;

    public ContactMessageClientservice(IContactMessageRepository contactMessageService)
    {
        _contactMessageService = contactMessageService;
    }
}
