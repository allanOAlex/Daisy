using AutoMapper;
using Daisy.Application.Abstractions.Interfaces;
using Daisy.Application.Abstractions.IServices;
using Daisy.Domain.Exceptions.ModelExceptions;
using Daisy.Domain.Models;
using Daisy.Shared.Requests.Carousels;
using Daisy.Shared.Requests.Event;
using Daisy.Shared.Responses.Carousels;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Infrastructure.Implementations.Services
{
    public class CarouselService : ICarouselService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CarouselService(IUnitOfWork UnitOfWork, IMapper Mapper)
        {
            unitOfWork = UnitOfWork;
            mapper = Mapper;
        }

        public async Task<CreateCarouselResponse> CreateCarousel(CreateCarouselRequest createCarouselRequest)
        {
            try
            {
                IEnumerable<Carousel> existingItem = unitOfWork.Carousels.FindAll().Where(row =>
                    EF.Functions.Like(row.ImageData.ToString(), createCarouselRequest.ImageData.ToString()) &&
                    EF.Functions.Like(row.Label, createCarouselRequest.Label) &&
                    EF.Functions.Like(row.Paragraph, createCarouselRequest.Paragraph)
                    
                );

                if (existingItem.Any())
                    throw new CreatingDuplicateException("Duplicate Carousel");


                var request = new MapperConfiguration(cfg => cfg.CreateMap<CreateCarouselRequest, Carousel>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Carousel, CreateCarouselResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<CreateCarouselRequest, Carousel>(createCarouselRequest);
                Carousel itemToCreate = unitOfWork.Carousels.Create(destination);
                var result = responseMap.Map<Carousel, CreateCarouselResponse>(itemToCreate);

                await unitOfWork.CompleteAsync();
                result.Successful = true;

                return result.Successful == true ? new CreateCarouselResponse { Successful = true, Message = "Carousel created successfully!" } : new CreateCarouselResponse { Successful = false, Message = "Error|Creating carousel failed" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DeleteCarouselResponse> Delete(GetCarouselByIdResponse deleteCarouselRequest)
        {
            try
            {
                deleteCarouselRequest.IsDeleted = true;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<GetCarouselByIdResponse, Carousel>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Carousel, DeleteCarouselResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<GetCarouselByIdResponse, Carousel>(deleteCarouselRequest);
                Carousel itemToDelete = unitOfWork.Carousels.Delete(destination);
                var result = responseMap.Map<Carousel, DeleteCarouselResponse>(itemToDelete);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new DeleteCarouselResponse() { Successful = true, Message = "Carousel deleted successfully!" } : new DeleteCarouselResponse() { Successful = false, Message = "Error deleting carousel" };
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

        public async Task<DeleteCarouselResponse> DeleteCarousel(DeleteCarouselRequest deleteCarouselRequest)
        {
            try
            {
                deleteCarouselRequest.IsDeleted = true;

                var request = new MapperConfiguration(cfg => cfg.CreateMap<DeleteCarouselRequest, Carousel>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Carousel, DeleteCarouselResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<DeleteCarouselRequest, Carousel>(deleteCarouselRequest);
                Carousel itemToDelete = unitOfWork.Carousels.Delete(destination);
                var result = responseMap.Map<Carousel, DeleteCarouselResponse>(itemToDelete);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new DeleteCarouselResponse() { Successful = true, Message = "Carousel deleted successfully!" } : new DeleteCarouselResponse() { Successful = false, Message = "Error deleting carousel" };
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

        public async Task<List<GetAllCarouselsResponse>> GetAllCarousels()
        {
            try
            {
                List<GetAllCarouselsResponse> carouselList = new();
                var carousels = unitOfWork.Carousels.FindAll();
                if (carousels.Any())
                {
                    foreach (var carousel in carousels)
                    {
                        var listItem = new GetAllCarouselsResponse
                        {
                            Id = carousel.Id,
                            Image = carousel.ImageType,
                            ImageType = carousel.ImageType,
                            ImageData= carousel.ImageData,
                            ImageSource = string.Format($"data:{carousel.ImageType};base64,{0}", carousel.Image),
                            Label = carousel.Label,
                            Paragraph = carousel.Paragraph,
                        };

                        carouselList.Add(listItem);
                    }

                    return carouselList;
                }

                return carouselList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<GetCarouselByIdResponse> GetById(int Id)
        {
            try
            {
                var eventToFind = unitOfWork.Carousels.FindByCondition(e => e.Id == Id);
                if (eventToFind.Any())
                {
                    var response = from e in eventToFind
                                   select new GetCarouselByIdResponse
                                   {
                                       Id = e.Id,
                                       Image = e.Image,
                                       ImageType = e.ImageType,
                                       ImageData = e.ImageData,
                                       ImageSource = string.Format("data:image/jpeg;base64,{0}", e.Image),
                                       Label = e.Label,
                                       Paragraph = e.Paragraph,
                                   };

                    var carousel = response.FirstOrDefault();
                    carousel.Successful = true; 
                    return carousel;
                }

                return new GetCarouselByIdResponse() { Successful = false, Message = $"Carousel with Id {Id} not found." };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UpdateCarouselResponse> UpdateCarousel(UpdateCarouselRequest updateCarouselRequest)
        {
            try
            {
                var request = new MapperConfiguration(cfg => cfg.CreateMap<UpdateCarouselRequest, Carousel>());
                var response = new MapperConfiguration(cfg => cfg.CreateMap<Carousel, UpdateCarouselResponse>());

                IMapper requestMap = request.CreateMapper();
                IMapper responseMap = response.CreateMapper();

                var destination = requestMap.Map<UpdateCarouselRequest, Carousel>(updateCarouselRequest);
                Carousel itemToUpdate = unitOfWork.Carousels.Update(destination);
                var result = responseMap.Map<Carousel, UpdateCarouselResponse>(itemToUpdate);

                try
                {
                    await unitOfWork.CompleteAsync();
                    result.Successful = true;
                    return result.Successful == true ? new UpdateCarouselResponse() { Successful = true, Message = "Carousel details updated successfully!" } : new UpdateCarouselResponse() { Successful = false, Message = "Error updating carousel" };
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
