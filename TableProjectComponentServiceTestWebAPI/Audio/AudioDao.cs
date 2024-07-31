using Dapper;
using Microsoft.Data.SqlClient;

namespace TableProjectComponentServiceTestWebAPI.Audio
{
    public class AudioDao
    {
        private readonly string connectionString;

        public AudioDao(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("test");
        }

        public async Task<int> saveAudio(AudioFile audioFile)
        {
            using(var connection = new SqlConnection(connectionString))
            {
                var query = "INSERT INTO AudioFiles (FileName, FilePath) VALUES (@FileName, @FilePath); SELECT CAST(SCOPE_IDENTITY() as int)";

                return await connection.QuerySingleAsync<int>(query, audioFile);
            }
        }

        public async Task<AudioFile> GetAudioFileAsync(int Id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "select * from AudioFiles where Id = @Id";
                return await connection.QuerySingleOrDefaultAsync<AudioFile>(query, new { Id = Id });
            }
        }


    }
}
