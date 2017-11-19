using System.Collections.Generic;
using System.Linq;
using EletronicPartsCatalog.Contracts.DataContracts;
using EletronicPartsCatalog.Contracts.Repositories;
using EletronicPartsCatalog.DataAccess;
using EletronicPartsCatalog.DataAccess.Repositories;
using EletronicPartsCatalog.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EletronicPartsCatalog.Tests
{
    [TestClass]
    public class ProjectsServiceTests
    {
        private DbContextOptions<ApplicationDbContext> _options;

        [TestInitialize]
        public void Init() {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "In_memory_db")
                .Options;
        }

        [TestCleanup]
        public void Cleanup() {
            using (var context = new ApplicationDbContext(_options)) {
                context.Database.EnsureDeleted();
            }
        }

        [TestMethod]
        public void AddProjectShouldSucceed() {
            using (var context = new ApplicationDbContext(_options)) {
                var repository = new ProjectsRepository(context);
                var service = new ProjectsService(repository);
                service.Add(new AddProjectDto { Name = "Test_project", Description = "Test_Desc" });
            }

            using (var context = new ApplicationDbContext(_options)) {
                Assert.AreEqual(1, context.Projects.Count());
            }
        }

        [TestMethod]
        public void AddProjectWithDuplicatedNameShouldFailed() {
            using (var context = new ApplicationDbContext(_options)) {
                var repository = new ProjectsRepository(context);
                var service = new ProjectsService(repository);

                var firstResult = service.Add(new AddProjectDto { Name = "Test_project", Description = "Test_Desc_1" });
                var secondResult = service.Add(new AddProjectDto { Name = "Test_project", Description = "Test_Desc_2" });

                Assert.AreEqual(true, firstResult.IsSuccess);
                Assert.AreEqual(false, secondResult.IsSuccess);
            }

            using (var context = new ApplicationDbContext(_options)) {
                Assert.AreEqual(1, context.Projects.Count());
            }
        }

        [TestMethod]
        public void GetAllMethodShouldReturnProjects() {
            var repositoryMock = new Mock<IProjectsRepository>();
            repositoryMock.Setup(x => x.GetAll())
                .Returns(new List<ProjectDto>
                {
                    new ProjectDto
                    {
                        Name = "project_from_repository_mock"
                    }
                });

            var projectsService = new ProjectsService(repositoryMock.Object);

            var result = projectsService.GetAll();

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.First().Name, "project_from_repository_mock");
        }
    }
}
