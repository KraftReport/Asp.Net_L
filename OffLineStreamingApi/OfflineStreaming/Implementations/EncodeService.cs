using OffLineStreamingApi.OfflineStreaming.Abstractions;
using System.Security.Cryptography; 

namespace OffLineStreamingApi.OfflineStreaming.Implementations
{

    public class EncodeService : Encoder
    {
        public override byte[] GenerateKey(string hex, string credentials)
        {
            var byteArray = new byte[hex.Length/2];

            for(int i = 0; i< hex.Length; i+=2)
            {
                byteArray[i/2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            byte[] hashByte ;

            using (var hmac = new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(credentials)))
            {
                hashByte = hmac.ComputeHash(byteArray);
            };

            var result = new byte[16];

            Array.Copy(hashByte, result, 16);

            return result;
        }
    }
}
