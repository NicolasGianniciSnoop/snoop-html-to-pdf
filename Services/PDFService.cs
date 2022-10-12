using html_to_pdf.Models;

namespace html_to_pdf.Services
{
    public class PDFService
    {
        public async Task<PDFDocument> BuildDocument(CreateDocRequest request)
        {
            var builder = new PDFBuilder(request.HtmlDocument);

            var document = await builder
                .SetFormat(PuppeteerSharp.Media.PaperFormat.A4)
                .SetScale(request.Scale.HasValue ? request.Scale.Value : 1)
                .BuildAsync(request.Nombre);
            return document;
        }

    }
}
