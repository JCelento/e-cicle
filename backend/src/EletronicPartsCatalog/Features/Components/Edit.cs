using System;
using System.Linq;
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
    public class Edit
    {
        public class ComponentData
        {
 
            public string ComponentImage { get; set; }

            public string Description { get; set; }
        }

        public class Command : IRequest<ComponentEnvelope>
        {
            public ComponentData Component { get; set; }
            public string Slug { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Component).NotNull();
            }
        }

        public class Handler : IRequestHandler<Command, ComponentEnvelope>
        {
            private readonly EletronicPartsCatalogContext _context;

            public Handler(EletronicPartsCatalogContext context)
            {
                _context = context;
            }

            public async Task<ComponentEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var component = await _context.Components
                    .Where(x => x.Slug == message.Slug)
                    .FirstOrDefaultAsync(cancellationToken);

                if (component == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Component = "Component not found." });
                }


                component.ComponentImage = message.Component.ComponentImage ?? component.ComponentImage;
                component.Description = message.Component.Description ?? component.Description;
                component.Slug = component.ComponentId.GenerateSlug();

                if (_context.ChangeTracker.Entries().First(x => x.Entity == component).State == EntityState.Modified)
                {
                    component.UpdatedAt = DateTime.UtcNow;
                }
                
                await _context.SaveChangesAsync(cancellationToken);

                return new ComponentEnvelope(await _context.Components.GetAllData()
                    .Where(x => x.Slug == component.Slug)
                    .FirstOrDefaultAsync(cancellationToken));            }
        }
    }
}
