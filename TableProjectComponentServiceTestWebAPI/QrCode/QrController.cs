using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TableProjectComponentServiceTestWebAPI.QrCode
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrController : ControllerBase
    {
        private QrCodeService QrCodeService;
        public QrController(QrCodeService qrCodeService)
        {
            this.QrCodeService = qrCodeService;
        }

        [HttpPost("/{title}/{data}")]
        public IActionResult createQrCode(string title, string data)
        {
            QrCodeService.GenerateQrCode(title, data);
            return Ok("ok");
        }

        [HttpGet("{title}")]
        public IActionResult GetQr(string title)
        {
            return File(QrCodeService.GetQrCode(title),"image/png");
        }
    }
}
