using Microsoft.EntityFrameworkCore;
using MVC_Migration.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. ******* Session �w�]�n�J30����
builder.Services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout
    options.Cookie.HttpOnly = true; // Make the session cookie HTTP only
    options.Cookie.IsEssential = true; // Make the session cookie essential
});



// Add services to the container.
builder.Services.AddControllersWithViews();
//�o��!!!
string connectionString = builder.Configuration.GetConnectionString("CmsContext");

builder.Services.AddDbContext<CmsContext>(options => options.UseSqlServer(connectionString));
//��o��!!!
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

// Add session *******
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
