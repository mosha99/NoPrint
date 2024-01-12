using NoPrint.Framework.Identity;
using NoPrint.Framework.Specification;

namespace NoPrint.Framework;

public interface IReadRepositoryBase<T, Y>
    where Y : IdentityBase, new()
    where T : Aggregate<Y>
{
    public Task<T> GetByIdAsync(Y shopId, bool track = false);
    public Task<T?> GetSingleByCondition(IBaseGetSpecification<T> specification);
    public Task<List<T>> GetAllByCondition(IBaseGetListSpecification<T> specification);
}