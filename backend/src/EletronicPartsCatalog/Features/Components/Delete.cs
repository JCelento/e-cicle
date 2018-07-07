using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Components
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
                var component = await _context.Components
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (component == null)
                {
                    throw new RestException(HttpStatusCode.NotFound , new { Component = "Component not found." });
                }

                _context.Components.Remove(component);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}