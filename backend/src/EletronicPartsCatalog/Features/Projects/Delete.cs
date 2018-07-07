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
    public class Delete
    {
        public class Command : IRequest
        {
            public Command(string slug)
            {
                Slug = slug;
            }

            public string Slug { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Slug).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Command>
        {
            private readonly EletronicPartsCatalogContext _context;

            public QueryHandler(EletronicPartsCatalogContext context)
            {
                _context = context;
            }

            public async Task Handle(Command message, CancellationToken cancellationToken)
            {
                var Project = await _context.Projects
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (Project == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Project = "Project not found." });
                }

                _context.Projects.Remove(Project);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}