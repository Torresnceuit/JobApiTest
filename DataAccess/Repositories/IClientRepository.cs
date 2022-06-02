using DataAccess.DataModel;
namespace DataAccess.Repositories;

public interface IClientRepository
{
    public Task<DbClient> CreateClientAsync(DbClient client);
    public Task<DbClient?> GetClientAsync(Guid id);
    public Task<IReadOnlyList<DbClient>> GetClientsAsync(int skip, int take);
    public Task<IReadOnlyList<DbJob>> GetJobsAsync(Guid clientId);
}
