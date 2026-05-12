using Microsoft.EntityFrameworkCore;
using Glorpa.Data;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString(
    "DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found.");

// DbContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));

// MVC + API
builder.Services.AddControllersWithViews();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// MVC ruta
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// API kontroleri
app.MapControllers();

app.Run();