namespace OffLineStreamingApi.OfflineStreaming.Abstractions
{
    public interface KeyManager
    {
        public byte[] GenerateKey(string input, string credential);
        public void GenerateKeyFilePath(string filePath, byte[] key);
        public void GenerateKeyInfoFilePath(string filePath);
    }
}
