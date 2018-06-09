using EletronicPartsCatalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Infrastructure
{
    public class EletronicPartsCatalogContext : DbContext
    {
        private readonly string _databaseName = Startup.DATABASE_FILE;

        public EletronicPartsCatalogContext(DbContextOptions options) 
            : base(options)
        {
        }

        public EletronicPartsCatalogContext(string databaseName)
        {
            _databaseName = databaseName;
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProjectTag> ProjectTags { get; set; }
        public DbSet<ProjectFavorite> ProjectFavorites { get; set; }
        public DbSet<FollowedPeople> FollowedPeople { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databaseName}");
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

            modelBuilder.Entity<FollowedPeople>(b =>
            {
                b.HasKey(t => new { t.ObserverId, t.TargetId });

                b.HasOne(pt => pt.Observer)
                    .WithMany(p => p.Followers)
                    .HasForeignKey(pt => pt.ObserverId);

                b.HasOne(pt => pt.Target)
                    .WithMany(t => t.Following)
                    .HasForeignKey(pt => pt.TargetId);
            });
        }
    }
}
