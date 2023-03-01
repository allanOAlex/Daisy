using AutoMapper;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IServices;
using Daisy.Domain.Exceptions.ModelExceptions;
using Daisy.Domain.Models;
using Daisy.Infrastructure.Implementations.Interfaces;
using Daisy.Shared.Dtos;
using Daisy.Shared.Requests.Appointments;
using Daisy.Shared.Requests.Event;
using Daisy.Shared.Responses.Appointments;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Daisy.Infrastructure.Implementations.Services
{
    internal sealed class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;

        public AppointmentService(IUnitOfWork UnitOfWork, IMapper Mapper, UserManager<AppUser> UserManager)
        {
            unitOfWork = UnitOfWork;
            mapper = Mapper;
            userManager = UserManager;  
        }

        public async Task<CancelAppointmentResponse> Cancel(GetAppointmentByIdResponse getAppointmentByIdResponse)
        {
            try
            {
                getAppointmentByIdResponse.IsCancelled = true;
                getAppointmentByIdResponse.CancelledOn = DateTime.Now;
                getAppointmentByIdResponse.CancelledBy = 1;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<GetAppointmentByIdResponse, Appointment>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Appointment, CancelAppointmentResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<GetAppointmentByIdResponse, Appointment>(getAppointmentByIdResponse);
                Appointment itemToCancel = unitOfWork.Appointments.Update(destination);
                var result = responseMap.Map<Appointment, CancelAppointmentResponse>(itemToCancel);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new CancelAppointmentResponse { Successful = true, Message = "Appointment cancelled successfully!" } : new CancelAppointmentResponse { Successful = false, Message = "Error|Cancelling appointment failed" };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CancelAppointmentResponse> CancelAppointment(CancelAppointmentRequest cancelAppointmentRequest)
        {
            try
            {
                cancelAppointmentRequest.IsCancelled = true;
                cancelAppointmentRequest.CancelledOn = DateTime.Now;
                cancelAppointmentRequest.CancelledBy = 1;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<CancelAppointmentRequest, Appointment>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Appointment, CancelAppointmentResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<CancelAppointmentRequest, Appointment>(cancelAppointmentRequest);
                Appointment itemToCancel = unitOfWork.Appointments.Update(destination);
                var result = responseMap.Map<Appointment, CancelAppointmentResponse>(itemToCancel);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new CancelAppointmentResponse { Successful = true, Message = "Appointment cancelled successfully!" } : new CancelAppointmentResponse { Successful = false, Message = "Error|Cancelling appointment failed" };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CompleteAppointmentResponse> Complete(GetAppointmentByIdResponse getAppointmentByIdResponse)
        {
            try
            {
                getAppointmentByIdResponse.IsComplete = true;
                getAppointmentByIdResponse.CompletedOn = DateTime.Now;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<GetAppointmentByIdResponse, Appointment>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Appointment, CompleteAppointmentResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<GetAppointmentByIdResponse, Appointment>(getAppointmentByIdResponse);
                Appointment itemToComplete = unitOfWork.Appointments.Update(destination);
                var result = responseMap.Map<Appointment, CompleteAppointmentResponse>(itemToComplete);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new CompleteAppointmentResponse { Successful = true, Message = "Appointment completed successfully!" } : new CompleteAppointmentResponse { Successful = false, Message = "Error|Completing appointment failed" };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CompleteAppointmentResponse> CompleteAppointment(CompleteAppointmentRequest completeAppointmentRequest)
        {
            try
            {
                completeAppointmentRequest.IsComplete = true;
                completeAppointmentRequest.CompletedOn = DateTime.Now;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<CompleteAppointmentRequest, Appointment>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Appointment, CompleteAppointmentResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<CompleteAppointmentRequest, Appointment>(completeAppointmentRequest);
                Appointment itemToComplete = unitOfWork.Appointments.Update(destination);
                var result = responseMap.Map<Appointment, CompleteAppointmentResponse>(itemToComplete);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new CompleteAppointmentResponse { Successful = true, Message = "Appointment completed successfully!" } : new CompleteAppointmentResponse { Successful = false, Message = "Error|Completing appointment failed" };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<CreateAppointmentResponse> CreateAppointment(CreateAppointmentRequest createAppointmentRequest)
        {
            try
            {
                createAppointmentRequest.CreatedOn = DateTime.Now;
                createAppointmentRequest.CreatedBy = 1;

                IEnumerable<Appointment> existingEvent = unitOfWork.Appointments.FindAll().Where(row =>
                EF.Functions.Like(row.Salutation.ToString(), createAppointmentRequest.Salutation.ToString()) &&
                EF.Functions.Like(row.FirstName, createAppointmentRequest.FirstName) &&
                EF.Functions.Like(row.MiddleName, createAppointmentRequest.MiddleName) &&
                EF.Functions.Like(row.LastName, createAppointmentRequest.LastName) &&
                EF.Functions.Like(row.Date.ToString(), createAppointmentRequest.Date.ToString()) &&
                EF.Functions.Like(row.Time.ToString(), createAppointmentRequest.Time.ToString()) &&
                EF.Functions.Like(row.ContactNumber, createAppointmentRequest.ContactNumber)
                );

                if (existingEvent.Any())
                    throw new CreatingDuplicateException("Duplicate Appointment");


                var request = new MapperConfiguration(cfg => cfg.CreateMap<CreateAppointmentRequest, Appointment>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Appointment, CreateAppointmentResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<CreateAppointmentRequest, Appointment>(createAppointmentRequest);
                Appointment itemToCreate = unitOfWork.Appointments.Create(destination);
                var result = responseMap.Map<Appointment, CreateAppointmentResponse>(itemToCreate);

                await unitOfWork.CompleteAsync();
                result.Successful = true;

                return result.Successful == true ? new CreateAppointmentResponse { Successful = true, Message = "Appointment created successfully!" } : new CreateAppointmentResponse { Successful = false, Message = "Error|Creating appointment failed" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetAllAppointmentsResponse>> GetAllAppointments()
        {
            try
            {
                List<GetAllAppointmentsResponse> appointmentsList = new();

                var appointments = unitOfWork.Appointments.FindAll();
                if (appointments.Any())
                {
                    
                    foreach (var appointment in appointments)
                    {
                        var listItem = new GetAllAppointmentsResponse
                        {
                            Id = appointment.Id,
                            Salutation = appointment.Salutation,
                            FirstName = appointment.FirstName,
                            MiddleName = appointment.MiddleName,
                            LastName = appointment.LastName,
                            ContactNumber = appointment.ContactNumber,
                            Email = appointment.Email,
                            Date = appointment.Date,
                            Time = appointment.Time,
                            PreferedLocation = appointment.PreferedLocation,
                            Remarks = appointment.Remarks,
                            CreatedOn = appointment.CreatedOn,
                            CreatedBy = appointment.CreatedBy,
                            UpdatedOn = appointment.UpdatedOn,
                            UpdatedBy = appointment.UpdatedBy,
                            IsComplete = appointment.IsComplete,
                            CompletedOn = appointment.CompletedOn,
                            CompletedBy = appointment.CompletedBy,
                            CompletedRemarks = appointment.CompletedRemarks,
                            IsCancelled = appointment.IsCancelled,
                            CancelledOn = appointment.CancelledOn,
                            CancelledBy = appointment.CancelledBy,
                        };

                        appointmentsList.Add(listItem);
                    }

                    return appointmentsList;
                }

                return appointmentsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<GetAppointmentsByDateRangeResponse> GetByDateRange(GetAppointmentsByDateRangeRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAppointmentByIdResponse> GetById(int Id)
        {
            try
            {
                var appointmentToFind = unitOfWork.Appointments.FindByCondition(e => e.Id == Id);
                if (appointmentToFind.Any())
                {
                    var response = from e in appointmentToFind
                                   select new GetAppointmentByIdResponse
                                   {
                                       Id = e.Id,
                                       FirstName = e.FirstName,
                                       LastName = e.LastName,
                                       ContactNumber = e.ContactNumber,
                                       Email = e.Email,
                                       Date = e.Date,
                                       Time = e.Time,
                                       Remarks = e.Remarks
                                   };

                    var appointment = response.FirstOrDefault();
                    appointment.Successful = true;
                    return appointment; 
                }

                return new GetAppointmentByIdResponse() { Successful = false, Message = $"Appointment with Id {Id} not found." };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<GetAppointmentsForDateResponse> GetForDate(GetAppointmentsForDateRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateAppointmentResponse> Update(GetAppointmentByIdResponse getAppointmentByIdResponse)
        {
            try
            {
                getAppointmentByIdResponse.UpdatedOn = DateTime.Now;
                getAppointmentByIdResponse.UpdatedBy = 1;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<GetAppointmentByIdResponse, Appointment>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Appointment, UpdateAppointmentResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<GetAppointmentByIdResponse, Appointment>(getAppointmentByIdResponse);
                Appointment itemToUpdate = unitOfWork.Appointments.Update(destination);
                var result = responseMap.Map<Appointment, UpdateAppointmentResponse>(itemToUpdate);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new UpdateAppointmentResponse() { Successful = true, Message = "Appointment details updated successfully!" } : new UpdateAppointmentResponse() { Successful = false, Message = "Error updating appointment" };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UpdateAppointmentResponse> UpdateAppointment(UpdateAppointmentRequest updateAppointmentRequest)
        {
            try
            {
                updateAppointmentRequest.UpdatedOn = DateTime.Now;
                updateAppointmentRequest.UpdatedBy = 1;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<UpdateAppointmentRequest, Appointment>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Appointment, UpdateAppointmentResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<UpdateAppointmentRequest, Appointment>(updateAppointmentRequest);
                Appointment itemToUpdate = unitOfWork.Appointments.Update(destination);
                var result = responseMap.Map<Appointment, UpdateAppointmentResponse>(itemToUpdate);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful= true;
                    return result.Successful == true ? new UpdateAppointmentResponse() { Successful = true, Message = "Appointment details updated successfully!" } : new UpdateAppointmentResponse() { Successful = false, Message = "Error updating appointment" };
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
