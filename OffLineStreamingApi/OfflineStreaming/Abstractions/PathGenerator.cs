using System.Runtime.InteropServices;

namespace OffLineStreamingApi.OfflineStreaming.Abstractions
{
    public abstract class PathGenerator
    {
        protected string GenerateBasePath()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return ConstUtility.windowBasePath;
            }
            return ConstUtility.basePath;   
        }

        protected string GenerateSongPath(string songName)
        {
            return CommonFilePathGenerator(songName, ConstUtility.streamFile);
        }

        protected string GenerateTargetSongFilePath(string songName)
        {
            return CommonFilePathGenerator(songName, ConstUtility.fullSong);
        }

        protected string GenerateZipFilePath(string songName)
        {
            return CommonFilePathGenerator(songName,ConstUtility.zipFile);
        }

        private string CommonFilePathGenerator(string songName,string pathType)
        {
            return Path.Combine(
                GenerateBasePath(),
                ConstUtility.musicUpload,
                ConstUtility.offlineStreaming,
                pathType.Trim(), songName.Trim());
        }
    }
}
