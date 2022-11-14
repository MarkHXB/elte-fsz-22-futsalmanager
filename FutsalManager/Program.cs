global using FutsalManager.Models;
global using FutsalManager.Data;
global using FutsalManager.Common;
global using FutsalManager.Extensions;
global using FutsalManager.Models.History;
using Microsoft.EntityFrameworkCore;
using FutsalManager.Server.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"));
});

builder.Services.AddWebServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();  
builder.Services.AddSession(options => {  
    options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
});  
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
