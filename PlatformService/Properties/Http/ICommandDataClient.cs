using PlatformService.Dtos;

namespace PlatformService.Properties.Http
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommand(PlatformReadDto plat);
    }
}