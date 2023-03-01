using Daisy.Shared.Requests.Event;
using Daisy.Shared.Responses.Event;

namespace Daisy.Client.Wasm.ApiClients.Event
{
    public interface IEventApiClient
    {
        Task<CreateEventResponse> CreateEvent(CreateEventRequest request);
        Task<List<GetAllEventsResponse>> GetAllEvents();
        Task<GetEventByIdResponse> GetById(GetEventByIdRequest request);
        Task<UpdateEventResponse> UpdateEvent(int Id, UpdateEventRequest request);
        Task<CancelEventResponse> Cancel(int Id);
        Task<CancelEventResponse> CancelEvent(int Id, CancelEventRequest request);
    }
}
