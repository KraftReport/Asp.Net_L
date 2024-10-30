using OffLineStreamingApi.OfflineStreaming.Abstractions;
using System.Diagnostics;
using System.Text;

namespace OffLineStreamingApi.OfflineStreaming.Implementations
{
    public class FfmpegService : RawFileProcessor
    {
        public async void StreamProcessor(string inputFilePath, string outputFilePath)
        {
            var command = new StringBuilder();
            command.Append(ConstUtility.overrideOutput + " ");
            command.Append($"{ConstUtility.inputFile + " "} \"{inputFilePath}\" ");
            command.Append(ConstUtility.segmentDuration15Seconds + " ");
            command.Append(ConstUtility.copyCodec + " ");
            command.Append($"{ConstUtility.keyinfoFileLocation + " "} \"{Path.Combine(outputFilePath, ConstUtility.keyFileName, ConstUtility.keyinfoFileExtension)}\" ");
            command.Append(ConstUtility.playListType + " ");
            command.Append($"{ConstUtility.segmentFile + " "} \"{Path.Combine(outputFilePath,ConstUtility.segmentName)}\" ");
            command.Append($"\"{Path.Combine(outputFilePath,ConstUtility.outputPlaylist)}\" ");

            var start = new ProcessStartInfo()
            {
                FileName = "ffmpeg",
                Arguments = command.ToString(),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = outputFilePath
            };

            using(var process = new Process() { StartInfo = start })
            {
                var source = new TaskCompletionSource<bool>();

                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                var outputTask = process.StandardOutput.ReadToEndAsync();
                var outputError = process.StandardError.ReadToEndAsync();

                process.WaitForExit();

                if(process.ExitCode != 0)
                {
                    throw new Exception();
                }

                await outputTask;
            }
        }
    }
}
