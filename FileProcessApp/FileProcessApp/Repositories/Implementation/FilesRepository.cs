using Dapper;
using FileProcessApp.Common;
using FileProcessingApp.Common.Dapper;
using FileProcessingApp.Models.Entities;
using FileProcessingApp.Repositories.Interface;
using System.Data;
using System.Data.Common;

namespace FileProcessingApp.Repositories.Implementation
{
    public class FilesRepository : IFilesRepository
    {
        private readonly IDapperContext _dapperContext;

        public FilesRepository(IDapperContext _dapperContext)
        {
            this._dapperContext = _dapperContext;
        }

        public async Task<IEnumerable<Files>> GetFilesForUserAsync()
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", HttpContextHelper.GetClaimsValueByKey("UserId"), DbType.Int64);

            return await _dapperContext.QueryAllMapAsync<Files>(
                "GetFilesByUserId",
                CommandType.StoredProcedure,
                param:parameters
            );
        }

        public async Task<long> AddFileAsync(Files file)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FilePath", file.FilePath);
            parameters.Add("@FileName", file.FileName);
            parameters.Add("@FileSize", file.FileSize);
            parameters.Add("@CreatedBy", HttpContextHelper.GetClaimsValueByKey("UserId"));
            parameters.Add("@UserId", HttpContextHelper.GetClaimsValueByKey("UserId"));

            long newFileId = await _dapperContext.QueryFirstOrDefaultAsync<long>(
                "AddFile",
                parameters
            );

            return newFileId;
        }
    }
}
