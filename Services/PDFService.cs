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
                .BuildAsync(request.Nombre);

            var base64 = Convert.ToBase64String(document.Bytes);

            Console.WriteLine("BASE64: " + base64);

            return document;
        }

    }
}
