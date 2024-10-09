using MySql.Data.MySqlClient;

namespace CKeditor.CKEditorModule
{
    public class GenericDAO<T> : IGenricRepository<T> where T : class
    {
        private readonly MySqlConnection connection;
        private readonly string createSql = "insert into article (Title,Description) values (@Title,@Description)";
        private readonly string allSql = "select * from article";

        public GenericDAO(DatabaseConnector databaseConnector)
        {
            connection = databaseConnector.GetConnection();
        }

        public List<T> GetRecords()
        {
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = allSql;
            var reader = cmd.ExecuteReader();
            var entities = new List<T>();
            while (reader.Read())
            {
                T entity = MapToEntity(reader);
                entities.Add(entity);

            }
            reader.Close();
            connection.Close();
            return entities;
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

   private T MapToEntity(MySqlDataReader reader)
    {
        T entity = Activator.CreateInstance<T>();
        var properties = typeof(T).GetProperties();

            properties.ToList().ForEach(prop =>
            {
                if (!reader.IsDBNull(reader.GetOrdinal(prop.Name))){
                    prop.SetValue(entity, reader[prop.Name]);
                }
            });
 

        return entity;
    }
    }
}
