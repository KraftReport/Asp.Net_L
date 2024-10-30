namespace OffLineStreamingApi.OfflineStreaming
{
    public class ConstUtility
    {
        public const string basePath = "/home";
        public const string windowBasePath = "C:\\home";
        public const string musicUpload = "musicUpload";
        public const string offlineStreaming = "offlineStreaming";
        public const string fullSong = "fullSong";
        public const string zipFile = "zipFile";
        public const string streamFile = "streamFile";
        public const string keyFileName = "enc";
        public const string keyFileExtension = ".key";
        public const string keyinfoFileExtension = ".keyinfo";
        public const string inputFile = "-i";
        public const string overrideOutput = "-y";
        public const string segmentDuration15Seconds = "-hls_time 15";
        public const string keyinfoFileLocation = "-hls_key_info_file";
        public const string playListType = "-hls_playlist_type vod";
        public const string copyCodec = "-c copy";
        public const string segmentFile = "-hls_segment_filename";
        public const string outputPlaylist = "index.m3u8";
        public const string segmentName = "output%d.ts";
    }
}
