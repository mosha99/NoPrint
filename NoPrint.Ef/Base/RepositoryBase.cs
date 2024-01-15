using Microsoft.EntityFrameworkCore;
using NoPrint.Framework;
using NoPrint.Framework.Identity;
using NoPrint.Framework.Specification;

namespace NoPrint.Application.Ef.Base;

public abstract class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId>
    where TId : IdentityBase, new()
    where TEntity : Aggregate<TId>
{
    protected readonly DbContext _context;

    protected RepositoryBase(DbContext context)
    {
        _context = context;
    }

    protected async Task<TId> GenerateId()
    {
        var sequenceName = IdentityBase.GetSequenceBase<TId>();

        string x2 = $"DECLARE @seq BIGINT = NEXT VALUE FOR [" +sequenceName+"]; select @seq;";

        var id = await _context.Database.SqlQueryRaw<long>(x2).ToListAsync();

        return new() { Id = id.Single()};
    }

    public async Task<TId> AddAsync(TEntity entity)
    {
        entity.Id = await GenerateId();
        _context.Add(entity);
        return entity.Id;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public Guid GetDbContextId() => _context.ContextId.InstanceId;
    public async Task<TEntity> GetByIdAsync(TId Id, bool track = false)
    {
        if (!track) return await _context.Set<TEntity>().AsNoTracking().SingleAsync(x => x._Id == Id.Id);
        return await _context.Set<TEntity>().SingleAsync(x => x._Id == Id.Id);
    }

    public async Task<TEntity> GetSingleByCondition(IBaseGetSpecification<TEntity> specification)
    {
        return await specification.GetAsync(_context.Set<TEntity>());
    }

    public async Task<List<TEntity>> GetAllByCondition(IBaseGetListSpecification<TEntity> specification)
    {
        return await specification.GetAllAsync(_context.Set<TEntity>());
    }
}

