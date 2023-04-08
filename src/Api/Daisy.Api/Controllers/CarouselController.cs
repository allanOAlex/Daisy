using Daisy.Application.Abstractions.Interfaces;
using Daisy.Shared.Enums;
using Daisy.Shared.Helpers.Authorization;
using Daisy.Shared.Requests.Carousels;
using Daisy.Shared.Requests.Event;
using Daisy.Shared.Responses.Carousels;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Daisy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CarouselController : ControllerBase
    {
        private readonly IServiceManager serviceManager;
        public CarouselController(IServiceManager ServiceManager)
        {
            serviceManager = ServiceManager;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateCarouselResponse>> CreateCarousel(CreateCarouselRequest request)
        {
            var createdCarousel = await serviceManager.CarouselService.CreateCarousel(request);
            return createdCarousel.Successful == true ? StatusCode(StatusCodes.Status201Created, new CreateCarouselResponse { Successful = true, Message = "Carousel created successfully!" }) : StatusCode(StatusCodes.Status500InternalServerError, new CreateCarouselResponse { Successful = createdCarousel.Successful, Message = $"{createdCarousel.Message}" });

        }

        [AllowAnonymous]
        [HttpGet("fetchall")]
        public async Task<ActionResult<List<GetAllCarouselsResponse>>> GetAllCarousels()
        {
            var carousels = await serviceManager.CarouselService.GetAllCarousels();
            if (carousels != null)
                return Ok(carousels);

            return carousels;

        }

        [HttpGet("getbyId/{Id}")]
        public async Task<ActionResult<GetCarouselByIdResponse>> GetById(int Id)
        {
            var carouselToFind = await serviceManager.CarouselService.GetById(Id);
            if (carouselToFind == null)
                return new GetCarouselByIdResponse() { Successful = false, Message = $"Carousel with Id : {Id} not found" };

            carouselToFind.Successful= true;
            return Ok(carouselToFind);

        }

        [HttpPut("update/{Id}")]
        public async Task<ActionResult<UpdateCarouselResponse>> UpdateCarousel(int Id, UpdateCarouselRequest request)
        {
            if (Id != request.Id)
                return new UpdateCarouselResponse() { Successful = false, Message = $"Carousel ID mismatch" };

            var carouselToUpdate = await serviceManager.CarouselService.GetById(Id);

            if (carouselToUpdate == null)
                return new UpdateCarouselResponse() { Successful = false, Message = $"Carousel with Id : {Id} not found" };

            var updatedCarousel = await serviceManager.CarouselService.UpdateCarousel(request);
            return Ok(updatedCarousel);

        }

        [AuthorizeRoles(UserRole.SuperAdmin)]
        [HttpPut("delete/{Id}")]
        public async Task<ActionResult<DeleteCarouselResponse>> Delete(int Id)
        {
            var carouselToDelete = await serviceManager.CarouselService.GetById(Id);
            if (carouselToDelete == null)
                return new DeleteCarouselResponse() { Successful = false, Message = $"Carousel with Id : {Id} not found" };

            var deletedCarousel = await serviceManager.CarouselService.Delete(carouselToDelete);
            return Ok(deletedCarousel);
        }

        [AuthorizeRoles(UserRole.SuperAdmin)]
        [HttpPut("deletecarousel/{Id}")]
        public async Task<ActionResult<DeleteCarouselResponse>> DeleteCarousel(int Id, DeleteCarouselRequest request)
        {
            if (Id != request.Id)
                return new DeleteCarouselResponse() { Successful = false, Message = $"Carousel ID mismatch" };

            var carouselToDelete = await serviceManager.CarouselService.GetById(Id);

            if (carouselToDelete == null)
                return new DeleteCarouselResponse() { Successful = false, Message = $"Carousel with Id : {Id} not found" };

            var deletedCarousel = await serviceManager.CarouselService.DeleteCarousel(request);
            return Ok(deletedCarousel);
        }


    }
}
