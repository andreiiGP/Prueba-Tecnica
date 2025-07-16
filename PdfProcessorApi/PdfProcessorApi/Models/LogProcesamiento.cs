namespace PdfProcessorApi.Models
{
    public class LogProcesamiento
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
