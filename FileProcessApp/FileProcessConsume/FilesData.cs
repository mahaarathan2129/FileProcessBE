using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessConsume
{
    internal class FilesData
    {
        private readonly string _connString;

        public FilesData(string _connString)
        {
            this._connString = _connString;
        }
        public bool UpdateStatusByFileName(long id, string fileName,string status,string filePath)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                // Define the SQL Update command
                string updateQuery = @"Exec ModifyFileStatus @FileName,@FilePath,@Status,@Id";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@FileName", fileName);
                    cmd.Parameters.AddWithValue("@FilePath", filePath);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
    }
}
