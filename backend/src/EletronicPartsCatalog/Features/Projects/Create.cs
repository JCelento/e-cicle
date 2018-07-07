using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Api.Resources.Images;
using EletronicPartsCatalog.Api.Domain;
using EletronicPartsCatalog.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Projects
{
    public class Create
    {
        public class ProjectData
        {
            public string Title { get; set; }

            public string ProjectImage { get; set; }

            public string Description { get; set; }

            public string Body { get; set; }

            public string[] TagList { get; set; }
        
            public string[] ComponentList { get; set; }

        }

        public class ProjectDataValidator : AbstractValidator<ProjectData>
        {
            public ProjectDataValidator()
            {
                RuleFor(x => x.Title).NotNull().WithMessage(" O titulo do projeto é obrigatório.");
                RuleFor(x => x.Title).NotEmpty().WithMessage(" O campo titulo deve ser preenchido.");
                RuleFor(x => x.Description).NotNull().WithMessage(" A descrição do projeto é obrigatória");
                RuleFor(x => x.Description).NotEmpty().WithMessage(" O campo descrição deve ser preenchido."); ;
                RuleFor(x => x.Body).NotNull().WithMessage(" Uma definição do projeto é obrigatória.");
                RuleFor(x => x.Body).NotEmpty().WithMessage(" A definição do projeto deve ser preenchida."); ;
            }
        }

        public class Command : IRequest<ProjectEnvelope>
        {
            public ProjectData Project { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Project).NotNull().SetValidator(new ProjectDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, ProjectEnvelope>
        {
            private readonly EletronicPartsCatalogContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;
            public Handler(EletronicPartsCatalogContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<ProjectEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var author = await _context.Persons.FirstAsync(x => x.Username == _currentUserAccessor.GetCurrentUsername(), cancellationToken);

                var components = new List<Component>();
                foreach (var component in (message.Project.ComponentList ?? Enumerable.Empty<string>()))
                {
                    var c = await _context.Components.FindAsync(component);
                    if (c == null)
                    {
                        c = new Component()
                        {
                            ComponentId = component,
                            CreatedAt = DateTime.Now,
                            UpdatedAt = DateTime.Now,
                            Slug = component.GenerateSlug()
                        };
                        await _context.Components.AddAsync(c, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    components.Add(c);
                }
                var tags = new List<Tag>();
                foreach(var tag in (message.Project.TagList ?? Enumerable.Empty<string>()))
                {
                    var t = await _context.Tags.FindAsync(tag);
                    if (t == null)
                    {
                        t = new Tag()
                        {
                            TagId = tag
                        };
                        await _context.Tags.AddAsync(t, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    tags.Add(t);
                }

                var Project = new Project()
                {
                    Author = author,
                    Body = message.Project.Body,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Description = message.Project.Description,
                    ProjectImage = string.IsNullOrEmpty(message.Project.ProjectImage) ? ImagePath.ImageNotProvided : message.Project.ProjectImage,
                    Title = message.Project.Title,
                    Slug = message.Project.Title.GenerateSlug()
                };
                await _context.Projects.AddAsync(Project, cancellationToken);

                await _context.ProjectComponents.AddRangeAsync(components.Select(x => new ProjectComponent()
                {
                    Project = Project,
                    Component = x
                }), cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                await _context.ProjectTags.AddRangeAsync(tags.Select(x => new ProjectTag()
                {
                    Project = Project,
                    Tag = x
                }), cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return new ProjectEnvelope(Project);
            }
        }
    }
}
