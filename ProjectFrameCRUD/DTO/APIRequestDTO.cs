namespace ProjectFrameCRUD.DTO
{
    public class APIRequestDTO
    {
        public int Id { get; set; }
        public BookDTO? Book { get; set; }
        public List<BookDTO>? Books { get; set; }
    }
}
