namespace CustomCookieAuth.Attributes
{
    public class LogHtokeMalAttribute : Attribute
    {
        public string message {  get; set; }

        public LogHtokeMalAttribute(string message)
        {
            this.message = message;
        }
    }
}
