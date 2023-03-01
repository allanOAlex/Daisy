using Daisy.Shared.Requests.Carousels;
using Daisy.Shared.Requests.Event;
using Daisy.Shared.Responses.Carousels;
using Daisy.Shared.Responses.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Application.Abstractions.IServices
{
    public interface ICarouselService
    {
        Task<List<GetAllCarouselsResponse>> GetAllCarousels();
        Task<GetCarouselByIdResponse> GetById(int Id);
        Task<CreateCarouselResponse> CreateCarousel(CreateCarouselRequest request);
        Task<UpdateCarouselResponse> UpdateCarousel(UpdateCarouselRequest request);
        Task<DeleteCarouselResponse> Delete(GetCarouselByIdResponse request);
        Task<DeleteCarouselResponse> DeleteCarousel(DeleteCarouselRequest request);
    }
}
