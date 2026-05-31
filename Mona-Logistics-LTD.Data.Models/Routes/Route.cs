using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Models.Routes;

public class Route
{
    public Guid Id { get; set; }

    public string Origin { get; set; } = null!;

    public string Destination { get; set; } = null!;

    public double DistanceKm { get; set; }

    public ICollection<Trip> Trips { get; set; }
        = new HashSet<Trip>();
}