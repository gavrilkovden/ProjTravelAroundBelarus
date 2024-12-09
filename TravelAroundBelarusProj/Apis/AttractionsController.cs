using Attractions.Application.Dtos;
using Attractions.Application.Handlers.AttractionFeedbacks.Commands.CreateFeedbackAttraction;
using Attractions.Application.Handlers.AttractionFeedbacks.Commands.DeleteFeedbackAttraction;
using Attractions.Application.Handlers.AttractionFeedbacks.Commands.UpdateFeedbackAttraction;
using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttraction;
using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractions;
using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractionsCount;
using Attractions.Application.Handlers.Attractions.Commands.CreateAttraction;
using Attractions.Application.Handlers.Attractions.Commands.DeleteAttraction;
using Attractions.Application.Handlers.Attractions.Commands.DeleteImage;
using Attractions.Application.Handlers.Attractions.Commands.UpdateAttraction;
using Attractions.Application.Handlers.Attractions.Commands.UpdateAttractionStatus;
using Attractions.Application.Handlers.Attractions.Commands.UpdateImageApproveStatus;
using Attractions.Application.Handlers.Attractions.Commands.UploadImage;
using Attractions.Application.Handlers.Attractions.Queries.GetAttraction;
using Attractions.Application.Handlers.Attractions.Queries.GetAttractions;
using Attractions.Application.Handlers.Attractions.Queries.GetAttractionsCount;
using Attractions.Application.Handlers.Attractions.Queries.GetImages;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tours.Application.Handlers.TourFeedbacks.Commands.UpdateFeedbackTour;
using Travel.Application.Dtos;

namespace TravelAroundBelarusProj.Api.Apis
{
    [ApiController]
    [Route("Api/Attractions")]
    public class AttractionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttractionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddAttraction(
          [FromBody] CreateAttractionCommand createAttractionCommand,
          CancellationToken cancellationToken)
        {
            var createdAttraction = await _mediator.Send(createAttractionCommand, cancellationToken);

            return Ok(createdAttraction);
        }
        [Authorize]
        [HttpPost("Image")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> AddImage(
        [FromForm] UploadImageCommand uploadImageCommand,
        CancellationToken cancellationToken)
        {
            var UploadImage = await _mediator.Send(uploadImageCommand, cancellationToken);

            return Ok(UploadImage);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAttractionDto>> GetAttractionById([FromRoute] int id, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetAttractionQuery { Id = id }, cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAttractionsDto>>> GetAttractions(
            [FromQuery] GetAttractionsQuery getAttractionsQuery,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(getAttractionsQuery, cancellationToken);
            Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }

        [HttpGet("Images")]
        public async Task<ActionResult<IEnumerable<GetImageDto>>> GetImages(
            [FromQuery] GetImagesQuery getImagesQuery,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(getImagesQuery, cancellationToken);
            Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }

        [HttpGet("count")]
        public async Task<int> GetAttractionsCount([FromQuery] GetAttractionsCountQuery query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<GetAttractionDto> UpdateAttraction([FromRoute] int id,
           [FromBody] UpdateAttractionCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            return await _mediator.Send(command, cancellationToken);
        }

        [Authorize]
        [HttpPatch("{id}/IsApproved/Attraction")]
        public async Task<GetAttractionDto> PatchIsApprovedAttraction([FromRoute] int id, [FromBody] UpdateAttractionStatusCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            return await _mediator.Send(command, cancellationToken);
        }

        [Authorize]
        [HttpPatch("{id}/IsApproved/Image")]
        public async Task<GetImageDto> PatchIsApprovedImage([FromRoute] int id, [FromBody] UpdateImageApproveStatusCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            return await _mediator.Send(command, cancellationToken);
        }

        [Authorize]
        [HttpDelete("Images/{id}")]
        public async Task<ActionResult> DeleteImages([FromRoute] int id, CancellationToken cancellationToken)
        {
             await _mediator.Send(new DeleteImageCommand { Id = id }, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAttraction([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteAttractionCommand { Id = id }, cancellationToken);
            return Ok();
        }

        [Authorize]
        [HttpPost("AttractionFeedback")]
        public async Task<ActionResult> AddAttractionFeedback(
            [FromBody] CreateFeedbackAttractionCommand createFeedbackAttractionCommand,
            CancellationToken cancellationToken)
        {
            var createdAttractionFeedback = await _mediator.Send(createFeedbackAttractionCommand, cancellationToken);

            return Ok(createdAttractionFeedback);
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete("AttractionFeedback/{id}")]
        public async Task<ActionResult> DeleteAttractionFeedback([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteFeedbackAttractionCommand { Id = id }, cancellationToken);
            return Ok();
        }
    }
}
