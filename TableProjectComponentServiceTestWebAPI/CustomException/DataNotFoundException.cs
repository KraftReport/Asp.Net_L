namespace TableProjectComponentServiceTestWebAPI.CustomException
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException() { }

        public DataNotFoundException(string message) : base(message) { }
    }
}
