using DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class TechTestDbContext : DbContext
{
    public DbSet<DbClient> Client { get; set; } = null!;
    public DbSet<DbJob> Job { get; set; } = null!;
    public DbSet<DbNote> Note { get; set; } = null!;

    public TechTestDbContext(DbContextOptions<TechTestDbContext> options)
            : base(options) { }

}
