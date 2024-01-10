namespace NoPrint.Framework.Specification;

public interface IBaseGetListSpecification<T>
{
    public Task<List<T>> GetAllAsync(IQueryable<T> queryable);
}