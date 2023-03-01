using Azure.Core;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Shared.Enums;
using Daisy.Shared.Helpers.Authorization;
using Daisy.Shared.Requests.Appointments;
using Daisy.Shared.Responses.Appointments;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Daisy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AppointmentController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public AppointmentController(IServiceManager ServiceManager)
        {
            serviceManager = ServiceManager;
        }

        [AllowAnonymous] 
        [HttpPost("create")]
        public async Task<ActionResult<CreateAppointmentResponse>> CreateAppointment(CreateAppointmentRequest request)
        {
            var createdAppointment = await serviceManager.AppointmentService.CreateAppointment(request);
            return createdAppointment.Successful == true ? StatusCode(StatusCodes.Status201Created, new CreateAppointmentResponse { Successful = true, Message = "Appointment created successfully!" }) : StatusCode(StatusCodes.Status500InternalServerError, new CreateAppointmentResponse { Successful = createdAppointment.Successful, Message = $"{createdAppointment.Message}" });

        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("fetchall")]
        public async Task<ActionResult<List<GetAllAppointmentsResponse>>> GetAllAppointments()
        {
            var appointments = await serviceManager.AppointmentService.GetAllAppointments();
            if (appointments.Count > 0)
                return Ok(appointments);

            return appointments;

        }

        [HttpGet("getbyId/{Id}")]
        public async Task<ActionResult<GetAppointmentByIdResponse>> GetById(int Id)
        {
            var appointmentToFind = await serviceManager.AppointmentService.GetById(Id);
            if (appointmentToFind == null)
                return new GetAppointmentByIdResponse() { Successful = false, Message = $"Appointment with Id : {Id} not found" };

            return Ok(appointmentToFind);

        }

        [HttpGet("getfordate")]
        public async Task<ActionResult<GetAppointmentsForDateResponse>> GetForDate(GetAppointmentsForDateRequest request)
        {
            var appointments = await serviceManager.AppointmentService.GetForDate(request);
            if (appointments == null)
                return NotFound($"No Appointment(s) for date : {request.Date.ToString("d")} was found");

            return Ok(appointments);

        }

        [HttpGet("getbydaterange")]
        public async Task<ActionResult<GetAppointmentsByDateRangeResponse>> GetByDateRange(GetAppointmentsByDateRangeRequest request)
        {
            var appointments = await serviceManager.AppointmentService.GetByDateRange(request);
            if (appointments == null)
                return NotFound($"No Appointment(s) within the specified dates was found");

            return Ok(appointments);

        }

        [AuthorizeRoles(UserRole.SuperAdmin)]
        [HttpPut("update/{Id}")]
        public async Task<ActionResult<UpdateAppointmentResponse>> Update(int Id)
        {
            var itemToUpdate = await serviceManager.AppointmentService.GetById(Id);
            if (!itemToUpdate.Successful)
                return new UpdateAppointmentResponse() { Successful = false, Message = $"Appointment with Id : {Id} not found" };

            var updatedItem = await serviceManager.AppointmentService.Update(itemToUpdate);
            return Ok(updatedItem);

        }

        [AuthorizeRoles(UserRole.SuperAdmin)]
        [HttpPut("updateappointment/{Id}")]
        public async Task<ActionResult<UpdateAppointmentResponse>> UpdateAppointment(int Id, UpdateAppointmentRequest request)
        {
            if (Id != request.Id)
                return new UpdateAppointmentResponse() { Successful = false, Message = $"Appointment ID mismatch" };

            var itemToUpdate = await serviceManager.AppointmentService.GetById(Id);

            if (itemToUpdate == null)
                return new UpdateAppointmentResponse() { Successful = false, Message = $"Appointment with Id : {Id} not found" };

            var updatedItem = await serviceManager.AppointmentService.UpdateAppointment(request);
            return Ok(updatedItem);

        }

        [AuthorizeRoles(UserRole.SuperAdmin)]
        [HttpPut("complete/{Id}/{UserId}")]
        public async Task<ActionResult<CompleteAppointmentResponse>> Complete(int Id, int UserId)
        {
            try
            {
                var appointmentToComplete = await serviceManager.AppointmentService.GetById(Id);
                if (!appointmentToComplete.Successful)
                    return new CompleteAppointmentResponse() { Successful = false, Message = $"Appointment with Id : {Id} not found" };

                appointmentToComplete.CompletedBy = UserId;
                var completedAppointment = await serviceManager.AppointmentService.Complete(appointmentToComplete);
                return Ok(completedAppointment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error cancelling appointment | {ex.Message}");

            }
        }

        [AuthorizeRoles(UserRole.SuperAdmin)]
        [HttpPut("completeappointment/{request.Id}")]
        public async Task<ActionResult<CompleteAppointmentResponse>> CompleteAppointment(CompleteAppointmentRequest request)
        {
            try
            {
                var appointmentToComplete = await serviceManager.AppointmentService.GetById(request.Id);
                if (!appointmentToComplete.Successful)
                    return new CompleteAppointmentResponse() { Successful = false, Message = $"Appointment with Id : {request.Id} not found" };

                var completedAppointment = await serviceManager.AppointmentService.CompleteAppointment(request);
                return Ok(completedAppointment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error cancelling appointment | {ex.Message}");

            }
        }

        [AuthorizeRoles(UserRole.SuperAdmin)]
        [HttpPut("cancel/{Id}")]
        public async Task<ActionResult<CancelAppointmentResponse>> Cancel(int Id)
        {
            try
            {
                var appointmentToCancel = await serviceManager.AppointmentService.GetById(Id);
                if (!appointmentToCancel.Successful)
                    return new CancelAppointmentResponse() { Successful = false, Message = $"Appointment with Id : {Id} not found" };

                var cancelledAppointment = await serviceManager.AppointmentService.Cancel(appointmentToCancel);
                return Ok(cancelledAppointment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error cancelling appointment | {ex.Message}");

            }
        }

        [AuthorizeRoles(UserRole.SuperAdmin)]
        [HttpPut("cancelappointment/{Id}")]
        public async Task<ActionResult<CancelAppointmentResponse>> CancelAppointment(int Id, CancelAppointmentRequest request)
        {
            try
            {
                if (Id != request.Id)
                    return new CancelAppointmentResponse() { Successful = false, Message = $"Appointment ID mismatch" };

                var appointmentToCancel = await serviceManager.AppointmentService.GetById(Id);

                if (appointmentToCancel == null)
                    return new CancelAppointmentResponse() { Successful = false, Message = $"Appointment with Id : {Id} not found" };

                var cancelledAppointment = await serviceManager.AppointmentService.CancelAppointment(request);
                return Ok(cancelledAppointment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error cancelling appointment | {ex.Message}");

            }
        }


    }
}
