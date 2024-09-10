using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FileProcessingApp.Common.Dapper
{
    public class DapperContext : IDapperContext, IDisposable
    {
        private readonly IDbConnection _connection;

        public DapperContext(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IReadOnlyList<T>> QueryAsync<T>(
            string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default
        )
        {
            return (await _connection.QueryAsync<T>(sql, param, transaction)).AsList();
        }

        public async Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, TResult>(
            string sql,
            Func<T1, T2, TResult> map,
            object? param = null,
            IDbTransaction? transaction = null,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = CommandType.StoredProcedure,
            CancellationToken cancellationToken = default
        )
        {
            return await _connection.QueryAsync(sql, map, param, transaction, true, splitOn, commandTimeout, commandType);
        }

        public async Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, T3, TResult>(
            string sql,
            Func<T1, T2, T3, TResult> map,
            object? param = null,
            IDbTransaction? transaction = null,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = CommandType.StoredProcedure,
            CancellationToken cancellationToken = default
        )
        {
            return await _connection.QueryAsync(sql, map, param, transaction, true, splitOn, commandTimeout, commandType);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(
            string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default
        )
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }

        public async Task<T> QuerySingleAsync<T>(
            string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default
        )
        {
            return await _connection.QuerySingleAsync<T>(sql, param, transaction);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(
            string sql,
            object? param = null,
            CommandType commandType = CommandType.Text,
            CancellationToken cancellationToken = default
        )
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(
                sql,
                param,
                null,
                null,
                commandType
            );
        }

        public async Task<IEnumerable<T>> QueryAllMapAsync<T>(
            string sql,
            CommandType commandType,
            object? param = null,
            CancellationToken cancellationToken = default
        )
        {
            return (await _connection.QueryAsync<T>(sql, param, null, null, commandType)).AsList();
        }

        public async Task<T> ExecuteScalarAsync<T>(
            string sql,
            object param = null,
            CommandType commandType = CommandType.Text
        )
        {
            return await _connection.ExecuteScalarAsync<T>(sql, param, null, null, commandType);
        }

        public async Task<int> ExecuteAsync(
            string sql,
            object param = null,
            CommandType commandType = CommandType.Text
        )
        {
            return await _connection.ExecuteAsync(sql, param, null, null, commandType);
        }

        public string BulkCopy(string sql, DataTable dataTable)
        {
            try
            {
                using (var bulkCopy = new SqlBulkCopy(_connection.ConnectionString))
                {
                    bulkCopy.DestinationTableName = sql;
                    bulkCopy.WriteToServer(dataTable);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
