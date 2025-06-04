using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dota2RandomizerApp.Models;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Dota2RandomizerApp.Controllers;

public class HeroController : Controller
{
    private readonly ILogger<HeroController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public HeroController(ILogger<HeroController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var heroes = new List<Hero>();
        var client = _httpClientFactory.CreateClient();

        var response = await client.GetStringAsync("https://api.opendota.com/api/heroes");
        var json = JArray.Parse(response);

        foreach (var hero in json)
        {
            string apiName = hero["name"]?.ToString()?.Replace("npc_dota_hero_", "") ?? "";
            string imageUrl = $"https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/heroes/{apiName}.png";

            heroes.Add(new Hero
            {
                Name = apiName,
                LocalizedName = hero["localized_name"]?.ToString() ?? "",
                Roles = string.Join(", ", hero["roles"]?.Select(r => r.ToString()) ?? []).ToLower(new CultureInfo("en-US", true)),
                AttackType = hero["attack_type"]?.ToString() ?? "",
                PrimaryAttribute = hero["primary_attr"]?.ToString() ?? "",
                ImageUrl = imageUrl
            });
        }

        return View(heroes); // or return Json(heroes) for API-style
    }

}
