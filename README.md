<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/d/d3/Soccerball.svg/500px-Soccerball.svg.png" width=48> Futsal Manager
=======================
## Features
- Local database communication
- It run on web
- You can manage futsal <strong>teams</strong> and do CRUD on them
- You can manage futsal <strong>players</strong> and do CRUD on them
- You can transfer players between teams
- Active / Inactive players status

## Development

- IDE: Jetbrains Rider
- Language: C# 11.0
- SDK: .NET Core 6.0

## Build
1. Clone repository
2. Install Mssql server
3. Install the needed NuGet packages
4. Build the project in Visual Studio or in Rider
5. And run it
6. Open up a browser and ( By default: type : localhost:7104 as an URL ) and the website you should see

## How it works
<strong><i>The soul of the application is in Program.cs.</i></strong>
Program.cs
```C#
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

// Get the connection string from the appsettings.json
// Then tell the builder to use Sql Server as datatabse
// Sql Server will be used with the LocalConnection string
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"));
});

// Referencing the DI section of the application
// You can find the logic in Configurations/ConfigureWebServices.cs
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
```
