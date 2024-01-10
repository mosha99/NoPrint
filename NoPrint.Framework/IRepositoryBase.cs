using NoPrint.Framework.Identity;

namespace NoPrint.Framework;

public interface IRepositoryBase<T, Y> : IWriteRepositoryBase<T, Y> , IReadRepositoryBase<T, Y>
    where Y : IdentityBase, new()
    where T : Aggregate<Y>
{
}