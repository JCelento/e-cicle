using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Users
{
    public class Details
    {
        public class Query : IRequest<UserEnvelope>
        {
            public string Username { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Username).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, UserEnvelope>
        {
            private readonly EletronicPartsCatalogContext _context;
            private readonly IMapper _mapper;

            public QueryHandler(EletronicPartsCatalogContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UserEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var person = await _context.Persons
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Username == message.Username, cancellationToken);
                if (person == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { User = "User not found." });
                }
                return new UserEnvelope(_mapper.Map<Api.Domain.Person, User>(person));
            }
        }
    }
}