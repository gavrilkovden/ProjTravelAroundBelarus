using Attractions.Application.Handlers.AttractionFeedbacks.Commands.CreateFeedbackAttraction;
using Attractions.Application.Handlers.AttractionFeedbacks.Commands.DeleteFeedbackAttraction;
using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttraction;
using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractions;
using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractionsCount;
using Attractions.Application.Handlers.Attractions.Commands.CreateAttraction;
using Attractions.Application.Handlers.Attractions.Commands.DeleteAttraction;
using Attractions.Application.Handlers.Attractions.Commands.UpdateAttraction;
using Attractions.Application.Handlers.Attractions.Commands.UpdateAttractionStatus;
using Attractions.Application.Handlers.Attractions.Queries.GetAttraction;
using Attractions.Application.Handlers.Attractions.Queries.GetAttractions;
using Attractions.Application.Handlers.Attractions.Queries.GetAttractionsCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel.Application.Dtos;

namespace TravelAroundBelarusProj.Api.Apis
{
    [Authorize]
    [ApiController]
    [Route("Api/Attractions")]
    public class AttractionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttractionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> AddAttraction(
          [FromBody] CreateAttractionCommand createAttractionCommand,
          CancellationToken cancellationToken)
        {
            var createdAttraction = await _mediator.Send(createAttractionCommand, cancellationToken);

            return Ok(createdAttraction);
        }


        [HttpGet("id")]
        public async Task<ActionResult<GetAttractionDto>> GetAttractionById([FromQuery] GetAttractionQuery getAttractionQuery,
            CancellationToken cancellationToken = default)
        {
            var attraction = await _mediator.Send(getAttractionQuery, cancellationToken);

            return Ok(attraction);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAttractionDto>>> GetAttractions(
            [FromQuery] GetAttractionsQuery getAttractionsQuery,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(getAttractionsQuery, cancellationToken);
            Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }


        [HttpGet("count")]
        public Task<int> GetAttractionsCount([FromQuery] GetAttractionsCountQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query, cancellationToken);
        }

        [HttpPut("id")]
        public Task<GetAttractionDto> PutAttraction(
            UpdateAttractionCommand command,
            [FromQuery] int id,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            return _mediator.Send(command, cancellationToken);
        }

        [HttpPatch("{id}/IsApproved")]
        public Task<GetAttractionDto> PatchIsApproved(UpdateAttractionStatusCommand command, [FromRoute] int id, CancellationToken cancellationToken)
        {
            command.Id = id;
            return _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteAttraction([FromRoute] int id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new DeleteAttractionCommand { Id = id }, cancellationToken);
        }

        [HttpPost("AttractionFeedback")]
        public async Task<ActionResult> AddAttractionFeedback(
            [FromBody] CreateFeedbackAttractionCommand createFeedbackAttractionCommand,
            CancellationToken cancellationToken)
        {
            var createdAttractionFeedback = await _mediator.Send(createFeedbackAttractionCommand, cancellationToken);

            return Ok(createdAttractionFeedback);
        }

        [HttpGet("AttractionFeedbacks")]
        public async Task<ActionResult<IEnumerable<GetFeedbackAttractionDto>>> GetAttractionFeedbacks(
            [FromQuery] GetFeedbackAttractionsQuery getFeedbackAttractionsQuery,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(getFeedbackAttractionsQuery, cancellationToken);
            Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }

        [HttpGet("AttractionFeedback/id")]
        public async Task<ActionResult<GetFeedbackAttractionDto>> GetAttractionFeedbackById([FromQuery] GetFeedbackAttractionQuery getFeedbackAttractionQuery,CancellationToken cancellationToken = default)
        {
            var FeedbackAttraction = await _mediator.Send(getFeedbackAttractionQuery, cancellationToken);

            return Ok(FeedbackAttraction);
        }

        [HttpGet("AttractionFeedbacks/count")]
        public Task<int> GetFeedbackAttractionsCount([FromQuery] GetFeedbackAttractionsCountQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query, cancellationToken);
        }

        [HttpDelete("AttractionFeedback {id}")]
        public Task DeleteAttractionFeedback([FromRoute] int id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new DeleteFeedbackAttractionCommand { Id = id }, cancellationToken);
        }
    }
}
