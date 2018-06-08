using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Domain;
using EletronicPartsCatalog.Features.Projects;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Favorites
{
    public class Add
    {
        public class Command : IRequest<ProjectEnvelope>
        {
            public Command(string slug)
            {
                Slug = slug;
            }

            public string Slug { get; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                DefaultValidatorExtensions.NotNull(RuleFor(x => x.Slug)).NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Command, ProjectEnvelope>
        {
            private readonly EletronicPartsCatalogContext _context;
            private readonly ICurrentUserAccessor _currentUserAccessor;

            public QueryHandler(EletronicPartsCatalogContext context, ICurrentUserAccessor currentUserAccessor)
            {
                _context = context;
                _currentUserAccessor = currentUserAccessor;
            }

            public async Task<ProjectEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var Project = await _context.Projects.FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (Project == null)
                {
                    throw new RestException(HttpStatusCode.NotFound);
                }
                
                var person = await _context.Persons.FirstOrDefaultAsync(x => x.Username == _currentUserAccessor.GetCurrentUsername(), cancellationToken);

                var favorite = await _context.ProjectFavorites.FirstOrDefaultAsync(x => x.ProjectId == Project.ProjectId && x.PersonId == person.PersonId, cancellationToken);

                if (favorite == null)
                {
                    favorite = new ProjectFavorite()
                    {
                        Project = Project,
                        ProjectId = Project.ProjectId,
                        Person = person,
                        PersonId = person.PersonId
                    };
                    await _context.ProjectFavorites.AddAsync(favorite, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                return new ProjectEnvelope(await _context.Projects.GetAllData()
                    .FirstOrDefaultAsync(x => x.ProjectId == Project.ProjectId, cancellationToken));
            }
        }
    }
}