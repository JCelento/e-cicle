using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Api.Resources.Images;
using EletronicPartsCatalog.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Components
{
    public class Create
    {
        public class ComponentData
        {
            public string Name { get; set; }

            public string ComponentImage { get; set; }

            public string Description { get; set; }


            public string[] WhereToFindItList { get; set; }
        }

        public class ComponentDataValidator : AbstractValidator<ComponentData>
        {
            public ComponentDataValidator()
            {
                RuleFor(x => x.Name).NotNull().WithMessage(" O nome do componente é obrigatório.");
                RuleFor(x => x.Name).NotEmpty().WithMessage(" O campo nome deve ser preenchido."); ;
                RuleFor(x => x.WhereToFindItList).NotNull()
                    .WithMessage(" A indicação de onde conseguir o componente é obrigatória.");
                RuleFor(x => x.WhereToFindItList).NotEmpty().WithMessage(" O campo de onde conseguir o componente deve ser preenchido."); ;
            }
        }

        public class Command : IRequest<ComponentEnvelope>
        {
            public ComponentData Component { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Component).NotNull().SetValidator(new ComponentDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, ComponentEnvelope>
        {
            private readonly EletronicPartsCatalogContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public Handler(EletronicPartsCatalogContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<ComponentEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var author = await _context.Persons.FirstAsync(x => x.Username == _currentUserAccessor.GetCurrentUsername(), cancellationToken);

                var whereToFindIt = new List<WhereToFindIt>();
                foreach(var where in (message.Component.WhereToFindItList ?? Enumerable.Empty<string>()))
                {
                    var w = await _context.WhereToFind.FindAsync(where);
                    if (w == null)
                    {
                        w = new WhereToFindIt()
                        {
                            WhereToFindItId = where
                        };
                        await _context.WhereToFind.AddAsync(w, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    whereToFindIt.Add(w);
                }

                var component = new Component()
                {
                    ComponentId = message.Component.Name,
                    Author = author,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Description = message.Component.Description,
                    ComponentImage = string.IsNullOrEmpty(message.Component.ComponentImage) ? ImagePath.ImageNotProvided : message.Component.ComponentImage,
                    Slug = message.Component.Name.GenerateSlug()
                };
                await _context.Components.AddAsync(component, cancellationToken);

                await _context.ComponentWhereToFindIt.AddRangeAsync(whereToFindIt.Select(x => new ComponentWhereToFindIt()
                {
                    Component = component,
                    WhereToFindIt = x
                }), cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return new ComponentEnvelope(component);
            }
        }
    }
}
