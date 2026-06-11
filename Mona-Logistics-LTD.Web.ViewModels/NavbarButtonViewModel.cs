namespace Mona_Logistics_LTD.Web.ViewModels;

public class NavbarButtonsViewModel
{
    public bool IsLoggedIn { get; set; }
    public bool IsUser { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsManager { get; set; }
    public int UnreadMessagesCount { get; set; }
}