using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Builder;
using HeavyMetalBands.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HeavyMetalTests
{

    public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor1 = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(IDbContextOptionsConfiguration<DbContext_Write>));

                services.Remove(dbContextDescriptor1);

                var dbContextDescriptor2 = services.SingleOrDefault(
                   d => d.ServiceType ==
                       typeof(IDbContextOptionsConfiguration<DbContext_Read>));

                services.Remove(dbContextDescriptor2);


                 

                // Create open SqliteConnection so EF won't automatically close it.
                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<DbContext_Write>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });

                services.AddDbContext<DbContext_Read>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });
            });

            builder.UseEnvironment("Development");
        }
    }

    //public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    //{
    //    protected override void ConfigureWebHost(IWebHostBuilder builder)
    //    {
    //        builder.ConfigureServices(services =>
    //        {
    //            // Remove global anti-forgery validation (if used)
    //            //services.Configure<MvcOptions>(options =>
    //            //{
    //            //    options.Filters.Remove(new AutoValidateAntiforgeryTokenAttribute());
    //            //});

    //            // Optional antiforgery service config
    //       //     services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);
    //            services.AddControllersWithViews();
    //        });

    //        builder.Configure(app =>
    //        {
    //            // Ensure routing is set up properly
    //            app.UseRouting();

    //            // Optional: Suppress anti-forgery token manually (rarely needed)
    //            //app.Use(async (context, next) =>
    //            //{
    //            //    context.Request.Headers.Remove("RequestVerificationToken");
    //            //    await next();
    //            //});

    //            app.UseEndpoints(endpoints =>
    //            {



    //               // endpoints.MapControllers();
    //               endpoints.MapDefaultControllerRoute(); // <== Required for MVC with views
    //            });
    //        });
    //    }
    //}
}
