using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace PdfProcessorApi.Controllers
{
    // Modelo para encapsular el archivo y que Swagger pueda procesarlo correctamente
    public class UploadPdfRequest
    {
        [FromForm(Name = "file")]
        public IFormFile File { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly string _uploadPath = @"C:\PruebaEQ";

        [HttpPost("pdf")]
        public async Task<IActionResult> UploadPdf([FromForm] UploadPdfRequest request)
        {
            var file = request.File;

            if (file == null || file.Length == 0)
                return BadRequest("Archivo no válido");

            var extension = Path.GetExtension(file.FileName).ToLower();
            if (extension != ".pdf")
                return BadRequest("Solo se permiten archivos PDF");

            if (file.Length > 10 * 1024 * 1024)
                return BadRequest("El archivo excede el tamaño máximo de 10 MB");

            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);

            var filePath = Path.Combine(_uploadPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new
            {
                message = "Archivo subido exitosamente",
                fileName = file.FileName
            });
        }
    }
}
