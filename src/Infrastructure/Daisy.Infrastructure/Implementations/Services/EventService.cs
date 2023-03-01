using AutoMapper;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IServices;
using Daisy.Domain.Exceptions.ModelExceptions;
using Daisy.Domain.Models;
using Daisy.Shared.Extensions;
using Daisy.Shared.Requests.Event;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using Microsoft.EntityFrameworkCore;

namespace Daisy.Infrastructure.Implementations.Services
{
    internal sealed class EventService : IEventService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public EventService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            unitOfWork = UnitOfWork;
            mapper = Mapper;
        }

        public async Task<CreateEventResponse> CreateEvent(CreateEventRequest createEventRequest)
        {
            try
            {
                ModelValidationsExtension.ValidateDates(createEventRequest.StartDate, createEventRequest.Time, createEventRequest.EndDate);

                createEventRequest.CreatedOn = DateTime.Now;
                createEventRequest.CreatedBy = 1;

                IEnumerable<Event> existingEvent = unitOfWork.Events.FindAll().Where(row =>
                EF.Functions.Like(row.Title, createEventRequest.Title) &&
                EF.Functions.Like(row.Description, createEventRequest.Description) &&
                EF.Functions.Like(row.StartDate.ToString(), createEventRequest.StartDate.ToString()) &&
                EF.Functions.Like(row.Time.ToString(), createEventRequest.Time.ToString()) &&
                EF.Functions.Like(row.Location, createEventRequest.Location) &&
                EF.Functions.Like(row.Venue, createEventRequest.Venue)
                );

                if (existingEvent.Any())
                    throw new CreatingDuplicateException("Duplicate Event");

                var request = new MapperConfiguration(cfg => cfg.CreateMap<CreateEventRequest, Event>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Event, CreateEventResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<CreateEventRequest, Event>(createEventRequest);
                Event eventToCreate = unitOfWork.Events.Create(destination);
                var result = responseMap.Map<Event, CreateEventResponse>(eventToCreate);

                await unitOfWork.CompleteAsync();
                result.Successful = true;

                return result.Successful == true ? new CreateEventResponse { Successful = true, Message = "Event created successfully!" } : new CreateEventResponse { Successful = false, Message = "Error|Creating event failed" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetAllEventsResponse>> GetAllEvents()
        {
            try
            {
                List<GetAllEventsResponse> eventsList = new();
                var events = unitOfWork.Events.FindAll();
                if (events.Any())
                {
                    foreach (var item in events)
                    {
                        var listItem = new GetAllEventsResponse
                        {
                            Id = item.Id,
                            ImageData= item.ImageData,  
                            ImageType= item.ImageType,  
                            Title = item.Title,
                            Description = item.Description,
                            StartDate = item.StartDate,
                            Time = item.Time,
                            EndDate = item.EndDate,
                            Location = item.Location,
                            Venue = item.Venue
                        };

                        eventsList.Add(listItem);    
                    }
                    
                    return eventsList;
                }

                return eventsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetEventByIdResponse> GetById(int Id)
        {
            try
            {
                var eventToFind = unitOfWork.Events.FindByCondition(e=> e.Id == Id);
                if (eventToFind.Any())
                {
                    var response = from e in eventToFind select new GetEventByIdResponse
                    {
                        Id = e.Id,
                        ImageData= e.ImageData,
                        ImageType= e.ImageType,
                        Title = e.Title,
                        Description = e.Description,
                        StartDate = e.StartDate,
                        Time = e.Time,
                        EndDate = e.EndDate,
                        Location = e.Location,
                        Venue = e.Venue 
                    };

                    var item = response.FirstOrDefault();
                    item.Successful = true;
                    return item;
                }

                return new GetEventByIdResponse() { Successful = false, Message = $"Event with Id {Id} not found." };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UpdateEventResponse> UpdateEvent(UpdateEventRequest updateEventRequest)
        {
            try
            {
                updateEventRequest.UpdatedOn = DateTime.Now;
                updateEventRequest.UpdatedBy = 1;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<UpdateEventRequest, Event>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Event, UpdateEventResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<UpdateEventRequest, Event>(updateEventRequest);
                Event eventToCreate = unitOfWork.Events.Update(destination);
                var result = responseMap.Map<Event, UpdateEventResponse>(eventToCreate);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new UpdateEventResponse() { Successful = true, Message = "Event details updated successfully!" } : new UpdateEventResponse() { Successful = false, Message = "Error updating event" };
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

        public async Task<CancelEventResponse> Cancel(GetEventByIdResponse cancelEventRequest)
        {
            try
            {
                cancelEventRequest.IsCancelled = true;
                cancelEventRequest.CancelledOn = DateTime.Now;
                cancelEventRequest.CancelledBy = 1;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<GetEventByIdResponse, Event>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Event, CancelEventResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<GetEventByIdResponse, Event>(cancelEventRequest);
                Event eventToCreate = unitOfWork.Events.Update(destination);
                var result = responseMap.Map<Event, CancelEventResponse>(eventToCreate);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new CancelEventResponse() { Successful = true, Message = "Event cancelled successfully!" } : new CancelEventResponse() { Successful = false, Message = "Error cancelling event" };
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

        public async Task<CancelEventResponse> CancelEvent(CancelEventRequest cancelEventRequest)
        {
            try
            {
                cancelEventRequest.IsCancelled = true;
                cancelEventRequest.CancelledOn = DateTime.Now;
                cancelEventRequest.CancelledBy = 1;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<CancelEventRequest, Event>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Event, CancelEventResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<CancelEventRequest, Event>(cancelEventRequest);
                Event eventToCreate = unitOfWork.Events.Update(destination);
                var result = responseMap.Map<Event, CancelEventResponse>(eventToCreate);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new CancelEventResponse() { Successful = true, Message = "Event cancelled successfully!" } : new CancelEventResponse() { Successful = false, Message = "Error cancelling event" };
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


    }
}
