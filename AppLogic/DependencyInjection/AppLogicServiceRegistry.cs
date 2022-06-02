using DataAccess.DependencyInjection;
using Lamar;
using Microsoft.Extensions.DependencyInjection;

namespace AppLogic.DependencyInjection;

public class AppLogicServiceRegistry : ServiceRegistry
{
    public AppLogicServiceRegistry()
    {
        IncludeRegistry<DataAccessServiceRegistry>();
        Scan(scanner =>
        {
            scanner.TheCallingAssembly();
            scanner.WithDefaultConventions(ServiceLifetime.Scoped);
        });
    }
}
