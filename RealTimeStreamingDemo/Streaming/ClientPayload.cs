namespace RealTimeStreamingDemo.Streaming
{
    public class ClientPayload
    {
        public string Policy { get; set; }
        public string Key { get; set; }
        public string XAmzSignature { get; set; }
        public string XAmzAlgorithm { get; set; }
        public string XAmzDate { get; set; }
        public string XAmzCredential { get; set; }
        public string UploadLink { get; set; }
    }
}
