using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Routes.Application.Dtos;
using Routes.Application.Handlers.Commands.CreateAttractionInRoute;
using Routes.Application.Handlers.Commands.CreateRoute;
using Routes.Application.Handlers.Commands.DeleteAttractionInRoute;
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

        [HttpPost]
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

        [HttpDelete("AttractionInRoute{id}")]
        public async Task<ActionResult> DeleteAttractionInRoute([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteAttractionInRouteCommand { Id = id }, cancellationToken);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetRouteDto>> GetRouteById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetRouteQuery { Id = id }, cancellationToken);
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
        public async Task<int> GetRoutesCount([FromQuery] GetRotesCountQuery query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<GetRouteDto> PutRoute([FromRoute] int id, [FromBody] UpdateRouteCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoute([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteRouteCommand { Id = id }, cancellationToken);
            return Ok();
        }
    }
}
