namespace html_to_pdf.Models
{
    public class CreateDocRequest
    {
        public string HtmlDocument { get; set; }
        public int? CantidadPaginas { get; set; }
        public string Nombre { get; set; }
        public decimal? Scale { get; set; }
    }
}
