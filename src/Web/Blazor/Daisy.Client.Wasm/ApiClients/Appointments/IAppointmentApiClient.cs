using Daisy.Shared.Requests.Appointments;
using Daisy.Shared.Requests.Event;
using Daisy.Shared.Responses.Appointments;
using Daisy.Shared.Responses.Event;

namespace Daisy.Client.Wasm.ApiClients.Appointments
{
    public interface IAppointmentApiClient
    {
        Task<CreateAppointmentResponse> CreateAppointment(CreateAppointmentRequest request);
        Task<List<GetAllAppointmentsResponse>> GetAllAppointments();
        Task<GetAppointmentByIdResponse> GetById(GetAppointmentByIdRequest request);
        Task<GetAppointmentsForDateResponse> GetForDate(GetAppointmentsForDateRequest request);
        Task<GetAppointmentsByDateRangeResponse> GetByDateRange(GetAppointmentsByDateRangeRequest request);
        Task<UpdateAppointmentResponse> Update(int Id);
        Task<UpdateAppointmentResponse> UpdateAppointment(int Id, UpdateAppointmentRequest request);
        Task<CancelAppointmentResponse> Cancel(int Id);
        Task<CancelAppointmentResponse> CancelAppointment(int Id, CancelAppointmentRequest request);
        Task<CompleteAppointmentResponse> Complete(int Id, int UserId);
        Task<CompleteAppointmentResponse> CompleteAppointment(CompleteAppointmentRequest request);
    }
}
