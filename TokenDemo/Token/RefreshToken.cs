using System;

namespace TokenDemo.Token
{
    public class RefreshToken
    {
        public long id { get; set; }
        public string code { get; set; }
        public DateTime expirationTime { get; set; }
    }
}
