using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Mona_Logistics_LTD.Services.Localizer.Interfaces;
using System.Text.Json;

namespace Mona_Logistics_LTD.Services.Localizer.Implementations;

public class JsonLocalizerService : IJsonLocalizer
{
    private readonly IHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private Dictionary<string, string> _strings = new();

    public JsonLocalizerService(IHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
        LoadStrings();
    }

    private void LoadStrings()
    {
        var culture = GetCurrentCulture();
        var filePath = Path.Combine(_env.ContentRootPath, $"{culture}.json");

        Console.WriteLine($"Loading localization file: {filePath}");

        if (File.Exists(filePath))
        {
            var json = File.ReadAllText(filePath);
            _strings = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            Console.WriteLine($"Loaded {_strings.Count} strings for culture {culture}");
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
            // Fallback to bg-BG
            var fallbackPath = Path.Combine(_env.ContentRootPath, "bg-BG.json");
            if (File.Exists(fallbackPath))
            {
                var json = File.ReadAllText(fallbackPath);
                _strings = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }
        }
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
                    var culture = part[2..];
                    return culture == "bg-BG" ? "bg-BG" : "en-US";
                }
            }
        }

        var acceptLanguage = _httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].ToString();
        return acceptLanguage?.StartsWith("bg") == true ? "bg-BG" : "en-US";
    }

    public string GetString(string key)
    {
        if (_strings.TryGetValue(key, out var value))
            return value;

        Console.WriteLine($"Missing key: {key}");
        return key; // Return the key if not found
    }

    public string this[string key] => GetString(key);
}