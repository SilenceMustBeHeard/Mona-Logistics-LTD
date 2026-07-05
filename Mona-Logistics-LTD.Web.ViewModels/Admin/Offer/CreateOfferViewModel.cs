using System.ComponentModel.DataAnnotations;
using Mona_Logistics_LTD.Data.Common.Enums;

namespace Mona_Logistics_LTD.Web.ViewModels.Admin.Offer;

public class CreateOfferViewModel
{
    public Guid LoadRequestId { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999,999.99")]
    public decimal Price { get; set; }

    [Range(0, 999999.99, ErrorMessage = "Discount must be between 0 and 999,999.99")]
    public decimal? Discount { get; set; }

    [StringLength(500, ErrorMessage = "Notes must be less than 500 characters")]
    public string? Notes { get; set; }

    [Required(ErrorMessage = "Validity date is required")]
    public DateTime ValidUntil { get; set; } = DateTime.UtcNow.AddDays(7);
}