using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mona_Logistics_LTD.Web.ViewModels.User.Account.Profile;


public class LoginViewModel
{
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address.")]

    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }
}