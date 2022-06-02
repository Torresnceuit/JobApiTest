using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AppLogic.Tests.XUnit;
public class ConnectionFactory : IDisposable
{
    private bool disposedValue = false;

    public TechTestDbContext CreateContextForInMemory()
    {
        var option = new DbContextOptionsBuilder<TechTestDbContext>().UseInMemoryDatabase(databaseName: "Test_Database_" + Guid.NewGuid().ToString()).Options;

        var context = new TechTestDbContext(option);
        if(context != null)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        return context;
    }


    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }
}
