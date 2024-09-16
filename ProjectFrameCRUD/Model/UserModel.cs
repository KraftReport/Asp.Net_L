namespace ProjectFrameCRUD.Model
{
    public class UserModel
    { 

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
