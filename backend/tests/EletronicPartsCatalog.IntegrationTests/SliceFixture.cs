using System;
using System.IO;
using System.Threading.Tasks;
using EletronicPartsCatalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EletronicPartsCatalog.IntegrationTests
{
    public class SliceFixture : IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SliceFixture()
        {
            AutoMapper.ServiceCollectionExtensions.UseStaticRegistration = false;
            var startup = new Startup();
            var services = new ServiceCollection();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<EletronicPartsCatalogContext>();
            var connectionString = configuration.GetConnectionString("Test");
            builder.UseSqlServer(connectionString);

            services.AddSingleton(new EletronicPartsCatalogContext(builder.Options));

            startup.ConfigureServices(services);

            var provider = services.BuildServiceProvider();


            provider.GetRequiredService<EletronicPartsCatalogContext>().Database.EnsureCreated();
            _scopeFactory = provider.GetService<IServiceScopeFactory>();
        }

        public void Dispose()
        {
            this.Dispose();
        }

        public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                await action(scope.ServiceProvider);
            }
        }

        public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                return await action(scope.ServiceProvider);
            }
        }

        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();

                return mediator.Send(request);
            });
        }

        public Task SendAsync(IRequest request)
        {
            return ExecuteScopeAsync(sp =>
            {
                var mediator = sp.GetService<IMediator>();

                return mediator.Send(request);
            });
        }

        public Task ExecuteDbContextAsync(Func<EletronicPartsCatalogContext, Task> action)
        {
            return ExecuteScopeAsync(sp => action(sp.GetService<EletronicPartsCatalogContext>()));
        }

        public Task<T> ExecuteDbContextAsync<T>(Func<EletronicPartsCatalogContext, Task<T>> action)
        {
            return ExecuteScopeAsync(sp => action(sp.GetService<EletronicPartsCatalogContext>()));
        }

        public Task InsertAsync(params object[] entities)
        {
            return ExecuteDbContextAsync(db =>
            {
                foreach (var entity in entities)
                {
                    db.Add(entity);
                }
                return db.SaveChangesAsync();
            });
        }
    }
}