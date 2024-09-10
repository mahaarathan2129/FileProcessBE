using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessConsume
{
    internal class ImportCsv
    {
        public async static Task ImportCsvUsingFilePath(string filePath, string connectionString, string tableName = "Products")
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string headerLine = reader.ReadLine();

                    string[] headers = headerLine.Split(',');
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }

                    while (!reader.EndOfStream)
                    {
                        string[] rows = reader.ReadLine().Split(',');
                        dataTable.Rows.Add(rows);
                    }
                }

                // Insert data into the database using SqlBulkCopy
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection))
                    {
                        sqlBulkCopy.DestinationTableName = tableName;

                        await sqlBulkCopy.WriteToServerAsync(dataTable);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occured at ImportCsv : " + ex.Message);
            }
        }
    }
}
