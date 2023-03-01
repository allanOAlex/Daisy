using Daisy.Shared.Requests.Appointments;
using Daisy.Shared.Requests.Event;
using Daisy.Shared.Responses.Appointments;
using Daisy.Shared.Responses.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IServices
{
    public interface IAppointmentService
    {
        Task<CreateAppointmentResponse> CreateAppointment(CreateAppointmentRequest request);
        Task<List<GetAllAppointmentsResponse>> GetAllAppointments();
        Task<GetAppointmentByIdResponse> GetById(int Id);
        Task<GetAppointmentsForDateResponse> GetForDate(GetAppointmentsForDateRequest request);
        Task<GetAppointmentsByDateRangeResponse> GetByDateRange(GetAppointmentsByDateRangeRequest request);
        Task<UpdateAppointmentResponse> Update(GetAppointmentByIdResponse request);
        Task<UpdateAppointmentResponse> UpdateAppointment(UpdateAppointmentRequest request);
        Task<CancelAppointmentResponse> Cancel(GetAppointmentByIdResponse request);
        Task<CancelAppointmentResponse> CancelAppointment(CancelAppointmentRequest request);
        Task<CompleteAppointmentResponse> Complete(GetAppointmentByIdResponse request);
        Task<CompleteAppointmentResponse> CompleteAppointment(CompleteAppointmentRequest request);
    }
}
