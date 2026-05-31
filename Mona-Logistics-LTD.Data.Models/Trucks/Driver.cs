using Mona_Logistics_LTD.Data.Models.Routes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Models.Trucks;

public class Driver
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string LicenseNumber { get; set; } = null!;

    public DateTime LicenseExpiryDate { get; set; }

    public bool IsAvailable { get; set; }

    public ICollection<Trip> Trips { get; set; }
        = new HashSet<Trip>();
}