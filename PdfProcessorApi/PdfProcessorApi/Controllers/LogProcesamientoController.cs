using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfProcessorApi.Data;
using PdfProcessorApi.Models;

namespace PdfProcessorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogProcesamientoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogProcesamientoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/logprocesamiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogProcesamiento>>> GetLogs()
        {
            return await _context.LogProcesamientos
                .OrderByDescending(l => l.Fecha)
                .ToListAsync();
        }

        // POST: api/logprocesamiento
        [HttpPost]
        public async Task<ActionResult<LogProcesamiento>> PostLog(LogProcesamiento log)
        {
            _context.LogProcesamientos.Add(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostLog), new { id = log.Id }, log);
        }
    }
}
