using System.Security.Cryptography;
using Microsoft.AspNetCore.Http.HttpResults;
using Xabe.FFmpeg;

namespace StreamLab.MP3;

public class Mp3Service
{
    public async Task StreamSong(string inputFilePath)
    {
        var basePath = "C:\\Users\\KraftWork\\Desktop\\GitWorkSpace\\Asp.Net_L\\StreamLab\\StreamLab\\Output"; 
        var m3U8FilePath = Path.Combine(basePath,"main.m3u8 ");
        var keyFilePath = Path.Combine(basePath,"key.bin");
        var keyInfoPath = Path.Combine(basePath, "keyInfo.txt");

        var key = GenerateRandomKey(18);

        if(!Path.Exists(basePath))
            Directory.CreateDirectory(basePath);
        
        await File.WriteAllBytesAsync(keyFilePath,key);

        var conversion = FFmpeg.Conversions.New()
            .AddParameter($"-i \"{inputFilePath}\"")
            .AddParameter($"-c copy -map 0 -f segment -segment_time 30 -segment_format mpegts")
            .AddParameter($"-hls_key_info_file \"{keyInfoPath}\"")
            .AddParameter("-hls_flags independent_segments")
            .AddParameter($"-segment_list \"{m3U8FilePath}\"")
            .AddParameter($"-segment_list_type m3u8 \"{Path.Combine(basePath, "seg_%03d.ts")}\"");

        await conversion.Start(); 
    }

    private byte[] GenerateRandomKey(int size)
    {
        using var rng = RandomNumberGenerator.Create();
        var key = new byte[size];
        rng.GetBytes(key);
        return key;
    }
}