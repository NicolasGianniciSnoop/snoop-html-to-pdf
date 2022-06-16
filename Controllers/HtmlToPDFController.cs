using html_to_pdf.Models;
using html_to_pdf.Services;
using Microsoft.AspNetCore.Mvc;

namespace html_to_pdf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HtmlToPDFController : ControllerBase
    {
        private readonly ILogger<HtmlToPDFController> _logger;
        private readonly PDFService _pdfService;
        public HtmlToPDFController(ILogger<HtmlToPDFController> logger, PDFService pdfService)
        {
            _logger = logger;
            _pdfService = pdfService;
        }

        [HttpPost(Name = "BuildDoc")]
        public async Task<IActionResult> BuildDoc([FromBody] CreateDocRequest createDocumentRequest)
        {
            try
            {
                var document = await _pdfService.BuildDocument(createDocumentRequest);
                MemoryStream ms = new MemoryStream(document.Bytes);
                return new FileStreamResult(ms, "application/pdf");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new JsonResult(new { message = "Ocurrio un error procesando el documento"}) { StatusCode = 500 };
            }
        }
    }
}