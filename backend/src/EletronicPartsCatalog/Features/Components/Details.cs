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
    public class Details
    {
        public class Query : IRequest<ComponentEnvelope>
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

        public class QueryHandler : IRequestHandler<Query, ComponentEnvelope>
        {
            private readonly EletronicPartsCatalogContext _context;

            public QueryHandler(EletronicPartsCatalogContext context)
            {
                _context = context;
            }

            public async Task<ComponentEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var component = await _context.Components.GetAllData()
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (component == null)
                {
                   throw new RestException(HttpStatusCode.NotFound, new { Component = "Component not found." });
                }
                return new ComponentEnvelope(component);
            }
        }
    }
}