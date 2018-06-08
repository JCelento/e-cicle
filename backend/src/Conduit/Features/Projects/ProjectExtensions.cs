using System.Linq;
using EletronicPartsCatalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Projects
{
    public static class ProjectExtensions
    {
        public static IQueryable<Project> GetAllData(this DbSet<Project> Projects)
        {
            return Projects
                .Include(x => x.Author)
                .Include(x => x.ProjectFavorites)
                .Include(x => x.ProjectTags)
                .AsNoTracking();
        }
    }
}