using DMT.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DMT.RequestsSender;

public static class Bootstrapper
{
    public static IServiceCollection AddRequestSender(this IServiceCollection services)
    {
        return services
            .AddScoped<IRequestSender, RequestSender>();
    }
}