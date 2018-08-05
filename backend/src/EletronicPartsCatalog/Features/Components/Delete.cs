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

            private readonly ICurrentUserAccessor _currentUserAccessor;

            public QueryHandler(EletronicPartsCatalogContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task Handle(Command message, CancellationToken cancellationToken)
            {
                var component = await _context.Components
                    .Include(x => x.Author)
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (component == null)
                {
                    throw new RestException(HttpStatusCode.NotFound , new { Component = "Component not found." });
                }

                if (component.Author.Username != _currentUserAccessor.GetCurrentUsername())
                {
                    throw new RestException(HttpStatusCode.Unauthorized, new { Project = "Components can only be deleted by its owner." });
                }

                _context.Components.Remove(component);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}