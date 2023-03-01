using Daisy.Api.Extensions;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Shared.Requests.Event;
using Daisy.Shared.Responses.Appointments;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Daisy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenCORSPolicy")]
    [Authorize]
    public class EventController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public EventController(IServiceManager ServiceManager)
        {
            serviceManager = ServiceManager;
        }


        [HttpPost("create")]
        public async Task<ActionResult<CreateEventResponse>> CreateNewEvent(CreateEventRequest request)
        {
            try
            {
                var validDates = ApiExtensions.ValidateDates(request.StartDate, request.Time, request.EndDate);

                if (validDates != 0)
                {
                    switch (validDates)
                    {
                        case -1:
                            return new CreateEventResponse { Successful = false, Message = "Start Date must be greater than today" };

                        case -3:
                            return new CreateEventResponse { Successful = false, Message = "End Date must be greater than today" };

                        case 1:
                            return new CreateEventResponse { Successful = false, Message = "Start Date must not be null" };

                        case 2:
                            return new CreateEventResponse { Successful = false, Message = "Time must not be null" };

                        case 3:
                            return new CreateEventResponse { Successful = false, Message = "End Date must not be null" };

                        default:
                            break;
                    }
                }

                var createdEvent = await serviceManager.EventService.CreateEvent(request);
                return createdEvent.Successful == true ? StatusCode(StatusCodes.Status201Created, new CreateEventResponse { Successful = true, Message = "Event created successfully!" }) : StatusCode(StatusCodes.Status500InternalServerError, new CreateEventResponse { Successful = createdEvent.Successful, Message = $"{createdEvent.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating event | {ex.Message}");
            }
            
        }

        [AllowAnonymous]
        [HttpGet("fetchall")]
        public async Task<ActionResult<List<GetAllEventsResponse>>> GetAllEvents()
        {
            var events = await serviceManager.EventService.GetAllEvents();
            if (events != null)
                return Ok(events);

            return events;

        }

        [HttpGet("getbyId/{Id}")]
        public async Task<ActionResult<GetEventByIdResponse>> GetById(int Id)
        {
            var eventToFind = await serviceManager.EventService.GetById(Id);
            if (eventToFind == null)
                return new GetEventByIdResponse() { Successful = false, Message = $"Event with Id : {Id} not found" };

            return Ok(eventToFind);

        }

        [HttpPut("update/{Id}")]
        public async Task<ActionResult<UpdateEventResponse>> UpdateEvent(int Id, UpdateEventRequest request)
        {
            if (Id != request.Id)
                return new UpdateEventResponse() { Successful = false, Message = $"Event ID mismatch" };

            var eventToUpdate = await serviceManager.EventService.GetById(Id);

            if (eventToUpdate == null)
                return new UpdateEventResponse() { Successful = false, Message = $"Event with Id : {Id} not found" };

            var updatedEvent = await serviceManager.EventService.UpdateEvent(request);
            return Ok(updatedEvent);

        }

        [HttpPut("cancel/{Id}")]
        public async Task<ActionResult<CancelEventResponse>> Cancel(int Id)
        {
            var eventToCancel = await serviceManager.EventService.GetById(Id);
            if (eventToCancel == null)
                return new CancelEventResponse() { Successful = false, Message = $"Event with Id : {Id} not found" };

            var cancelledEvent = await serviceManager.EventService.Cancel(eventToCancel);
            return Ok(cancelledEvent);

        }

        [HttpPut("cancelevent/{Id}")]
        public async Task<ActionResult<CancelEventResponse>> CancelEvent(int Id, CancelEventRequest request)
        {
            if (Id != request.Id)
                return new CancelEventResponse() { Successful = false, Message = $"Event ID mismatch" };

            var eventToCancel = await serviceManager.EventService.GetById(Id);

            if (eventToCancel == null)
                return new CancelEventResponse() { Successful = false, Message = $"Event with Id : {Id} not found" };

            var cancelledEvent = await serviceManager.EventService.CancelEvent(request);
            return Ok(cancelledEvent);

        }


    }
}
