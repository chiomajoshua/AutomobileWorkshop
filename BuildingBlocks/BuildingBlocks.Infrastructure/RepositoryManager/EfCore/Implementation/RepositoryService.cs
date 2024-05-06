using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Domain.Models.Responses;
using BuildingBlocks.Infrastructure.DatabaseContext;
using BuildingBlocks.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;

public class RepositoryService<TEntity> : IRepositoryService<TEntity>
    where TEntity : class
{
    private readonly AutomobileDbContext _context;

    public RepositoryService(AutomobileDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }     

    public async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> filter)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (filter is not null)
        {
            query = query.Where(filter);
        }
        return await query.FirstOrDefaultAsync();
    } 
    
    public async Task<List<TEntity>> GetAsync(
                                Expression<Func<TEntity, bool>> filter,
                                Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
                                Expression<Func<TEntity, object>>? orderBy = null)
    {
        return await QueryableBase.ExtendIncludes(includeFunc).ExtendWhere(filter).ExtendOrderBy(orderBy).ToListAsync();
    }
    public async Task<PageData<TEntity>> GetPagedAsync(
                                PaginationModel pagination,
                                Expression<Func<TEntity, bool>> filter,
                                Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
                                string orderBy = "",
                                CancellationToken token = default)
    {
        var query = QueryableBase.ExtendIncludes(includeFunc).ExtendWhere(filter);
        var count = await query.CountAsync();
        var data = await query.ExtendOrderBy(orderBy).ExtendPagination(pagination).ToListAsync(token);

        return new PageData<TEntity> { TotalCount = count, EntityData = data };
    }
    public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter, bool trackChanges = false, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null)
    {
        if (!trackChanges)
        {
            return await _context.Set<TEntity>().AsNoTracking().ExtendIncludes(includeFunc).ExtendWhere(filter).FirstOrDefaultAsync();
        }

        return await GetByKeyAsync(filter, includeFunc);
    }
    public async Task<TEntity?> GetByKeyAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null)
    {
        return await QueryableBase.ExtendIncludes(includeFunc).ExtendWhere(filter).FirstOrDefaultAsync();
    }

    public async Task<int> CreateAsync(TEntity model) 
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model));

        await _context.Set<TEntity>().AddAsync(model);
        return await _context.SaveChangesAsync();
    }
    public async Task<TEntity?> InsertAsync(TEntity model, CancellationToken token = default)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        await _context.Set<TEntity>().AddAsync(model);
        await _context.SaveChangesAsync(token);

        return model;
    }
    public async Task<bool> InsertBulkAsync(List<TEntity> model, CancellationToken token = default)
    {
        if (model is not null)
            throw new ArgumentNullException(nameof(model));

        await _context.Set<List<TEntity>>().AddRangeAsync(model);

        return await _context.SaveChangesAsync(token) > 0;
    }
    public async Task<bool> UpdateAsync(TEntity model, CancellationToken token = default)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model));

        _context.Set<TEntity>().Update(model);

        return await _context.SaveChangesAsync(token) > 0;
    }
    public async Task<bool> UpdateEntityAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    private IQueryable<TEntity> QueryableBase => _context.Set<TEntity>().AsNoTracking().AsQueryable();
}

