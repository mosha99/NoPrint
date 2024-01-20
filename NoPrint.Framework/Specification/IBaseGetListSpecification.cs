using NoPrint.Application.Infra;

namespace NoPrint.Framework.Specification;

public interface IBaseGetListSpecification<T>
{
    public Task<ListResult<T>> GetAllAsync(IQueryable<T> queryable);
}
