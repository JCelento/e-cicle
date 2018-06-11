using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Components
{
    public class List
    {
        public class Query : IRequest<ComponentsEnvelope>
        {
            public Query(string name, string whereToFindIt, string search, int? limit, int? offset)
            {
                Name = name;
                WhereToFindIt = whereToFindIt;
                Limit = limit;
                Offset = offset;
                Search = search;
            }

            public string Name { get; }
            public int? Limit { get; }
            public int? Offset { get; }
            public bool IsFeed { get; set; }
            public string Search { get; set; }
            public string WhereToFindIt { get; set; }

        }

        public class QueryHandler : IRequestHandler<Query, ComponentsEnvelope>
        {
            private readonly EletronicPartsCatalogContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public QueryHandler(EletronicPartsCatalogContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<ComponentsEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                IQueryable<Component> queryable = _context.Components.GetAllData();

                if (!string.IsNullOrWhiteSpace(message.WhereToFindIt))
                {
                    var where = await _context.ComponentWhereToFindIt.FirstOrDefaultAsync(x => x.WhereToFindItId == message.WhereToFindIt, cancellationToken);
                    if (where != null)
                    {
                        queryable = queryable.Where(x => x.ComponentWhereToFindIt.Select(y => y.WhereToFindItId).Contains(where.WhereToFindItId));
                    }
                    else
                    {
                        return new ComponentsEnvelope();
                    }
                }

                if (!string.IsNullOrEmpty(message.Search)){
                    queryable = GetComponentsLikeSearched(message.Search, queryable);
                }

                var Components = await queryable
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip(message.Offset ?? 0)
                    .Take(message.Limit ?? 20)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new ComponentsEnvelope()
                {
                    Components = Components,
                    ComponentsCount = queryable.Count()
                };
            }


            public IQueryable<Component> GetComponentsLikeSearched(string q, IQueryable<Component> queryable)
            {
                    return queryable.
                    Where((c) => c.ComponentId.ToLower().Contains(q.ToLower()) 
                    || c.ComponentWhereToFindIt.Any(x => x.WhereToFindItId.ToLower().Contains(q.ToLower())))
                    .OrderBy((o) => o.ComponentId);
                
            }
        }

    }
}