using BuildingBlocks.Infrastructure.RepositoryManager.Dapper.Contracts;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

namespace BuildingBlocks.Infrastructure.RepositoryManager.Dapper.Implementation;

public class BaseQuery : IGetQuery
{
    private readonly IDbConnection _db;      

    public BaseQuery(IConfiguration configuration)
    {
        _db ??= CreateConnection(configuration);
    }

    public T? Get<T>(string query, T? obj, CommandType? commandType = CommandType.StoredProcedure)
        => _db.QueryFirstOrDefault<T?>(query, obj, commandType: commandType);    


    public Task<T?> GetAsync<T>(string query, object? obj = null, CommandType? commandType = null)
        => _db.QueryFirstOrDefaultAsync<T?>(query, obj, commandType: commandType);

    public IEnumerable<T> GetAll<T>(string query, T? obj, int? timeout, CommandType? commandType = CommandType.StoredProcedure)
    {
        var param = new DynamicParameters();
        LoadProps(obj, param);
        return _db.Query<T>(query, param, commandTimeout: timeout, commandType: commandType);
    }

    public Task<IEnumerable<T?>> GetAllAsync<T>(string query, object? obj = null, CommandType? commandType = CommandType.StoredProcedure)
    {
        var param = new DynamicParameters();
        LoadProps(obj, param);
        return _db.QueryAsync<T?>(query, obj, commandType: commandType);
    }

    public IEnumerable<T1> GetAllAsync<T1, T2>(string query, Func<T1, T2, T1> map, object? obj, CommandType? commandType, string splitter)
    {
        return _db.Query(query, map,
        commandType: commandType,
        param: obj,
        splitOn: splitter);
    }

    public Task<IEnumerable<T?>> GetFilterAsync<T>(string query, object? obj = null, CommandType? commandType = CommandType.StoredProcedure, bool includeSortProp = false)
    {
        var param = new DynamicParameters();
        LoadProps(obj, param);
        return _db.QueryAsync<T?>(query, param, commandType: commandType);
    }
    public async Task<T> GetGenericTypeAsync<T>(string query, object? obj, CommandType? commandType = CommandType.StoredProcedure)
        => (T)await _db.QueryAsync<T>(query, obj, commandType: commandType);
    
    public Task<SqlMapper.GridReader> GetMultipleAsync(string query, object? obj, CommandType? commandType = CommandType.StoredProcedure)
        => _db.QueryMultipleAsync(query, obj, commandType: commandType);
       

    private static IDbConnection CreateConnection(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("connectionString");
        return new SqlConnection(connectionString);
    }

    private static void LoadProps<T>(T? obj, DynamicParameters param)
    {
        if (obj is not null)
        {
            foreach (PropertyInfo p in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                param.Add("@" + p.Name, p.GetValue(obj));
            }
        }
    }
}
