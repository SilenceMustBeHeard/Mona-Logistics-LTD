namespace Mona_Logistics_LTD.Services.User.Interfaces.Account;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string to, string subject, string body);
}