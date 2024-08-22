using CustomCookieAuth.Attributes;

namespace CustomCookieAuth.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        [LogHtokeMal("this is username")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public ROLE Role { get; set; }
    }
}
