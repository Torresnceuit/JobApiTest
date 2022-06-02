using DataAccess.DataModel;
using DataAccess.Repositories;
using ServiceModel;

namespace AppLogic;

public class ClientLogic : IClientLogic
{
    private readonly IClientRepository _clientRepo;
    private readonly IObjectMapper _mapper;

    public ClientLogic(IClientRepository clientRepo, IObjectMapper mapper)
    {
        _clientRepo = clientRepo ?? throw new ArgumentNullException(nameof(clientRepo));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Client> CreateClientAsync(Client client)
    {
        if (client is null)
        {
            throw new ArgumentNullException(nameof(client));
        }

        var dbObject = _mapper.Map<Client, DbClient>(client);
        var result = await _clientRepo.CreateClientAsync(dbObject).ConfigureAwait(false);
        return _mapper.Map<DbClient, Client>(result);
    }

    public async Task<Client> GetClientAsync(Guid id)
    {
        var result = await _clientRepo.GetClientAsync(id).ConfigureAwait(false);
        return _mapper.Map<DbClient, Client>(result);
    }

    public async Task<IEnumerable<Client>> GetClientsAsync(int skip = 0, int take = 100)
    {
        var result = await _clientRepo.GetClientsAsync(skip, take).ConfigureAwait(false);
        return _mapper.Map<DbClient, Client>(result);
    }
    public async Task<IEnumerable<Job>> GetJobsAsync(Guid clientId)
    {
        var result = await _clientRepo.GetJobsAsync(clientId).ConfigureAwait(false);
        return _mapper.Map<DbJob, Job>(result);
    }
}
