global using FutsalManager.Models;
global using FutsalManager.Data;
global using FutsalManager.Common;
global using FutsalManager.Extensions;
global using FutsalManager.Models.History;
using Microsoft.EntityFrameworkCore;
using FutsalManager.Server.Configurations;
using M6T.Core.TupleModelBinder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"));
});

builder.Services.AddWebServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.ModelBinderProviders.Insert(0, new TupleModelBinderProvider());
});

builder.Services.AddDistributedMemoryCache();  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
