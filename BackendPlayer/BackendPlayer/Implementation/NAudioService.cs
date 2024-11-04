using BackendPlayer.BackendPlayer.Interface;
using NAudio.Wave;

namespace BackendPlayer.BackendPlayer.Implementation
{
    public class NAudioService : IMusicPlayer,IDisposable
    {
        public IWavePlayer waveOutDevice;
        public AudioFileReader audioFileReader;
        


        public void Load(string filePath)
        {
            if(waveOutDevice == null)
            {
                waveOutDevice = new WaveOutEvent();
                audioFileReader = new AudioFileReader(filePath);
                waveOutDevice.Init(audioFileReader);
            }
        }

        public void Play()
        {
            waveOutDevice?.Play();
        }

        public void Pause()
        {
            waveOutDevice?.Pause();
        }

        public void Stop()
        {
            waveOutDevice?.Stop();
            if (audioFileReader != null)
            {
                audioFileReader.Position = 0;  
            }
        }

        public void SetVolume(float volume)
        {
            if (audioFileReader != null)
            {
                audioFileReader.Volume = volume;
            }
        }

        public void Dispose()
        {
            waveOutDevice?.Dispose();
            audioFileReader?.Dispose();
        }
    }
}
