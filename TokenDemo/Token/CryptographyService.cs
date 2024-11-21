using Newtonsoft.Json;
using NPOI.POIFS.Crypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TokenDemo.Token
{
    public class CryptographyService
    {
        private readonly string KEY = "b14ca5898a4e4142aace2ea2143a2410";
        private readonly string CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqurstuvwxyz0987654321";
        private readonly string BLOCKCIPHER = "AES/CBC/PKCS7PADDING";
        private readonly List<int[]> SWAPINDEXS = new List<int[]>() { new int[] { 2, 4 }, new int[] { 3, 5 } };

        public CryptoModel Encrypt(string plainText)
        {
            var iv = GenerateIV(16);
            return new CryptoModel
            {
                data = Convert.ToBase64String(AESService(plainText,iv,Cipher.ENCRYPT_MODE)),
                iv = Shuffle(iv),
            };
        }

        public string Decrypt(string plainText,string iv)
        {
            return Encoding.UTF8.GetString(AESService(plainText,iv, Cipher.DECRYPT_MODE));
        }


        public string DoCheckSum(string data)
        {
            return CheckSum(JsonConvert.DeserializeObject<Dictionary<string, string>>(data));
        }
        private string CheckSum(Dictionary<string,string> data)
        {
            var sha256 = SHA256.Create();

            /*            var ordered = data.OrderBy(d => d.Key);
                        var concat = string.Concat(ordered.Select(o => o.Key + o.Value));
                        var bike = Encoding.UTF8.GetBytes(concat);
                        var hash = sha256.ComputeHash(bike);
                        var result = BitConverter.ToString(hash).Replace("-",string.Empty);
                        var last = result.ToLower();

                        return last;*/

            return BitConverter
                .ToString(sha256
                .ComputeHash(Encoding.UTF8
                .GetBytes(string
                .Concat(data
                .OrderBy(d => d.Key)
                .Select(a => a.Key + a.Value)))))
                .Replace("-", string.Empty)
                .ToLower();
        }

        private string GenerateIV(int length)
        {
            var characterLength = CHARACTERS.Length;
            var random = new Random();
            return new string(Enumerable.Range(0, length)
                .Select(_ => CHARACTERS[random.Next(characterLength)])
                .ToArray());
        }

        private byte[] AESService(string plainText,string iv,int mode)
        {
            var keySpecification = new SecretKeySpec(Encoding.UTF8.GetBytes(KEY),"AES");
            var ivSpecification = new IvParameterSpec(Encoding.UTF8.GetBytes(iv));
            var cipher = Cipher.GetInstance(BLOCKCIPHER);
            cipher.Init(mode,keySpecification,ivSpecification); 
            if (mode == Cipher.ENCRYPT_MODE)
            {
                return cipher.DoFinal(Encoding.UTF8.GetBytes(plainText));  
            }
            if (mode == Cipher.DECRYPT_MODE)
            {
                return cipher.DoFinal(Convert.FromBase64String(plainText));  
            } 
            throw new ArgumentException("Invalid mode specified for AES operation.");
        }

        public string Shuffle(string iv)
        {
            var charArray = iv.ToCharArray();
            var firstPart = charArray.Take(iv.Length/2).ToArray();
            var secondPart = charArray.Skip(iv.Length/2).ToArray();
            var shuffledPartOne = new string(SwapIndex(firstPart,SWAPINDEXS));
            var shuffledPartTwo = new string(SwapIndex(secondPart,SWAPINDEXS));
            var builder = new StringBuilder();
            builder.Append(shuffledPartOne);
            builder.Append(shuffledPartTwo);
            return builder.ToString(); 
        }

        private char[] SwapIndex(char[] text, List<int[]> indexs)
        {
            indexs.ForEach(i =>
            {
                var temp = text[i[0]];
                text[i[0]] = text[i[1]];
                text[i[1]] = temp;
            });

            return text;
        }
    }
}
