
using Microsoft.Data.SqlClient;
using System.Data;

namespace TableProjectComponentServiceTestWebAPI.Ticket
{
    public class TicketRepository
    {
        private readonly string connectionString;

        public TicketRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("test");
        }

        public async Task<int> InsertUserAsync(string name ,string email)
        {
            using(var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("insert into Users (Name,Email) values (@Name,@Email); select SCOPE_IDENTITY();");
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task BulkTicketInsert(DataTable ticketDataTable)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using(var bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "Tickets";
                    await bulkCopy.WriteToServerAsync(ticketDataTable);
                }
            }
        }

        public DataTable GetDataTable()
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("UserId",typeof(int));
            dataTable.Columns.Add("EventName",typeof(string));
            dataTable.Columns.Add("EventDate",typeof(DateTime));
            dataTable.Columns.Add("QrCode",typeof (string));
            return dataTable;
        }
    }
}
