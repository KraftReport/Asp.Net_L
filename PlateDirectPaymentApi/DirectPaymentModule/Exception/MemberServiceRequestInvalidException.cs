using PlateDirectPaymentApi.DirectPaymentModule.Model;

namespace PlateDirectPaymentApi.DirectPaymentModule.Exception
{
    public class MemberServiceRequestInvalidException : System.Exception
    {
        public MemberDTO sampleDTO { get; }
        public MemberServiceRequestInvalidException() { }
        public MemberServiceRequestInvalidException(string message) : base(message)
        {

        }

        public MemberServiceRequestInvalidException(string message, MemberDTO inner) : base(message)
        {
            sampleDTO = inner;
        }

    }
}
