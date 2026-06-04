using Microsoft.AspNetCore.Identity;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Account;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Messages;
using Mona_Logistics_LTD.Services.Admin.Interfaces.Message;
using Mona_Logistics_LTD.Web.ViewModels.User.Account;
using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Models.Messages;

namespace Mona_Logistics_LTD.Services.Admin.Implementations.Message;

public class SystemMessageAdminService : ISystemMessageAdminService

{
    private readonly ISystemMessageRepository _messageRepository;
    private readonly IAppUserRepository _userRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SystemMessageAdminService(ISystemMessageRepository messageRepository,
        UserManager<AppUser> userManager,
         IAppUserRepository userRepository,
        RoleManager<IdentityRole> roleManager)
    {
        _messageRepository = messageRepository;
        _userManager = userManager;
        _userRepository = userRepository;
        _roleManager = roleManager;
    }

    // marks a message as read
    public async Task MarkMessageAsReadAsync(Guid messageId, string userId)
    {
        var message = await _messageRepository
            .FirstOrDefaultAsync(x => x.Id == messageId && x.ReceiverId == userId);

        if (message == null)
            return;

        message.IsRead = true;
        await _messageRepository.UpdateAsync(message);
    }

    // gets the count of unread messages
    public async Task<int> GetUnreadCountAsync(string userId)
    {
        return await _messageRepository
            .GetAllAttachedAsync()
            .CountAsync(x => x.ReceiverId == userId && !x.IsRead);
    }

    public async Task<SystemMessageViewModel?> GetMessageDetailsAsync(Guid messageId, string userId)
    {
        var message = await _messageRepository
            .GetAllAttachedAsync()
            .FirstOrDefaultAsync(m => m.Id == messageId && m.ReceiverId == userId);

        if (message == null)
            return null;

        message.IsRead = true;
        await _messageRepository.UpdateAsync(message);
        await _messageRepository.SaveChangesAsync();

        return new SystemMessageViewModel
        {
            Id = messageId,
            Description = message.Description,
            IsRead = message.IsRead,
            CreatedOn = message.CreatedAt,
            Type = message.Type,
            SenderId = message.SenderId
        };
    }

    public async Task CreateMessageAsync(SystemMessage message)
    {
        await _messageRepository.AddAsync(message);
        await _messageRepository.SaveChangesAsync();
    }

    public async Task<List<SystemMessageViewModel>> GetUserMessagesAsync(string userId)
    {
        return await _messageRepository
            .GetAllAttachedAsync()
            .Where(m => m.ReceiverId == userId)
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new SystemMessageViewModel
            {
                Id = m.Id,
                Description = m.Description,
                IsRead = m.IsRead,
                CreatedOn = m.CreatedAt,
                Type = m.Type,

                SenderId = m.SenderId
            })
            .ToListAsync();
    }

    public async Task<List<SystemMessageViewModel>> GetAdminMessagesAsync(string adminId)
    {
        return await _messageRepository
            .GetAllAttachedAsync()

            .Where(m => m.ReceiverId == adminId)
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new SystemMessageViewModel
            {
                Id = m.Id,
                Description = m.Description,
                IsRead = m.IsRead,
                CreatedOn = m.CreatedAt,
                Type = m.Type
            })
            .ToListAsync();
    }
}