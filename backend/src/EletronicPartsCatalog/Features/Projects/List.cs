using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Domain;
using EletronicPartsCatalog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Projects
{
    public class List
    {
        public class Query : IRequest<ProjectsEnvelope>
        {
            public Query(string tag, string author, string favorited, int? limit, int? offset)
            {
                Tag = tag;
                Author = author;
                FavoritedUsername = favorited;
                Limit = limit;
                Offset = offset;
            }

            public string Tag { get; }
            public string Author { get; }
            public string FavoritedUsername { get; }
            public int? Limit { get; }
            public int? Offset { get; }
            public bool IsFeed { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, ProjectsEnvelope>
        {
            private readonly EletronicPartsCatalogContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public QueryHandler(EletronicPartsCatalogContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<ProjectsEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                IQueryable<Project> queryable = _context.Projects.GetAllData();

                if (message.IsFeed && _currentUserAccessor.GetCurrentUsername() != null)
                {
                    var currentUser = await _context.Persons.Include(x => x.Following).FirstOrDefaultAsync(x => x.Username == _currentUserAccessor.GetCurrentUsername(), cancellationToken);
                    queryable = queryable.Where(x => currentUser.Following.Select(y => y.TargetId).Contains(x.Author.PersonId));
                }

                if (!string.IsNullOrWhiteSpace(message.Tag))
                {
                    var tag = await _context.ProjectTags.FirstOrDefaultAsync(x => x.TagId == message.Tag, cancellationToken);
                    if (tag != null)
                    {
                        queryable = queryable.Where(x => x.ProjectTags.Select(y => y.TagId).Contains(tag.TagId));
                    }
                    else
                    {
                        return new ProjectsEnvelope();
                    }
                }
                if (!string.IsNullOrWhiteSpace(message.Author))
                {
                    var author = await _context.Persons.FirstOrDefaultAsync(x => x.Username == message.Author, cancellationToken);
                    if (author != null)
                    {
                        queryable = queryable.Where(x => x.Author == author);
                    }
                    else
                    {
                        return new ProjectsEnvelope();
                    }
                }
                if (!string.IsNullOrWhiteSpace(message.FavoritedUsername))
                {
                    var author = await _context.Persons.FirstOrDefaultAsync(x => x.Username == message.FavoritedUsername, cancellationToken);
                    if (author != null)
                    {
                        queryable = queryable.Where(x => x.ProjectFavorites.Any(y => y.PersonId == author.PersonId));
                    }
                    else
                    {
                        return new ProjectsEnvelope();
                    }
                }

                var Projects = await queryable
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip(message.Offset ?? 0)
                    .Take(message.Limit ?? 20)
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return new ProjectsEnvelope()
                {
                    Projects = Projects,
                    ProjectsCount = queryable.Count()
                };
            }
        }
    }
}