using HeavyMetalBands.Data;
using Microsoft.EntityFrameworkCore;
using HeavyMetalBands.Services;
using HeavyMetalBands.Repositories;
using HeavyMetalBands.Maping;
using Autofac;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Use Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register services in Autofac container
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<BandsService>().As<IBandsService>().InstancePerLifetimeScope(); 
    containerBuilder.RegisterType<BandsRepository>().As<IBandsRepository>().InstancePerLifetimeScope();

});


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// mapping DbContexts
builder.Services.AddDbContext<DbContext_Write>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WriteDb")));

builder.Services.AddDbContext<DbContext_Read>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ReadDb")));

// defining Dependency Injection for Service and repository
//builder.Services.AddTransient<IBandsService, BandsService>();
//builder.Services.AddTransient<IBandsRepository, BandsRepository>();


// Register all mappings found across the application
// Requires e AutoMapper.Extensions.Microsoft.DependencyInjection
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register only selected mapping
 builder.Services.AddAutoMapper(typeof(BandProfile));



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


// Make the implicit Program class public so test projects can access it
public partial class Program { }