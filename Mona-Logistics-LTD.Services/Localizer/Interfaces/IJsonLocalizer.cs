using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Services.Localizer.Interfaces;

public interface IJsonLocalizer
{
    string GetString(string key);
    string this[string key] { get; }
}