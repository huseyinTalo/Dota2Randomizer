using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static async Task Main()
    {
        string folderPath = "dota2_heroes";
        Directory.CreateDirectory(folderPath);

        using var client = new HttpClient();

        // Step 1: Get hero data from OpenDota API
        var heroDataUrl = "https://api.opendota.com/api/heroes";
        var response = await client.GetStringAsync(heroDataUrl);
        var heroes = JArray.Parse(response);

        foreach (var hero in heroes)
        {
            string name = hero["localized_name"].ToString();
            string apiName = hero["name"].ToString().Replace("npc_dota_hero_", "");
            string imageUrl = $"https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/heroes/{apiName}.png";

            try
            {
                var imageBytes = await client.GetByteArrayAsync(imageUrl);
                string filePath = Path.Combine(folderPath, $"{apiName}.png");
                await File.WriteAllBytesAsync(filePath, imageBytes);
                Console.WriteLine($"Downloaded: {name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to download {name}: {ex.Message}");
            }
        }

        Console.WriteLine("Download complete.");
    }
}
