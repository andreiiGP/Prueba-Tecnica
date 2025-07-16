using System.Net.Http.Headers;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using Path = System.IO.Path;

string inputFolder = @"C:\PruebaEQ";
string ocrFolder = Path.Combine(inputFolder, "OCR");
string unknownFolder = Path.Combine(inputFolder, "UNKNOWN");
string apiBaseUrl = "https://localhost:7029/api"; // RUTA DE LA API 

//  carpetas
Directory.CreateDirectory(inputFolder);
Directory.CreateDirectory(ocrFolder);
Directory.CreateDirectory(unknownFolder);

// obtebgo las claves desde la API
HttpClient client = new HttpClient();
var response = await client.GetAsync($"{apiBaseUrl}/dockey");
var docKeysJson = await response.Content.ReadAsStringAsync();
var docKeys = JsonConvert.DeserializeObject<List<DocKey>>(docKeysJson);

foreach (var filePath in Directory.GetFiles(inputFolder, "*.pdf"))
{
    string fileName = Path.GetFileName(filePath);
    string text = ExtractTextFromPdf(filePath);

    var match = docKeys.FirstOrDefault(k =>
        text.IndexOf(k.Clave, StringComparison.OrdinalIgnoreCase) >= 0);

    if (match != null)
    {
        // Coincidencia: renombra y mueve a OCR
        string newFileName = $"{match.DocName}_{fileName}";
        string newPath = Path.Combine(ocrFolder, newFileName);
        File.Move(filePath, newPath);

        await RegistrarLogAsync(match.DocName + "_" + fileName, "Processed");
        Console.WriteLine($"[✓] {fileName} → OCR como {newFileName}");
    }
    else
    {
        // No se encontró coincidencia
        string newPath = Path.Combine(unknownFolder, fileName);
        File.Move(filePath, newPath);

        await RegistrarLogAsync(fileName, "Unknown");
        Console.WriteLine($"[✗] {fileName} → UNKNOWN");
    }
}

static string ExtractTextFromPdf(string path)
{
    using var reader = new PdfReader(path);
    string text = "";
    for (int i = 1; i <= reader.NumberOfPages; i++)
    {
        text += PdfTextExtractor.GetTextFromPage(reader, i);
    }
    return text;
}

static async Task RegistrarLogAsync(string fileName, string estado)
{
    var log = new
    {
        nombreArchivo = fileName,
        estado = estado
    };

    using var client = new HttpClient();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    var content = new StringContent(JsonConvert.SerializeObject(log), System.Text.Encoding.UTF8, "application/json");
    await client.PostAsync("https://localhost:7029/api/logprocesamiento", content);
}

// Modelo DocKey
public class DocKey
{
    public int Id { get; set; }
    public string Clave { get; set; }
    public string DocName { get; set; }
}
