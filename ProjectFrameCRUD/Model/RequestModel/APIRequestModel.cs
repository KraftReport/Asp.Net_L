namespace ProjectFrameCRUD.Model.RequestModel
{
    public class APIRequestModel
    {
        public int Id { get; set; }
        
        public BookModel? Book { get; set; }
        public UserModel? User { get; set; }
        public string? token { get; set; }
        public List<UserModel> Users { get; set; }
        public List<BookModel>? Books { get; set; }
    }
}
