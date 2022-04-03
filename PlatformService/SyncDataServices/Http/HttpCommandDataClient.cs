using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task SendPlatformToCommand(PlatformReadDto plat)
    {
        var httpContect = new StringContent(
            JsonSerializer.Serialize(plat),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync($"{_configuration["CommandService"]}/api/c/platforms", httpContect);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> Sync POST to Command Service was OK");
        }
        else
        {
            Console.WriteLine("--> Sync POST to Command Service was Not OK");
        }
    }
}
