using Azure;
using Daisy.Client.Wasm.ApiClients.User;
using Daisy.Shared.Requests.Event;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Daisy.Client.Wasm.ApiClients.Event
{
    public class EventApiClient : IEventApiClient
    {
        private readonly HttpClient client;
        private readonly IConfiguration config;
        private readonly ILogger<UserApiClient> logger;
        private string responseMessage;

        public EventApiClient(HttpClient Client, IConfiguration Config, ILogger<UserApiClient> Logger)
        {
            client = Client;
            config = Config;
            logger = Logger;
        }

        public async Task<CreateEventResponse> CreateEvent(CreateEventRequest request)
        {
            var response = await client.PostAsJsonAsync(config["Api:Routes:Event:CreateEvent"], request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(EventApiClient)}|(CreateEvent)API response not sucessful.", response);
                return new CreateEventResponse() { Successful = false, Message = $"Error Creating Event | {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var createdEvent = JsonConvert.DeserializeObject<CreateEventResponse>(apiResponse);

            return createdEvent;
        }

        public async Task<List<GetAllEventsResponse>> GetAllEvents()
        {
            var response = await client.GetFromJsonAsync<List<GetAllEventsResponse>>(config["Api:Routes:Event:GetAllEvents"]);
            if (response.Count <= 0)
            {
                logger.LogError($"{nameof(EventApiClient)}|(GetAllEvents)|No Records found");
                return new List<GetAllEventsResponse>();
            }

            return response;

        }

        public async Task<GetEventByIdResponse> GetById(GetEventByIdRequest request)
        {
            var response = await client.GetFromJsonAsync<GetEventByIdResponse>($"{config["Api:Routes:Event:GetById"]}{request.Id}");
            if (!response.Successful)
            {
                logger.LogError($"{nameof(EventApiClient)}|(GetById)|Error while fetching event with Id {request.Id}");
                return new GetEventByIdResponse() { Successful = false, Message = $"Error fetching event" };
            }

            logger.LogError($"{nameof(UserApiClient)}|(GetById)|Event with Id {request.Id} returned");
            return new GetEventByIdResponse() { Successful = true, Message = $"Event with Id {request.Id} found" };

        }

        public async Task<UpdateEventResponse> UpdateEvent(int Id, UpdateEventRequest request)
        {
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Event:UpdateEvent"]}{Id}", request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                return new UpdateEventResponse() { Successful = false, Message = $"Error updating event | {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var updatedEvent = JsonConvert.DeserializeObject<UpdateEventResponse>(apiResponse);

            return updatedEvent;
        }

        public async Task<CancelEventResponse> Cancel(int Id)
        {
            var cancelId = Id;
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Event:Cancel"]}{Id}", cancelId);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(EventApiClient)}|(CancelEvent)API response not sucessful.", response);
                return new CancelEventResponse() { Successful = false, Message = $"Error deleting event | {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var cancelledEvent = JsonConvert.DeserializeObject<CancelEventResponse>(apiResponse);

            return cancelledEvent;
        }

        public async Task<CancelEventResponse> CancelEvent(int Id, CancelEventRequest request)
        {
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Event:CancelEvent"]}{Id}", request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(EventApiClient)}|(CancelEvent)API response not sucessful.", response);
                return new CancelEventResponse() { Successful = false, Message = $"Error deleting event | {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var cancelledEvent = JsonConvert.DeserializeObject<CancelEventResponse>(apiResponse);

            return cancelledEvent;
        }


        
    }
}
