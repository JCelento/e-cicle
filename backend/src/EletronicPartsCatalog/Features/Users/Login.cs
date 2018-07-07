using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using EletronicPartsCatalog.Infrastructure.Security;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Users
{
    public class Login
    {
        public class UserData
        {
            public string EmailOrUsername { get; set; }

            public string Password { get; set; }
        }

        public class UserDataValidator : AbstractValidator<UserData>
        {
            public UserDataValidator()
            {
                RuleFor(x => x.EmailOrUsername).NotEmpty().WithMessage(" O campo username/email deve ser preenchido.");
                RuleFor(x => x.EmailOrUsername).NotNull().WithMessage(" Username/email é obrigatório.");
                RuleFor(x => x.Password).NotNull().WithMessage(" A senha é obrigatória.");
                RuleFor(x => x.Password).NotEmpty().WithMessage(" O campo senha deve ser preenchido."); 
            }
        }

        public class Command : IRequest<UserEnvelope>
        {
            public UserData User { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.User).NotNull().SetValidator(new UserDataValidator());
            }
        }

        public class Handler : IRequestHandler<Command, UserEnvelope>
        {
            private readonly EletronicPartsCatalogContext _context;
            private readonly IPasswordHasher _passwordHasher;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;
            private readonly IMapper _mapper;

            public Handler(EletronicPartsCatalogContext context, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _jwtTokenGenerator = jwtTokenGenerator;
                _mapper = mapper;
            }

            public async Task<UserEnvelope> Handle(Command message, CancellationToken cancellationToken)
            {
                var person = await _context.Persons.Where(x => x.Email == message.User.EmailOrUsername || x.Username == message.User.EmailOrUsername).SingleOrDefaultAsync(cancellationToken);
                if (person == null)
                {
                    throw new RestException(HttpStatusCode.Unauthorized, new { User = "Username/email invalido." });
                }

                if (!person.Hash.SequenceEqual(_passwordHasher.Hash(message.User.Password, person.Salt)))
                {
                    throw new RestException(HttpStatusCode.Unauthorized, new { User = "Senha invalida." });
                }
             
                var user  = _mapper.Map<Api.Domain.Person, User>(person);
                user.Token = await _jwtTokenGenerator.CreateToken(person.Username);
                return new UserEnvelope(user);
            }
        }
    }
}
