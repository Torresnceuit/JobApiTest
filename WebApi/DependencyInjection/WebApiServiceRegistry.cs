using AppLogic.DependencyInjection;
using Lamar;

namespace WebApi.DependencyInjection;

public class WebApiServiceRegistry : ServiceRegistry
{
    public WebApiServiceRegistry()
    {
        IncludeRegistry<AppLogicServiceRegistry>();
        Scan(scanner =>
        {
            scanner.TheCallingAssembly();
            scanner.WithDefaultConventions(ServiceLifetime.Scoped);
        });
    }
}
