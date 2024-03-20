using Microsoft.Extensions.DependencyInjection;
using OnionAPI.Application.Interfaces.AutoMapper;

namespace OnionAPI.Mapper;

public static class Registration
{
    public static void AddCustomMapper(this IServiceCollection services)
    {
        services.AddSingleton<IMapper,AutoMapper.Mapper>();
    }
}
