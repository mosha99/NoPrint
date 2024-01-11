using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoPrint.Framework;
using NoPrint.Framework.Validation;

namespace NoPrint.Ef;

public class UnitRepositories
{
    private readonly IServiceProvider _serviceProvider;

    public UnitRepositories(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        Repositories = new List<IWriteRepositoryBase>();
    }

    public List<IWriteRepositoryBase> Repositories { get; set; }

    public TRepo GetRepository<TRepo>() where TRepo : class, IWriteRepositoryBase
    {
        var repo = _serviceProvider.GetRequiredService<TRepo>();

        Repositories.ValidationCheck(x => x.All(y => y.GetDbContextId().Equals(repo.GetDbContextId())), "E1035");

        var findRepo = Repositories.SingleOrDefault(x => x.Equals(repo));

        if (findRepo is not null) return findRepo as TRepo;

        Repositories.Add(repo);

        return repo;
    }

    public async Task SaveChangeAsync()
    {
        await Repositories.First().Save();
    }
}