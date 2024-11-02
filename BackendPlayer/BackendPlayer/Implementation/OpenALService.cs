using System;
using System.IO;
using System.Runtime.InteropServices;
using BackendPlayer.BackendPlayer.Interface;
using NAudio.Wave;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

namespace BackendPlayer.BackendPlayer.Implementation
{
    public class OpenALService : IMusicPlayer
    {

        private int buffer;
        private int source;
        private ALFormat format;
        private byte[] audioData;
        private int sampleRate;
        private bool isPaused;
        public OpenALService()
        {
            if(buffer == 0 && source == 0)
            {
                buffer = AL.GenBuffer();
                source = AL.GenSource(); 
            }
        }

        public void Load(string filePath)
        { 
            using (var mp3Reader = new Mp3FileReader(filePath))
            using (var pcmStream = WaveFormatConversionStream.CreatePcmStream(mp3Reader))
            using (var memoryStream = new MemoryStream())
            { 
                pcmStream.CopyTo(memoryStream);
                audioData = memoryStream.ToArray();
                 
                format = pcmStream.WaveFormat.Channels == 2 ? ALFormat.Stereo16 : ALFormat.Mono16;
                sampleRate = pcmStream.WaveFormat.SampleRate;
            }
             
            GCHandle handle = GCHandle.Alloc(audioData, GCHandleType.Pinned);
            try
            {
                IntPtr pointer = handle.AddrOfPinnedObject();
                AL.BufferData(buffer, format, pointer, audioData.Length, sampleRate);
            }
            finally
            {
                handle.Free();
            }

            AL.Source(source, ALSourcei.Buffer, buffer);
        }

        public void Play()
        {
            if (isPaused)
            {
                AL.SourcePlay(source);
                isPaused = false;
            }
            else
            {
                Stop();  
                AL.SourcePlay(source);
            }
        }

        public void Pause()
        {
            AL.SourcePause(source);
            isPaused = true;
        }

        public void Stop()
        {
            AL.SourceStop(source);
            AL.SourceRewind(source);  
            isPaused = false;
        }

        public void SetVolume(float volume)
        {
            AL.Source(source, ALSourcef.Gain, volume);
        }

        public void Dispose()
        {
            Stop();
            AL.DeleteSource(source);
            AL.DeleteBuffer(buffer); 
        }
    }
}
