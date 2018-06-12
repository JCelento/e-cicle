using EletronicPartsCatalog.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace EletronicPartsCatalog.Infrastructure
{
    public class EletronicPartsCatalogContext : DbContext
    {

        public EletronicPartsCatalogContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<WhereToFindIt> WhereToFind { get; set; }

        public DbSet<ProjectTag> ProjectTags { get; set; }

        public DbSet<ProjectComponent> ProjectComponents { get; set; }

        public DbSet<ComponentWhereToFindIt> ComponentWhereToFindIt { get; set; }
        public DbSet<ProjectFavorite> ProjectFavorites { get; set; }
        public DbSet<FollowedPeople> FollowedPeople { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration.GetConnectionString("Sql");
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.EnableSensitiveDataLogging();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectTag>(b =>
            {
                b.HasKey(t => new { t.ProjectId, t.TagId });

                b.HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectTags)
                .HasForeignKey(pt => pt.ProjectId);

                b.HasOne(pt => pt.Tag)
                .WithMany(t => t.ProjectTags)
                .HasForeignKey(pt => pt.TagId);
            });

            modelBuilder.Entity<ComponentWhereToFindIt>(b =>
            {
                b.HasKey(t => new { t.ComponentId, t.WhereToFindItId });

                b.HasOne(pt => pt.Component)
                .WithMany(p => p.ComponentWhereToFindIt)
                .HasForeignKey(pt => pt.ComponentId);

                b.HasOne(pt => pt.WhereToFindIt)
                .WithMany(t => t.ComponentWhereToFindIt)
                .HasForeignKey(pt => pt.WhereToFindItId);
            });

            modelBuilder.Entity<ProjectComponent>(b =>
            {
                b.HasKey(t => new { t.ProjectId, t.ComponentId });

                b.HasOne(pt => pt.Project)
                .WithMany(p => p.ProjectComponents)
                .HasForeignKey(pt => pt.ProjectId);

                b.HasOne(pt => pt.Component)
                .WithMany(t => t.ProjectComponents)
                .HasForeignKey(pt => pt.ComponentId);
            });

            modelBuilder.Entity<ProjectFavorite>(b =>
            {
                b.HasKey(t => new { t.ProjectId, t.PersonId });

                b.HasOne(pt => pt.Project)
                    .WithMany(p => p.ProjectFavorites)
                    .HasForeignKey(pt => pt.ProjectId);

                b.HasOne(pt => pt.Person)
                    .WithMany(t => t.ProjectFavorites)
                    .HasForeignKey(pt => pt.PersonId);
            });

            modelBuilder.Entity<FollowedPeople>()
                .HasKey(t => new { t.ObserverId, t.TargetId });


            modelBuilder.Entity<FollowedPeople>()
                .HasOne(pt => pt.Observer)
                .WithMany(p => p.Followers)
                .HasForeignKey(pt => pt.ObserverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FollowedPeople>()
                .HasOne(pt => pt.Target)
                .WithMany(t => t.Following)
                .HasForeignKey(pt => pt.TargetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
