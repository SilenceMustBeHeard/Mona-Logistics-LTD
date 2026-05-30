using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Models.Messages;
using Mona_Logistics_LTD.Data.Repositories.Implementations.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Repositories.Implementations.Messages;

public class ContactMessageRepository
    : RepositoryAsync<ContactMessage, Guid>, IContactMessageRepository
{
    public ContactMessageRepository(AppDbContext context) : base(context)
    { }

    public async Task<List<ContactMessage>> GetAdminMessagesAsync(string adminId)

     => await Query()
         .Include(m => m.Sender)
         .Include(m => m.RespondedBy)
         .Where(m => m.ReceiverId == adminId)
         .OrderByDescending(m => m.CreatedAt)
         .ToListAsync();

}



