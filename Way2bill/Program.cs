using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Way2bill.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Way2BillDbContext>(options
    => options.UseSqlServer(
        builder.Configuration.GetConnectionString("Way2BillDBConnection")));

// Add logging service
builder.Services.AddLogging();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
});

var app = builder.Build();

// Inject ILogger
var logger = app.Services.GetRequiredService<ILogger<Program>>();

// Test the database connection and log the result
try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<Way2BillDbContext>();
        dbContext.Database.OpenConnection();
        logger.LogInformation("Successfully connected to the database.");
        dbContext.Database.CloseConnection();
    }
}
catch (Exception ex)
{
    logger.LogError("Database connection failed: {ErrorMessage}", ex.Message);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Home}/{id?}");

app.Run();
