namespace TableProjectComponentServiceTestWebAPI.Audio
{
    public class AudioService
    {
        private readonly AudioDao audioDao;
        private readonly IWebHostEnvironment environment;
        public AudioService(AudioDao audioDao, IWebHostEnvironment environment)
        {
            this.audioDao = audioDao;
            this.environment = environment;
        }
        public async Task<int> uploadAudio(IFormFile file)
        {
            var uploadFolder = Path.Combine(environment.ContentRootPath, "Uploads");
            Directory.CreateDirectory(uploadFolder);
            var filePath = Path.Combine(uploadFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var audioFile = new AudioFile
            {
                FileName = file.FileName,
                FilePath = filePath,
            };
            return await audioDao.saveAudio(audioFile);
        }
        public async Task<List<string>> GetAudioDetailsAsync(int Id)
        {
            var audio = await audioDao.GetAudioFileAsync(Id);

            if (audio == null)
            {
                return null;
            }

            var filePath = audio.FilePath;
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            var mimeType = extension switch
            {
                ".mp3" => "audio/mpeg",
                // Add other MIME types as needed
                _ => "application/octet-stream"
            };

            return new List<string> { mimeType, audio.FileName, filePath };
        }

    }
}
