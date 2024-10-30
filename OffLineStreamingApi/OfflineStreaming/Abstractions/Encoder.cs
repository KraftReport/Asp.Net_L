using System.Text;

namespace OffLineStreamingApi.OfflineStreaming.Abstractions
{
    public abstract class Encoder
    {
        public abstract byte[] GenerateKey(string hex, string credentials);
        public string GetHexString(string input,int desireLength)
        {
            var byteArray = Encoding.UTF8.GetBytes(input);
            var hex = BitConverter.ToString(byteArray).Replace("-","").ToLower();

            if ( hex.Length > desireLength )
            {
                return hex.Substring(0, desireLength);
            }

            if(hex.Length < desireLength )
            {
                return hex.PadRight(desireLength,'0');
            }

            return hex;
        }
    }
}
