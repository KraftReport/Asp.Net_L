namespace BackendPlayer.BackendPlayer.Interface
{
    public interface IMusicPlayer
    {
        public void Play();
        public void Pause();
        public void Stop();
        public void Load(string filepath);
    }
}
