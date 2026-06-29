using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Mona_Logistics_LTD.Services.Localizer.Interfaces;
using System.Text.Json;

namespace Mona_Logistics_LTD.Services.Localizer.Implementations;

public class JsonLocalizerService : IJsonLocalizer
{
    private readonly IHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JsonLocalizerService(IHostEnvironment env,
        IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    private Dictionary<string, string> GetStrings()
    {
        var culture = GetCurrentCulture();
        var filePath = Path.Combine(_env.ContentRootPath, $"{culture}.json");

        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        }

        // Fallback
        var fallbackPath = Path.Combine(_env.ContentRootPath, "bg-BG.json");
        if (File.Exists(fallbackPath))
        {
            var json = File.ReadAllText(fallbackPath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        }

        return new Dictionary<string, string>();
    }

    private string GetCurrentCulture()
    {
        var cookie = _httpContextAccessor.HttpContext?.Request.Cookies[".AspNetCore.Culture"];
        if (cookie != null && cookie.Contains("c="))
        {
            var parts = cookie.Split('|');
            foreach (var part in parts)
            {
                if (part.StartsWith("c="))
                {
                    return part[2..] == "bg-BG" ? "bg-BG" : "en-US";
                }
            }
        }

        return "bg-BG";
    }

    public string GetString(string key)
    {
        var strings = GetStrings();
        if (strings.TryGetValue(key, out var value))
            return value;
        return key;
    }

    public string this[string key] => GetString(key);
}