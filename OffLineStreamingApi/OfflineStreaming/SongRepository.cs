using MySql.Data.MySqlClient;

namespace OffLineStreamingApi.OfflineStreaming
{
    public class SongRepository
    {
        private readonly String connectionString;
        private readonly MySqlConnection connection;
        private readonly string insertQuery = "insert into song (name) values (@name)";

        public SongRepository(IConfiguration configuration)
        {
            this.connectionString = configuration.GetConnectionString("dev-msp");
            connection = new MySqlConnection(connectionString);
            
        }

        public async Task<bool> SaveSong(string name)
        {
            using (connection)
            {
                await connection.OpenAsync(); 

                using(var sqlCommand = new MySqlCommand(insertQuery, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@name", name);

                    return await sqlCommand.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}
