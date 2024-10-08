using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CKeditor.CKEditorModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class CKEditorController : ControllerBase
    {
        private readonly IGenricRepository<Article> genricRepository;
        public CKEditorController(IGenricRepository<Article> genricRepository)
        {
            this.genricRepository = genricRepository;
        }

        [HttpPost]
        public IActionResult Test(Article article)
        {
            var data = genricRepository.InsertRecord(article);
            return Ok(data);
        }
    }
}
