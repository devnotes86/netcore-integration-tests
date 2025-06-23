using Autofac;
using AutoMapper;
using HeavyMetalBands.Data;
using HeavyMetalBands.Maping;
using HeavyMetalBands.Repositories;
using HeavyMetalBands.Services;
using Microsoft.EntityFrameworkCore;

namespace HeavyMetalTests
{
    public class TestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BandsRepository>().As<IBandsRepository>();
            builder.RegisterType<BandsService>().As<IBandsService>();

            builder.Register(ctx =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<BandProfile>();
                });

                return config.CreateMapper();
            }).As<IMapper>().SingleInstance();


            // Register in-memory EF Core context
            builder.Register(c =>
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("BandsTestDb")
                    .Options;
                return new ApplicationDbContext(options);
            }).AsSelf().InstancePerLifetimeScope();

            // Register InMemory DbContexts
            builder.Register(ctx =>
            {
                var options = new DbContextOptionsBuilder<DbContext_Read>()
                    .UseInMemoryDatabase("BandsTestDb")
                    .Options;

                return new DbContext_Read(options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.Register(ctx =>
            {
                var options = new DbContextOptionsBuilder<DbContext_Write>()
                    .UseInMemoryDatabase("BandsTestDb")
                    .Options;

                return new DbContext_Write(options);
            }).AsSelf().InstancePerLifetimeScope();
        }
    }
}
