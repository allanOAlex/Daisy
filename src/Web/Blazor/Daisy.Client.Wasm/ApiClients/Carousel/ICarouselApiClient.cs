using Daisy.Shared.Requests.Appointments;
using Daisy.Shared.Requests.Carousels;
using Daisy.Shared.Responses.Appointments;
using Daisy.Shared.Responses.Carousels;

namespace Daisy.Client.Wasm.ApiClients.Carousel
{
    public interface ICarouselApiClient
    {
        Task<CreateCarouselResponse> CreateCarousel(CreateCarouselRequest request);
        Task<List<GetAllCarouselsResponse>> GetAllCarousels();
        Task<GetCarouselByIdResponse> GetById(GetCarouselByIdRequest request);
        Task<UpdateCarouselResponse> UpdateCarousel(int Id, UpdateCarouselRequest request);
        Task<DeleteCarouselResponse> Delete(int Id);
        Task<DeleteCarouselResponse> DeleteCarousel(int Id, DeleteCarouselRequest request);
    }
}
