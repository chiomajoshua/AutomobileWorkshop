using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Domain.Models.Responses;
using System.Linq.Expressions;

namespace BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;

public interface IRepositoryService<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAsync(
                            Expression<Func<TEntity, bool>> filter,
                            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
                            Expression<Func<TEntity, object>>? orderBy = null);
    Task<PageData<TEntity>> GetPagedAsync(
                            PaginationModel pagination,
                            Expression<Func<TEntity, bool>> filter,
                            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
                            string orderBy = "",
                            CancellationToken token = default);
    Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter, bool trackChanges = false, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null);
    Task<TEntity?> GetByKeyAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null);
    Task<TEntity?> InsertAsync(TEntity model, CancellationToken token = default);
    Task<bool> InsertBulkAsync(List<TEntity> model, CancellationToken token = default);
    Task<bool> UpdateAsync(TEntity model, CancellationToken token = default);
    Task<int> CreateAsync(TEntity model);
    Task<bool> UpdateEntityAsync(TEntity entity);
    Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> filter);
}