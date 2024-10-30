using OffLineStreamingApi.OfflineStreaming.Abstractions;

namespace OffLineStreamingApi.OfflineStreaming.Implementations
{
    public class KeyService : KeyManager
    {
        private readonly Encoder encoder;
        public KeyService(Encoder encoder)
        {
            this.encoder = encoder;
        }
        public byte[] GenerateKey(string input, string credential)
        { 
            return encoder.GenerateKey(encoder.GetHexString(input, 32), credential); 
        }

        public void GenerateKeyFilePath(string filePath, byte[] key)
        {
            Directory.CreateDirectory(filePath);
            var keyFile = $"{ConstUtility.keyFileName}{ConstUtility.keyFileExtension}";
            var keyFilePath = Path.Combine(filePath, keyFile);
            if(File.Exists(keyFilePath))
            {
               File.Delete(keyFilePath); 
            }
            File.WriteAllBytes(keyFilePath,key);
        }

        public void GenerateKeyInfoFilePath(string filePath)
        {
            var key = $"{ConstUtility.keyFileName}{ConstUtility.keyFileExtension}";
            var keyInfo = $"{ConstUtility.keyFileName}{ConstUtility.keyinfoFileExtension}";
            if (File.Exists(keyInfo))
            {
                File.Delete(keyInfo);
            }
            File.WriteAllText(keyInfo,$"{key}\n{key}");
        }
    }
}
