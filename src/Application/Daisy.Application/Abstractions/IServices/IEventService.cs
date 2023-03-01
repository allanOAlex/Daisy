using Daisy.Shared.Requests.Event;
using Daisy.Shared.Responses.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IServices
{
    public interface IEventService
    {
        Task<List<GetAllEventsResponse>> GetAllEvents();
        Task<GetEventByIdResponse> GetById(int Id);
        Task<CreateEventResponse> CreateEvent(CreateEventRequest request);
        Task<UpdateEventResponse> UpdateEvent(UpdateEventRequest request);
        Task<CancelEventResponse> Cancel(GetEventByIdResponse request);
        Task<CancelEventResponse> CancelEvent(CancelEventRequest request);
    }
}
