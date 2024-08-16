using Microsoft.Data.SqlClient;

namespace TableProjectComponentServiceTestWebAPI.Base64Demo
{
    public class CommonFileService
    {
        private readonly string connectionString;
        public CommonFileService(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("test");
        }

        public async Task<bool> UploadFile(string name, IFormFile file)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var sql = "insert into Files(Name,Data) values (@name,@data)";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    using (var memory = new MemoryStream())
                    {
                        await file.CopyToAsync(memory);
                        command.Parameters.AddWithValue("@data", memory.ToArray());
                    }

                    await command.ExecuteNonQueryAsync();
                    return true;
                }
            }
        }

        public async Task<byte[]> downloadFile(string filename)
        {
            byte[] byteArr;
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "select Data from Files where Name = @name";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", filename);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                          byteArr = (byte[])reader["Data"];
                    
                    }
                }
            }
            return byteArr;
        }
    }
}
