using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.UnitTests.Infrastructure
{
    public class EletronicPartsCatalogContextMock
    {
        public Person Person { get; set; }
        public Project Project { get; set; }

        public EletronicPartsCatalogContextMock()
        {
            var fixture = new Fixture();
            var hash = fixture.Create<byte[]>();
            var salt = fixture.Create<byte[]>();

            this.Person = new Person
            {
                Username = "test",
                Bio = "some bio",
                Email = "mail@test.com",
                Hash = hash,
                PersonId = 3,
                ProjectFavorites = new List<ProjectFavorite>(),
                Salt = salt

            };

            this.Project = new Project
            {
                Title = "title",
                Author = Person,
                Body = "some body",
                Comments = new List<Comment>(),
                CreatedAt = DateTime.Today,
                Description = "some description",
                ProjectId = 1,
                ProjectComponents = new List<ProjectComponent>(),
                ProjectFavorites = new List<ProjectFavorite>()
            };
        }

        public EletronicPartsCatalogContext GetMockedContextWithData()
        {
    
            var options = new DbContextOptionsBuilder<EletronicPartsCatalogContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            var context = new EletronicPartsCatalogContext(options);

            context.ChangeTracker.Entries()
                .Where(e => e.Entity != null).ToList()
                .ForEach(e => e.State = EntityState.Detached);

            context.Persons.Add(Person);
            
            context.SaveChanges();

            return context;
        }

    }
}
