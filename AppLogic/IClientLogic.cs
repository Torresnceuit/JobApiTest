using ServiceModel;

namespace AppLogic;

public interface IClientLogic
{
    public Task<Client> CreateClientAsync(Client client);
    public Task<Client> GetClientAsync(Guid id);
    public Task<IEnumerable<Client>> GetClientsAsync(int skip, int take);
    public Task<IEnumerable<Job>> GetJobsAsync(Guid clientId);
}
