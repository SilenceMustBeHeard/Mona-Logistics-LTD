using Mona_Logistics_LTD.Data.Repositories.Interfaces.Messages;
using Mona_Logistics_LTD.Services.User.Interfaces.Message;
using Mona_Logistics_LTD.Web.ViewModels.User.Account;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Mona_Logistics_LTD.Services.User.Implementations.Message;

public class SystemMessageClientService : ISystemMessageClientService
{
    private readonly ISystemMessageRepository _systemMessageRepository;

    public SystemMessageClientService(ISystemMessageRepository systemMessageRepository)
    {
        _systemMessageRepository = systemMessageRepository;
    }


    public async Task<List<SystemMessageViewModel>> GetUserMessagesAsync(string userId)
    {
        return await _systemMessageRepository
            .GetAllAttachedAsync()
            .Where(m => m.ReceiverId == userId)
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new SystemMessageViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                IsRead = m.IsRead,
                CreatedOn = m.CreatedAt,
                Type = m.Type
            })
            .ToListAsync();
    }

    public async Task<SystemMessageViewModel?> GetMessageDetailsAsync(Guid messageId, string userId)
    {
        var message = await _systemMessageRepository
            .GetAllAttachedAsync()
            .FirstOrDefaultAsync(m => m.Id == messageId && m.ReceiverId == userId);

        if (message == null)
            return null;

        if (!message.IsRead)
        {
            message.IsRead = true;
            await _systemMessageRepository.UpdateAsync(message);
            await _systemMessageRepository.SaveChangesAsync();
        }

        return new SystemMessageViewModel
        {
            Id = message.Id,
            Title = message.Title,
            Description = message.Description,
            IsRead = message.IsRead,
            CreatedOn = message.CreatedAt,
            Type = message.Type
        };
    }

    public async Task<int> GetUnreadCountAsync(string userId)
    {
        return await _systemMessageRepository
            .GetAllAttachedAsync()
            .CountAsync(m => m.ReceiverId == userId && !m.IsRead);
    }

    public async Task MarkAsReadAsync(Guid messageId, string userId)
    {
        var message = await _systemMessageRepository
            .GetAllAttachedAsync()
            .FirstOrDefaultAsync(m => m.Id == messageId && m.ReceiverId == userId);

        if (message != null && !message.IsRead)
        {
            message.IsRead = true;
            await _systemMessageRepository.UpdateAsync(message);
        }
    }
}
