namespace html_to_pdf.Models
{
    public class PDFDocument
    {
        public string Name { get; set; }
        public int Pages { get; set; }
        public byte[] Bytes { get; set; }

        public void SaveFile(string path)
        {
            Directory.CreateDirectory(path);
            File.WriteAllBytes(path + "/" + Name + ".pdf", Bytes);
        }

        public void SaveFile()
        {
            File.WriteAllBytes(Name + ".pdf", Bytes);
        }
    }
}
