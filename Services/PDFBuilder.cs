using html_to_pdf.Models;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace html_to_pdf.Services
{
    public class PDFBuilder
    {
        private PdfOptions pdfOptions { get; set; }
        private LaunchOptions launchOptions { get; set; }
        private int cantPages { get; set; }

        private string htmlDocument { get; set; }

        public PDFBuilder(string _htmlDocument)
        {
            htmlDocument = _htmlDocument;
            pdfOptions = new PdfOptions();
            pdfOptions.PrintBackground = true;

            launchOptions = new LaunchOptions 
            { 
                Headless = true,
                ExecutablePath = "/usr/bin/google-chrome-unstable",
                Args = new[]
                {
                    "--no-sandbox"
                }
            };
        }

        public PDFBuilder SetHeader(string headerHTML)
        {
            if (headerHTML is not null) 
            {
                pdfOptions.HeaderTemplate = headerHTML;
                pdfOptions.DisplayHeaderFooter = true;
            }
            return this;
        }

        public PDFBuilder SetFooter(string footerHTML)
        {
            if (footerHTML is not null)
            {
                pdfOptions.FooterTemplate = footerHTML;
                pdfOptions.DisplayHeaderFooter = true;
            }
            return this;
        }

        public PDFBuilder SetFormat(PaperFormat paperFormat)
        {
            pdfOptions.Format = paperFormat;
            return this;
        }

        public PDFBuilder SetLandScape(bool landscape)
        {
            pdfOptions.Landscape = landscape;
            return this;
        }

        public PDFBuilder SetPageRanges(int pageNum)
        {
            if (pageNum > 0)
            {
                cantPages = 1;
                pdfOptions.PageRanges = pageNum.ToString();
            }
            return this;
        }

        public PDFBuilder SetScale(decimal scale)
        {
            pdfOptions.Scale = scale;
            return this;
        }

        public PDFBuilder SetPageRanges(int from, int to)
        {
            if (validRange(from, to))
            {
                cantPages = to - from + 1;
                pdfOptions.PageRanges = from.ToString() + "-" + to.ToString();
            }
            return this;
        }

        public async Task<PDFDocument> BuildAsync(string filename)
        {
            // using var browserFetcher = new BrowserFetcher();
            // await browserFetcher.DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(launchOptions);
            var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlDocument);

            Thread.Sleep(5000);

            var data = await page.PdfDataAsync(pdfOptions);
            
            await browser.CloseAsync();
            reset();

            return new PDFDocument
            {
                Name = filename,
                Bytes = data,
                Pages = cantPages
            };
        }

        public PDFDocument Build(string filename) => BuildAsync(filename).GetAwaiter().GetResult();

        private string getDocument() => htmlDocument;

        private void reset() => pdfOptions = new PdfOptions();

        private bool validRange(int from, int to) => from <= to && from > 0 && to > 0;
    }
}
