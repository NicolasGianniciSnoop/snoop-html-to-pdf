using Microsoft.AspNetCore.Mvc;

namespace html_to_pdf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return new OkObjectResult(new { ok = true });
        }
    }
}
