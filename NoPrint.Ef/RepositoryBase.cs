using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NoPrint.Framework;
using NoPrint.Framework.Identity;
using NoPrint.Framework.Specification;
using System.Diagnostics.CodeAnalysis;

namespace NoPrint.Ef;

public abstract class RepositoryBase<T, Y> : IRepositoryBase<T, Y>
    where Y : IdentityBase, new()
    where T : Aggregate<Y>
{
    private readonly DbContext _context;

    protected RepositoryBase(DbContext context)
    {
        _context = context;
    }
    public async Task<Y> AddAndSaveAsync(T entity)
    {

        _context.Add(entity);

        await Save();

        return entity.Id;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<T> GetByIdAsync(Y Id, bool track = false)
    {
        if (!track) return await _context.Set<T>().AsNoTracking().SingleAsync(x => x._Id == Id.Id);
        return await _context.Set<T>().SingleAsync(x => x._Id == Id.Id);
    }

    public async Task<T> GetSingleByCondition(IBaseGetSpecification<T> specification)
    {
        return await specification.GetAsync(_context.Set<T>());
    }

    public async Task<List<T>> GetAllByCondition(IBaseGetListSpecification<T> specification)
    {
        return await specification.GetAllAsync(_context.Set<T>());
    }
}

