using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Models.Routes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Models.Loads;

public class Shipment
{
    public Guid Id { get; set; }

    public string TrackingNumber { get; set; } = null!;

    public string PickupAddress { get; set; } = null!;

    public string DeliveryAddress { get; set; } = null!;

    public DateTime PickupDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public ShipmentStatus Status { get; set; }

    public string ClientId { get; set; } = null!;

    public AppUser Client { get; set; } = null!;

    public ICollection<Cargo> CargoItems { get; set; }
        = new HashSet<Cargo>();

    public ICollection<Trip> Trips { get; set; }
        = new HashSet<Trip>();
}