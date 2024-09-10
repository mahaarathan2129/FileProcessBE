using System.Data;

namespace FileProcessingApp.Common.Dapper
{
    public interface IDapperContext
    {
        Task<T> ExecuteScalarAsync<T>(
            string sql,
            object? param = null,
            CommandType commandType = CommandType.Text
        );

        Task<int> ExecuteAsync(string sql, object? param = null, CommandType commandType = CommandType.Text);

        Task<IReadOnlyList<T>> QueryAsync<T>(
            string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default
        );
        Task<IEnumerable<T>> QueryAllMapAsync<T>(
          string sql,
          CommandType commandType,
          object? param = null,
          CancellationToken cancellationToken = default
      );

        Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, TResult>(
            string sql,
            Func<T1, T2, TResult> map,
            object? param = null,
            IDbTransaction? transaction = null,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = CommandType.StoredProcedure,
            CancellationToken cancellationToken = default
        );

        Task<IEnumerable<TResult>> QueryMapAsync<T1, T2, T3, TResult>(
            string sql,
            Func<T1, T2, T3, TResult> map,
            object? param = null,
            IDbTransaction? transaction = null,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = CommandType.StoredProcedure,
            CancellationToken cancellationToken = default
        );

        Task<T> QueryFirstOrDefaultAsync<T>(
            string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default
        );

        Task<T> QuerySingleAsync<T>(
            string sql,
            object? param = null,
            IDbTransaction? transaction = null,
            CancellationToken cancellationToken = default
        );

        public string BulkCopy(string sql, DataTable dataTable);
    }
}
