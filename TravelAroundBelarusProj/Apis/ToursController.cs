using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tours.Application.Dtos;
using Tours.Application.Handlers.TourFeedbacks.Commands.CreateFeedbackTour;
using Tours.Application.Handlers.TourFeedbacks.Commands.DeleteFeedbackTour;
using Tours.Application.Handlers.TourFeedbacks.Commands.UpdateFeedbackTour;
using Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbacksTour;
using Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbacksTourCount;
using Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbackTour;
using Tours.Application.Handlers.Tours.Commands.CreateTour;
using Tours.Application.Handlers.Tours.Commands.DeleteTour;
using Tours.Application.Handlers.Tours.Commands.UpdateTour;
using Tours.Application.Handlers.Tours.Commands.UpdateTourStatus;
using Tours.Application.Handlers.Tours.Queries.GetTour;
using Tours.Application.Handlers.Tours.Queries.GetTours;
using Tours.Application.Handlers.Tours.Queries.GetToursCount;

namespace TravelAroundBelarusProj.Api.Apis
{
    [Authorize]
    [ApiController]
    [Route("Api/Tours")]
    public class ToursController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ToursController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> AddTour(
          [FromBody] CreateTourCommand createTourCommand,
          CancellationToken cancellationToken)
        {
            var createdTour = await _mediator.Send(createTourCommand, cancellationToken);

            return Ok(createdTour);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetTourDto>> GetTourById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var tour = await _mediator.Send(new GetTourQuery { Id = id }, cancellationToken);
            return Ok(tour);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTourDto>>> GetTours(
            [FromQuery] GetToursQuery getToursQuery,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(getToursQuery, cancellationToken);
            Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }

        [HttpGet("count")]
        public async Task<int> GetToursCount([FromQuery] GetToursCountQuery query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task<GetTourDto> UpdateRoute([FromRoute] int id, [FromBody] UpdateTourCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPatch("{id}/IsApproved")]
        public async Task<GetTourDto> UpdateIsApproved([FromRoute] int id, [FromBody] UpdateTourStatusCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTour([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteTourCommand { Id = id }, cancellationToken);
            return Ok();
        }

        [HttpPost("FeedbackTour")]
        public async Task<ActionResult> AddTourFeedback(
            [FromBody] CreateFeedbackTourCommand createFeedbackTourCommand,
            CancellationToken cancellationToken)
        {
            var createdFeedbackTour = await _mediator.Send(createFeedbackTourCommand, cancellationToken);

            return Ok(createdFeedbackTour);
        }

        [HttpPut("FeedbackTour/{id}")]
        public async Task<ActionResult> UpdateTourFeedback(
            [FromRoute] int id,
            [FromBody] UpdateFeedbackTourCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            var updateFeedback = await _mediator.Send(command, cancellationToken);
            return Ok(updateFeedback);
        }

        [HttpGet("FeedbackTour/{id}")]
        public async Task<ActionResult<GetFeedbackTourDto>> GetFeedbackTourById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetFeedbackTourQuery { Id = id }, cancellationToken);
        }

        [HttpGet("FeedbackTours")]
        public async Task<ActionResult<IEnumerable<GetFeedbackTourDto>>> GetFeedbackTours(
            [FromQuery] GetFeedbackToursQuery getFeedbackToursQuery,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(getFeedbackToursQuery, cancellationToken);
            Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }

        [HttpGet("FeedbackTours/count")]
        public async Task<int> GetFeedbackToursCount([FromQuery] GetFeedbackToursCountQuery query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpDelete("FeedbackTour/{id}")]
        public async Task<ActionResult> DeleteFeedbackTour([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteFeedbackTourCommand { Id = id }, cancellationToken);
            return Ok();
        }
    }
}
