using Lamar;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.DependencyInjection;

public class DataAccessServiceRegistry : ServiceRegistry
{
    public DataAccessServiceRegistry()
    {
        Scan(scanner =>
        {
            scanner.TheCallingAssembly();
            scanner.WithDefaultConventions(ServiceLifetime.Scoped);
        });
    }
}
