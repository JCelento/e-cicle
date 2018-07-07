using System.Threading.Tasks;
using EletronicPartsCatalog.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EletronicPartsCatalog.Features.Projects
{
    [Route("Projects")]
    public class ProjectsController : Controller
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ProjectsEnvelope> Get([FromQuery] string tag, [FromQuery] string author, [FromQuery] string favorited, [FromQuery] string search, [FromQuery] string component, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await _mediator.Send(new List.Query(tag, author,favorited, search, component, limit, offset));
        }

        [HttpGet("feed")]
        public async Task<ProjectsEnvelope> GetFeed([FromQuery] string tag, [FromQuery] string author, [FromQuery] string favorited, [FromQuery] string search, [FromQuery] string component, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await _mediator.Send(new List.Query(tag, author, favorited, search, component, limit, offset)
            {
                IsFeed = true
            });
        }

        [HttpGet("{slug}")]
        public async Task<ProjectEnvelope> Get(string slug)
        {
            return await _mediator.Send(new Details.Query(slug));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<ProjectEnvelope> Create([FromBody]Create.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{slug}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<ProjectEnvelope> Edit(string slug, [FromBody]Edit.Command command)
        {
            command.Slug = slug;
            return await _mediator.Send(command);
        }

        [HttpDelete("{slug}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task Delete(string slug)
        {
            await _mediator.Send(new Delete.Command(slug));
        }
    }
}