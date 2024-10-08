using MySql.Data.MySqlClient;

namespace CKeditor.CKEditorModule
{
    public class GenericDAO<T> : IGenricRepository<T> where T : class
    {
        private readonly MySqlConnection connection;
        private readonly string createSql = "insert into article (Title,Description) values (@Title,@Description)";

        public GenericDAO(DatabaseConnector databaseConnector)
        {
            connection = databaseConnector.GetConnection();
        }

        public bool InsertRecord(T entity)
        {
            connection.Open();
            var cmd = CreateMysqlCommand(createSql, entity);
            return cmd.ExecuteNonQuery() > 0; 
        }

        private MySqlCommand CreateMysqlCommand(string sql,T entity)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = System.Data.CommandType.Text;
            return MapParameters(cmd,CreateMapping(entity));
        }

        private MySqlCommand MapParameters(MySqlCommand mySqlCommand,Dictionary<string,object> parameters)
        {
            parameters.ToList().ForEach(para =>
            {
                mySqlCommand.Parameters.AddWithValue("@" + para.Key, para.Value ?? DBNull.Value);
            });
            return mySqlCommand;
        }

        private Dictionary<string,object> CreateMapping(T entity)
        { 
            return typeof(T).GetProperties()
                       .ToDictionary(prop => prop.Name, prop =>
                       prop.GetValue(entity) ?? DBNull.Value);
        }
    }
}
