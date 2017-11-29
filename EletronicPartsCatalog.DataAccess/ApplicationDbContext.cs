using EletronicPartsCatalog.Contracts.DataContracts;
using EletronicPartsCatalog.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Object> Objects { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<ObjectPartDto> ObjectParts { get; set; }
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Project>().HasKey(p => p.Id);
            builder.Entity<Project>()
                .Property(p => p.Name)
                         .IsRequired();
            builder.Entity<Project>().HasMany(p => p.Parts);

            builder.Entity<Object>().HasKey(o => o.Id);
            builder.Entity<Object>().HasMany(o => o.ObjectParts);

            builder.Entity<Part>().HasKey(p => p.Id);
            builder.Entity<Part>().HasMany(p => p.PartObjects);

            builder.Entity<ObjectPartDto>().HasKey(op => new {op.PartId, op.ObjectId});

            builder.Entity<ObjectPartDto>()
                .HasOne(bc => bc.Part)
                .WithMany(b => b.PartObjects)
                .HasForeignKey(bc => bc.PartId);

            builder.Entity<ObjectPartDto>()
                .HasOne(bc => bc.Object)
                .WithMany(c => c.ObjectParts)
                .HasForeignKey(bc => bc.ObjectId);

        }
    }
}
