namespace OffLineStreamingApi.OfflineStreaming
{
    public class SongService
    {
        private readonly SongRepository _songRepository;
        public SongService(SongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<bool> SaveSong(string name)
        {
            return await _songRepository.SaveSong(name);
        }

        private async Task<string> GetBase64String(IFormFile file)
        {
            var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            return Convert.ToBase64String(stream.ToArray());
        }
    }
}
