using PlateDirectPaymentApi.DirectPaymentModule.Model;

namespace PlateDirectPaymentApi.DirectPaymentModule.Exception
{
    public class PaymentServiceRequestInvalidException : System.Exception
    {
        public PaymentDTO sampleDTO;

        public PaymentServiceRequestInvalidException() { }
        public PaymentServiceRequestInvalidException(string message) : base(message) { }
       
    }
}
