using Azure;
using Daisy.Client.Wasm.ApiClients.User;
using Daisy.Shared.Requests.Appointments;
using Daisy.Shared.Responses.Appointments;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;

namespace Daisy.Client.Wasm.ApiClients.Appointments
{
    public class AppointmentApiClient : IAppointmentApiClient
    {
        private readonly HttpClient client;
        private readonly IConfiguration config;
        private string responseMessage;
        private readonly ILogger<AppointmentApiClient> logger;

        public AppointmentApiClient(HttpClient Client, IConfiguration Config, ILogger<AppointmentApiClient> Logger)
        {
            client = Client;
            config = Config;
            logger= Logger;
        }

        public async Task<CancelAppointmentResponse> Cancel(int Id)
        {
            var cancelId = Id;
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Appointment:Cancel"]}{Id}", cancelId);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(AppointmentApiClient)}|(CancelAppointment)API response not sucessful.", response);
                return new CancelAppointmentResponse() { Successful = false, Message = $"Error deleting appointment| {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var cancelledAppointment = JsonConvert.DeserializeObject<CancelAppointmentResponse>(apiResponse);

            return cancelledAppointment;
        }

        public async Task<CancelAppointmentResponse> CancelAppointment(int Id, CancelAppointmentRequest request)
        {
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Appointment:CancelAppointment"]}{Id}", request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(AppointmentApiClient)}|(CancelAppointment)API response not sucessful.", response);
                return new CancelAppointmentResponse() { Successful = false, Message = $"Error deleting user| {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var cancelledAppointment = JsonConvert.DeserializeObject<CancelAppointmentResponse>(apiResponse);

            return cancelledAppointment;
        }
        public async Task<CompleteAppointmentResponse> Complete(int Id, int UserId)
        {
            var completeId = Id;
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Appointment:Complete"]}{Id}/{UserId}", completeId);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(AppointmentApiClient)}|(Complete)API response not sucessful.", response);
                return new CompleteAppointmentResponse() { Successful = false, Message = $"Error Completing appointment| {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var completedAppointment = JsonConvert.DeserializeObject<CompleteAppointmentResponse>(apiResponse);

            return completedAppointment;
        }

        public async Task<CompleteAppointmentResponse> CompleteAppointment(CompleteAppointmentRequest request)
        {
            var completeId = request.Id;
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Appointment:CompleteAppointment"]}{request.Id}", request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(AppointmentApiClient)}|(Complete)API response not sucessful.", response);
                return new CompleteAppointmentResponse() { Successful = false, Message = $"Error Completing appointment| {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var completedAppointment = JsonConvert.DeserializeObject<CompleteAppointmentResponse>(apiResponse);

            return completedAppointment;
        }

        public async Task<CreateAppointmentResponse> CreateAppointment(CreateAppointmentRequest request)
        {
            var response = await client.PostAsJsonAsync(config["Api:Routes:Appointment:CreateAppointment"], request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(AppointmentApiClient)}|(CreateAppointment)API response not sucessful.", response);
                return new CreateAppointmentResponse() { Successful = false, Message = $"Error Creating Appointment | {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var createdAppointment = JsonConvert.DeserializeObject<CreateAppointmentResponse>(apiResponse);

            return createdAppointment;
        }

        public async Task<List<GetAllAppointmentsResponse>> GetAllAppointments()
        {
            var response = await client.GetFromJsonAsync<List<GetAllAppointmentsResponse>>(config["Api:Routes:Appointment:GetAllAppointments"]);
            if (response.Count <= 0)
            {
                logger.LogError($"{nameof(AppointmentApiClient)}|(GetAllAppointments)|No records found");
                return new List<GetAllAppointmentsResponse>(); 
            }

            return response;
        }

        public async Task<GetAppointmentByIdResponse> GetById(GetAppointmentByIdRequest request)
        {
            var response = await client.GetFromJsonAsync<GetAppointmentByIdResponse>($"{config["Api:Routes:Appointment:GetById"]}{request.Id}");

            if (!response.Successful)
            {
                logger.LogError($"{nameof(AppointmentApiClient)}|(GetById)|Error while fetching appointment with Id {request.Id}");
                return new GetAppointmentByIdResponse() { Successful = false, Message = $"Error fetching appointment" };
            }

            logger.LogError($"{nameof(AppointmentApiClient)}|(GetById)|Appointment with Id {request.Id} returned");
            return new GetAppointmentByIdResponse() { Successful = true, Message = $"Appointment with Id {request.Id} found" };
        }

        public async Task<GetAppointmentsForDateResponse> GetForDate(GetAppointmentsForDateRequest request)// To Be Fixed
        {
            return await client.GetFromJsonAsync<GetAppointmentsForDateResponse>(config["Api:Routes:Appointment:GetForDate"]);
        }

        public async Task<GetAppointmentsByDateRangeResponse> GetByDateRange(GetAppointmentsByDateRangeRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateAppointmentResponse> Update(int Id)
        {
            var updateId = Id;
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Appointment:Update"]}{Id}", updateId);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                return new UpdateAppointmentResponse() { Successful = false, Message = $"Error updating user | {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var updatedAppointment = JsonConvert.DeserializeObject<UpdateAppointmentResponse>(apiResponse);

            return updatedAppointment;
        }

        public async Task<UpdateAppointmentResponse> UpdateAppointment(int Id, UpdateAppointmentRequest request)
        {
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Appointment:UpdateAppointment"]}{Id}", request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                return new UpdateAppointmentResponse() { Successful = false, Message = $"Error updating user | {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var updatedAppointment = JsonConvert.DeserializeObject<UpdateAppointmentResponse>(apiResponse);

            return updatedAppointment;
        }


    }
}
