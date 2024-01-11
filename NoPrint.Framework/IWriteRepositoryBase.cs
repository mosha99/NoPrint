using NoPrint.Framework.Identity;

namespace NoPrint.Framework;

public interface IWriteRepositoryBase<T, Y> : IWriteRepositoryBase
    where Y : IdentityBase, new()
    where T : Aggregate<Y>
{
    public Task<Y> AddAsync(T entity);
}
public interface IWriteRepositoryBase
{
    public Task Save();
    public Guid GetDbContextId();
}