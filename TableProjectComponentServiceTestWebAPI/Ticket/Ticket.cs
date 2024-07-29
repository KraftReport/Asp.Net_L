namespace TableProjectComponentServiceTestWebAPI.Ticket
{
    public class Ticket
    {
        public int UserId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string QrCode { get; set; }

        public User User { get; set; }
    }
}
