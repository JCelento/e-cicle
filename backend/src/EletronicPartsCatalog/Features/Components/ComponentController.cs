using System.Threading.Tasks;
using EletronicPartsCatalog.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EletronicPartsCatalog.Features.Components
{
    [Route("Components")]
    public class ComponentsController : Controller
    {
        private readonly IMediator _mediator;

        public ComponentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ComponentsEnvelope> Get([FromQuery] string name, [FromQuery] string wheretoFindIt, [FromQuery] string search, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await _mediator.Send(new List.Query(name, wheretoFindIt, search, limit, offset));
        }

        [HttpGet("{slug}")]
        public async Task<ComponentEnvelope> Get(string slug)
        {
            return await _mediator.Send(new Details.Query(slug));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<ComponentEnvelope> Create([FromBody]Create.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{slug}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<ComponentEnvelope> Edit(string slug, [FromBody]Edit.Command command)
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