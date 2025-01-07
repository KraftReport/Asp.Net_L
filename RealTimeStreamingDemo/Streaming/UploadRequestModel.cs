namespace RealTimeStreamingDemo.Streaming
{
    public class UploadRequestModel
    {
        public string VideoUrl { get; set; }
        public string Policy { get; set; }
        public string Key { get; set; }
        public string Signature { get; set; }
        public string Algorithm { get; set; }
        public string Date { get; set; } 
        public string UploadLink { get; set; }
        public string Credential { get; set; }
    }
}
