namespace PlateDirectPaymentApi.DirectPaymentModule.Exception
{
    public class TransactionServiceRequestInvalidException : System.Exception
    {
        public TransactionServiceRequestInvalidException() { }
        public TransactionServiceRequestInvalidException(string message) : base(message) { }
    }
}
