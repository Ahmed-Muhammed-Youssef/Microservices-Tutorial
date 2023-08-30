using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.Properties.Http
{
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
            var httpContent = new StringContent(JsonSerializer.Serialize(plat), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_configuration["CommandService"], httpContent);
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> post request sent to the command service successfully.");
            }
            else
            {
                System.Console.WriteLine("--> post request failed");
            }
        }
    }
}