using EletronicPartsCatalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EletronicPartsCatalog.Api.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EletronicPartsCatalogContext>
    {
        public EletronicPartsCatalogContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<EletronicPartsCatalogContext>();
            var connectionString = configuration.GetConnectionString("Sql");
            builder.UseSqlServer(connectionString);

            return new EletronicPartsCatalogContext(builder.Options);
        }
    }
}
