using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Projects
{
    public class Details
    {
        public class Query : IRequest<ProjectEnvelope>
        {
            public Query(string slug)
            {
                Slug = slug;
            }

            public string Slug { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Slug).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, ProjectEnvelope>
        {
            private readonly EletronicPartsCatalogContext _context;

            public QueryHandler(EletronicPartsCatalogContext context)
            {
                _context = context;
            }

            public async Task<ProjectEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var Project = await _context.Projects.GetAllData()
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (Project == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Project = "Project not found." });
                }
                return new ProjectEnvelope(Project);
            }
        }
    }
}