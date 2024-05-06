using Dapper;
using System.Data;

namespace BuildingBlocks.Infrastructure.RepositoryManager.Dapper.Contracts;

public interface IGetQuery
{
    T? Get<T>(string query, T? obj, CommandType? commandType = CommandType.StoredProcedure);
    Task<T?> GetAsync<T>(string query, object? obj = null, CommandType? commandType = null);
    IEnumerable<T> GetAll<T>(string query, T? obj, int? timeout, CommandType? commandType = CommandType.StoredProcedure);
    Task<IEnumerable<T?>> GetFilterAsync<T>(string query, object? obj = null, CommandType? commandType = CommandType.StoredProcedure, bool includeSortProp = false);
    Task<IEnumerable<T?>> GetAllAsync<T>(string query, object? obj = null, CommandType? commandType = CommandType.StoredProcedure);
    IEnumerable<T1> GetAllAsync<T1, T2>(string query, Func<T1, T2, T1> map, object? obj, CommandType? commandType, string splitter);
    Task<SqlMapper.GridReader> GetMultipleAsync(string query, object? obj, CommandType? commandType = CommandType.StoredProcedure);
    Task<T> GetGenericTypeAsync<T>(string query, object? obj, CommandType? commandType = CommandType.StoredProcedure);
}
