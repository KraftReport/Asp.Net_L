using ProjectFrameCRUD.Model.RequestModel;

namespace ProjectFrameCRUD.Model.ResponseModel
{
    public class APIResponseModel
    {
        public int Id { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public BookModel? Book { get; set; }
        public UserModel? User { get; set; }
        public List<UserModel>? Users { get; set; }  
        public List<BookModel>? Books { get; set; }
    }
}
