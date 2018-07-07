using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Comments
{
    public class List
    {
        public class Query : IRequest<CommentsEnvelope>
        {
            public Query(string slug)
            {
                Slug = slug;
            }

            public string Slug { get; }
        }

        public class QueryHandler : IRequestHandler<Query, CommentsEnvelope>
        {
            private readonly EletronicPartsCatalogContext _context;

            public QueryHandler(EletronicPartsCatalogContext context)
            {
                _context = context;
            }

            public async Task<CommentsEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var Project = await _context.Projects
                    .Include(x => x.Comments)
                    .ThenInclude(x => x.Author)
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (Project == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Project = "Project not found." });
                }

                return new CommentsEnvelope(Project.Comments);
            }
        }
    }
}