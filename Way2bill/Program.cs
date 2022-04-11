using Microsoft.EntityFrameworkCore;
using Way2bill.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Way2BillDbContext>(options
    => options.UseSqlServer(
        builder.Configuration.GetConnectionString("Way2BillDBConnection")));
builder.Services.AddSession(
options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
});

var app = builder.Build();

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
