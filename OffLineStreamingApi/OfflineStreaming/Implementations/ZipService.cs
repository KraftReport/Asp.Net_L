using OffLineStreamingApi.OfflineStreaming.Abstractions;
using System.IO.Compression;

namespace OffLineStreamingApi.OfflineStreaming.Implementations
{
    public class ZipService : Zipper
    {
        public void Zip(string inputFilePath, string outputFilePath)
        {
            var outputPath = Path.GetDirectoryName(outputFilePath);
            if(!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            var exculdedFile = $"{ConstUtility.keyFileName}{ConstUtility.keyFileExtension}";


            if (File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }

            using(var zipStream = new FileStream(outputFilePath, FileMode.Create))
            using(var archiveStream = new ZipArchive(zipStream,ZipArchiveMode.Create))
            {
                foreach(var file in Directory.GetFiles(inputFilePath, "*", SearchOption.AllDirectories))
                {
                    var relativePath = Path.GetRelativePath(inputFilePath, file);
                    if(!relativePath.Equals(exculdedFile))
                    {
                        archiveStream.CreateEntryFromFile(file, relativePath);
                    }
                }
            }
        }
    }
}
