using SkiaSharp;
using SkiaSharp.QrCode;

namespace TableProjectComponentServiceTestWebAPI.QrCode
{
    public class QrCodeService
    {
        private IWebHostEnvironment environment;

        public QrCodeService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        public void GenerateQrCode(string Title,string Content)
        {
            var qrCoder = new QRCodeGenerator();
            var qr = qrCoder.CreateQrCode(Content, ECCLevel.L);
            var info = new SKImageInfo(512, 512);
            var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Render(qr,info.Width,info.Height);
            var image = surface.Snapshot();
            var data = image.Encode(SKEncodedImageFormat.Png, 100);
            var uploadFolder = Path.Combine(environment.ContentRootPath, "Uploads");
            Directory.CreateDirectory(uploadFolder);
            var filePath = Path.Combine(uploadFolder, Title+".png");
            var stream = File.OpenWrite(filePath);
            data.SaveTo(stream);
        }

        public  byte[] GetQrCode(string Title)
        {
            var uploadFolder = Path.Combine(environment.ContentRootPath, "Uploads");
            Directory.CreateDirectory(uploadFolder);

            // Define the file path
            var filePath = Path.Combine(uploadFolder, Title + ".png");

            // Write to the file using a 'using' statement to ensure the file stream is properly closed
            using (var stream = File.OpenWrite(filePath))
            {
                // You can write to the file here if needed
            }

            // Read the file after the stream has been closed
            var ba = System.IO.File.ReadAllBytes(filePath);
            return ba;

        }
    }
}
