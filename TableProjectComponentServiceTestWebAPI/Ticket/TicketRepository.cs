
using Microsoft.Data.SqlClient;
using QRCoder;
using System.Data;
using System.Drawing.Imaging;

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
                var command = new SqlCommand(
                    "INSERT INTO Users (Name, Email) VALUES (@Name, @Email); SELECT CAST(SCOPE_IDENTITY() as int);",
                    connection
                );
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                await command.ExecuteScalarAsync();
                return 1;
            }
        }

        public async Task InsertPhoto(int userId,byte[] imageData)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("insert into Photos (UserId,ImageData) values (@UserId,@ImageData);", connection);
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@ImageData", imageData);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<byte[]> GetImageData(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT ImageData FROM Photos WHERE UserId = @UserId;", connection);
                command.Parameters.AddWithValue("@UserId", userId);

                var result = await command.ExecuteScalarAsync();
                if (result != null && result is byte[] imageData)
                {
                    return imageData;
                }
                else
                {
                    throw new Exception("No image data found or data is not in expected format.");
                }
            }
        }



        public string generateQrCodeString(string data)
        {
            using(var qrCodeGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrCodeGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
                using(var qrCode = new QRCode(qrCodeData))
                {
                    using(var bitmap = qrCode.GetGraphic(20))
                    {
                        using (var stream = new MemoryStream())
                        {
                            bitmap.Save(stream,ImageFormat.Png);
                            return Convert.ToBase64String(stream.ToArray());
                        }
                    }
                }
            }
        }

        public byte[] generateQrCodeByte(string data)
        {
            using(var qrCodeGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrCodeGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
                using (var qrCode = new QRCode(qrCodeData))
                {
                    using (var bitmap = qrCode.GetGraphic(20))
                    {
                        using(var stream = new  MemoryStream())
                        {
                            bitmap.Save(stream, ImageFormat.Png);
                            return stream.ToArray();
                        }
                    }

                }
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
            return dataTable;
        }
        
        public async Task<List<Ticket>> GetPaginatedTickets(int pageSize, int pageNum)
        {
            var tickets = new List<Ticket>();
            using(var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = new SqlCommand("SELECT * FROM Tickets ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY", connection);
                query.Parameters.AddWithValue("@Offset", (pageNum - 1) * pageSize);
                query.Parameters.AddWithValue("@PageSize", pageSize);
                var result = await query.ExecuteReaderAsync();
                while(await result.ReadAsync())
                { 
                    var ticket = new Ticket();
                    ticket.EventName = result.GetString(result.GetOrdinal("EventName"));
                    tickets.Add(ticket);
                }   
            }
            return  tickets;
        }
    }
}
