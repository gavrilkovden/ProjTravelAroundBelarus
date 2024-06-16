using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Routes.Application.Dtos;
using Routes.Application.Handlers.Commands.CreateAttractionInRoute;
using Routes.Application.Handlers.Commands.CreateRoute;
using Routes.Application.Handlers.Commands.DeleteRoute;
using Routes.Application.Handlers.Commands.UpdateRoute;
using Routes.Application.Handlers.Queries.GetRotesCount;
using Routes.Application.Handlers.Queries.GetRoute;
using Routes.Application.Handlers.Queries.GetRoutes;

namespace TravelAroundBelarusProj.Api.Apis
{
    [Authorize]
    [ApiController]
    [Route("Api/Routes")]
    public class RoutesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoutesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Route")]
        public async Task<ActionResult> AddRoute(
          [FromBody] CreateRouteCommand createRouteCommand,
          CancellationToken cancellationToken)
        {
            var createdRoute = await _mediator.Send(createRouteCommand, cancellationToken);

            return Ok(createdRoute);
        }

        [HttpPost("AttractionInRoute")]
        public async Task<ActionResult> AddAttractionInRoute(
          [FromBody] CreateAttractionInRouteCommand createAttractionInRouteCommand,
          CancellationToken cancellationToken)
        {
            var createAttractionInRoute = await _mediator.Send(createAttractionInRouteCommand, cancellationToken);

            return Ok(createAttractionInRoute);
        }

        [HttpGet("id")]
        public async Task<ActionResult<GetRouteDto>> GetRouteById([FromQuery] GetRouteQuery getRouteQuery,
            CancellationToken cancellationToken = default)
        {
            var route = await _mediator.Send(getRouteQuery, cancellationToken);

            return Ok(route);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetRoutesDto>>> GetRoutes(
    [FromQuery] GetRoutesQuery getRoutesQuery,
    CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(getRoutesQuery, cancellationToken);
            Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }

        [HttpGet("count")]
        public Task<int> GetRoutesCount([FromQuery] GetRotesCountQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query, cancellationToken);
        }

        [HttpPut("id")]
        public Task<GetRouteDto> PutRoute(
           UpdateRouteCommand command,
           [FromQuery] int id,
           CancellationToken cancellationToken)
        {
            command.Id = id;
            return _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteRoute([FromRoute] int id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new DeleteRouteCommand { Id = id }, cancellationToken);
        }
    }
}
