using MySql.Data.MySqlClient;

namespace CKeditor.CKEditorModule
{
    public class DatabaseConnector
    {
        private readonly IConfiguration configuration;
        private readonly MySqlConnection mySqlConnection;

        public DatabaseConnector(IConfiguration configuration)
        {
            this.configuration = configuration;
            mySqlConnection = new MySqlConnection(configuration.GetConnectionString("msp-dev"));
        }

        public MySqlConnection GetConnection()
        {
            return mySqlConnection;
        }

    }
}
