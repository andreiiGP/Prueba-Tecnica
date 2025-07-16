using Microsoft.EntityFrameworkCore;
using PdfProcessorApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura conexión a SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();
app.Run();
