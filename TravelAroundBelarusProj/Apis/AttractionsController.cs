using Attractions.Application.Handlers.AttractionFeedbacks.Commands.CreateFeedbackAttraction;
using Attractions.Application.Handlers.AttractionFeedbacks.Commands.DeleteFeedbackAttraction;
using Attractions.Application.Handlers.AttractionFeedbacks.Commands.UpdateFeedbackAttraction;
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
using Tours.Application.Handlers.TourFeedbacks.Commands.UpdateFeedbackTour;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAttractionDto>> GetAttractionById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetAttractionQuery { Id = id }, cancellationToken);
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
        public async Task<int> GetAttractionsCount([FromQuery] GetAttractionsCountQuery query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<GetAttractionDto> UpdateAttraction([FromRoute] int id,
           [FromBody] UpdateAttractionCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPatch("{id}/IsApproved")]
        public async Task<GetAttractionDto> PatchIsApproved([FromRoute] int id, [FromBody] UpdateAttractionStatusCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttraction([FromRoute] int id, CancellationToken cancellationToken)
        {
             await _mediator.Send(new DeleteAttractionCommand { Id = id }, cancellationToken);
            return Ok();
        }

        [HttpPost("AttractionFeedback")]
        public async Task<ActionResult> AddAttractionFeedback(
            [FromBody] CreateFeedbackAttractionCommand createFeedbackAttractionCommand,
            CancellationToken cancellationToken)
        {
            var createdAttractionFeedback = await _mediator.Send(createFeedbackAttractionCommand, cancellationToken);

            return Ok(createdAttractionFeedback);
        }

        [HttpPut("AttractionFeedback/{id}")]
        public async Task<ActionResult> AddAttractionFeedback([FromRoute] int id,
            [FromBody] UpdateFeedbackAttractionCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            var updateAttractionFeedback = await _mediator.Send(command, cancellationToken);

            return Ok(updateAttractionFeedback);
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

        [HttpGet("AttractionFeedback/{id}")]
        public async Task<ActionResult<GetFeedbackAttractionDto>> GetAttractionFeedbackById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetFeedbackAttractionQuery { Id = id }, cancellationToken);
        }

        [HttpGet("AttractionFeedbacks/count")]
        public async Task<int> GetFeedbackAttractionsCount([FromQuery] GetFeedbackAttractionsCountQuery query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpDelete("AttractionFeedback/{id}")]
        public async Task<ActionResult> DeleteAttractionFeedback([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteFeedbackAttractionCommand { Id = id }, cancellationToken);
            return Ok();
        }
    }
}
