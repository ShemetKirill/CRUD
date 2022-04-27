using CRUD.Database;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;




var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSingleton();
// Add services to the container.
builder.Services.AddBrowserDetection();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SqlDbContext>(opt =>
    opt.UseSqlServer(
        "Server=(localdb)\\mssqllocaldb;Database=CRUD;Trusted_Connection=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();


   