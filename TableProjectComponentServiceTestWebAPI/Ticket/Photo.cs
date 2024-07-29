namespace TableProjectComponentServiceTestWebAPI.Ticket
{
    public class Photo
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] ImageData { get; set; }

        public User User { get; set; }
    }
}
