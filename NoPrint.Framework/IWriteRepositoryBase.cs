using NoPrint.Framework.Identity;

namespace NoPrint.Framework;

public interface IWriteRepositoryBase<T, Y>
    where Y : IdentityBase, new()
    where T : Aggregate<Y>
{
    public Task<Y> AddAsync(T entity);
    public Task Save();
}