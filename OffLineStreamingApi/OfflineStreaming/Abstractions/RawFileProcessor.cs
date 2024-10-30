namespace OffLineStreamingApi.OfflineStreaming.Abstractions
{
    public interface RawFileProcessor
    {
        public void StreamProcessor(string inputFilePath,string outputFilePath);    
    }
}
