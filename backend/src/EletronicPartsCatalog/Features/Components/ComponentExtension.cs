using System.Linq;
using EletronicPartsCatalog.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Components
{
    public static class ComponentsExtensions
    {
        public static IQueryable<Component> GetAllData(this DbSet<Component> Components)
        {
            return Components
                .Include(x => x.ComponentWhereToFindIt)
                .AsNoTracking();
        }
    }
}