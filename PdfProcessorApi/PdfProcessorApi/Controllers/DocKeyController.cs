using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PdfProcessorApi.Data;
using PdfProcessorApi.Models;

namespace PdfProcessorApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocKeyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocKeyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/dockey
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocKey>>> GetDocKeys()
        {
            return await _context.DocKeys.ToListAsync();
        }

        // GET: api/dockey/
        [HttpGet("{id}")]
        public async Task<ActionResult<DocKey>> GetDocKey(int id)
        {
            var docKey = await _context.DocKeys.FindAsync(id);

            if (docKey == null)
                return NotFound();

            return docKey;
        }

        // POST: api/dockey
        [HttpPost]
        public async Task<ActionResult<DocKey>> PostDocKey(DocKey docKey)
        {
            _context.DocKeys.Add(docKey);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocKey), new { id = docKey.Id }, docKey);
        }


        // PUT: api/dockey/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocKey(int id, DocKey docKey)
        {
            if (id != docKey.Id)
                return BadRequest();

            _context.Entry(docKey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocKeyExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/dockey/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocKey(int id)
        {
            var docKey = await _context.DocKeys.FindAsync(id);
            if (docKey == null)
                return NotFound();

            _context.DocKeys.Remove(docKey);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocKeyExists(int id)
        {
            return _context.DocKeys.Any(e => e.Id == id);
        }
    }
}
