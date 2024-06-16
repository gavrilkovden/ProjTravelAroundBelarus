using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tours.Application.Dtos;
using Tours.Application.Handlers.TourFeedbacks.Commands.CreateFeedbackTour;
using Tours.Application.Handlers.TourFeedbacks.Commands.DeleteFeedbackTour;
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

        [HttpPost("tour")]
        public async Task<ActionResult> AddTour(
          [FromBody] CreateTourCommand createTourCommand,
          CancellationToken cancellationToken)
        {
            var createdTour = await _mediator.Send(createTourCommand, cancellationToken);

            return Ok(createdTour);
        }

        [HttpGet("id")]
        public async Task<ActionResult<GetTourDto>> GetTourById([FromQuery] GetTourQuery getTourQuery,
    CancellationToken cancellationToken = default)
        {
            var tour = await _mediator.Send(getTourQuery, cancellationToken);

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
        public Task<int> GetToursCount([FromQuery] GetToursCountQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query, cancellationToken);
        }

        [HttpPut("id")]
        public Task<GetTourDto> PutRoute(UpdateTourCommand command,
            [FromQuery] int id,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            return _mediator.Send(command, cancellationToken);
        }

        [HttpPatch("{id}/IsApproved")]
        public Task<GetTourDto> PatchIsApproved(UpdateTourStatusCommand command, [FromRoute] int id, CancellationToken cancellationToken)
        {
            command.Id = id;
            return _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteTour([FromRoute] int id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new DeleteTourCommand { Id = id }, cancellationToken);
        }

        [HttpPost("FeedbackTour")]
        public async Task<ActionResult> AddTourFeedback(
            [FromBody] CreateFeedbackTourCommand createFeedbackTourCommand,
            CancellationToken cancellationToken)
        {
            var createdFeedbackTour = await _mediator.Send(createFeedbackTourCommand, cancellationToken);

            return Ok(createdFeedbackTour);
        }
        [HttpGet("FeedbackTour /id")]
        public async Task<ActionResult<GetFeedbackTourDto>> GetFeedbackTourById([FromQuery] GetFeedbackTourQuery getFeedbackTourQuery,
   CancellationToken cancellationToken = default)
        {
            var tour = await _mediator.Send(getFeedbackTourQuery, cancellationToken);

            return Ok(tour);
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
        public Task<int> GetFeedbackToursCount([FromQuery] GetFeedbackToursCountQuery query, CancellationToken cancellationToken)
        {
            return _mediator.Send(query, cancellationToken);
        }

        [HttpDelete("FeedbackTour {id}")]
        public Task DeleteFeedbackTour([FromRoute] int id, CancellationToken cancellationToken)
        {
            return _mediator.Send(new DeleteFeedbackTourCommand { Id = id }, cancellationToken);
        }
    }
}
