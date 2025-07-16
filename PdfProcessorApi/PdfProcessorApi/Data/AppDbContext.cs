using Microsoft.EntityFrameworkCore;
using PdfProcessorApi.Models;

namespace PdfProcessorApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<DocKey> DocKeys { get; set; }
        public DbSet<LogProcesamiento> LogProcesamientos { get; set; }
    }
}
