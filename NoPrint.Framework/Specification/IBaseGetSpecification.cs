namespace NoPrint.Framework.Specification;

public interface IBaseGetSpecification<T>
{
    public Task<T> GetAsync(IQueryable<T> queryable);
}