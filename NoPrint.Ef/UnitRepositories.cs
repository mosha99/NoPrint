using Microsoft.Extensions.DependencyInjection;
using NoPrint.Framework;

namespace NoPrint.Application.Ef;

public class UnitRepositories
{
    private readonly IServiceProvider _serviceProvider;

    public UnitRepositories(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        RepositoryBases = new Dictionary<Guid, IWriteRepositoryBase>();
    }
    
    public Dictionary<Guid, IWriteRepositoryBase> RepositoryBases { get; set; }
    public TRepo GetRepository<TRepo>() where TRepo : class, IWriteRepositoryBase
    {
        var repo = _serviceProvider.GetRequiredService<TRepo>();

        if (!RepositoryBases.Any(x => x.Key.Equals(repo.GetDbContextId())))
            RepositoryBases.Add(repo.GetDbContextId(), repo);

        return repo;
    }

    public async Task SaveChangeAsync()
    {
        foreach (var writeRepository in RepositoryBases)
        {
            await writeRepository.Value.Save();
        }
    }
}