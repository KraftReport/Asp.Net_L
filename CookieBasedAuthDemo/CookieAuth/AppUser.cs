using System.ComponentModel.DataAnnotations;

namespace CookieBasedAuthDemo.CookieAuth
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
