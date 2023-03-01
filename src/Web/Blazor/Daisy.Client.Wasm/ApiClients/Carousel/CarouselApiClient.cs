using Azure.Core;
using Daisy.Client.Wasm.ApiClients.User;
using Daisy.Shared.Requests.Carousels;
using Daisy.Shared.Responses.Carousels;
using Daisy.Shared.Responses.Event;
using Daisy.Shared.Responses.User;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Daisy.Client.Wasm.ApiClients.Carousel
{
    public class CarouselApiClient : ICarouselApiClient
    {
        private readonly HttpClient client;
        private readonly IConfiguration config;
        private readonly ILogger<UserApiClient> logger;
        private string responseMessage;

        public CarouselApiClient(HttpClient Client, IConfiguration Config, ILogger<UserApiClient> Logger)
        {
            client = Client;
            config = Config;
            logger = Logger;
        }


        public async Task<CreateCarouselResponse> CreateCarousel(CreateCarouselRequest request)
        {
            var response = await client.PostAsJsonAsync(config["Api:Routes:Carousel:CreateCarousel"], request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError($"{nameof(CarouselApiClient)}|(CreateCarousel)API response not sucessful.", response);
                return new CreateCarouselResponse() { Successful = false, Message = $"Error Creating Carousel | {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var createdItem = JsonConvert.DeserializeObject<CreateCarouselResponse>(apiResponse);

            return createdItem;
        }

        public async Task<DeleteCarouselResponse> Delete(int Id)
        {
            var deleteId = Id;
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Carousel:Delete"]}{Id}", deleteId);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                responseMessage = $"Error Deleting Carousel | {response.StatusCode} | {response.ReasonPhrase}";
                logger.LogError($"{nameof(CarouselApiClient)}|(DeleteCarousel)API response not sucessful.", response);
                return new DeleteCarouselResponse() { Successful = false, Message = $"Error deleting carousel | {responseMessage}" };

            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var deletedItem = JsonConvert.DeserializeObject<DeleteCarouselResponse>(apiResponse);

            return deletedItem;
        }

        public async Task<DeleteCarouselResponse> DeleteCarousel(int Id, DeleteCarouselRequest request)
        {
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Carousel:DeleteCarousel"]}{request.Id}", request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                responseMessage = $"Error Deleting Carousel | {response.StatusCode} | {response.ReasonPhrase}";
                logger.LogError($"{nameof(CarouselApiClient)}|(DeleteCarousel)API response not sucessful.", response);
                return new DeleteCarouselResponse() { Successful = false, Message = $"Error deleting carousel | {responseMessage}" };

            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var deletedItem = JsonConvert.DeserializeObject<DeleteCarouselResponse>(apiResponse);

            return deletedItem;
        }

        public async Task<List<GetAllCarouselsResponse>> GetAllCarousels()
        {
            var response = await client.GetFromJsonAsync<List<GetAllCarouselsResponse>>(config["Api:Routes:Carousel:GetAllCarousels"]);
            if (response.Count <= 0)
            {
                logger.LogError($"{nameof(CarouselApiClient)}|(GetAllCarousels)|Error while fetching Carousels");
                return new List<GetAllCarouselsResponse>();
            }

            return response;
        }

        public async Task<GetCarouselByIdResponse> GetById(GetCarouselByIdRequest request)
        {
            var response = await client.GetFromJsonAsync<GetCarouselByIdResponse>($"{config["Api:Routes:Carousel:GetById"]}{request.Id}");
            if (!response.Successful)
            {
                logger.LogError($"{nameof(CarouselApiClient)}|(GetById)|Error while fetching carousel with Id {request.Id}");
                return new GetCarouselByIdResponse() { Successful = false, Message = $"Error fetching carousel" };
            }

            logger.LogError($"{nameof(UserApiClient)}|(GetById)|Carousel with Id {request.Id} returned");
            //return new GetCarouselByIdResponse() { Successful = true, Message = $"Carousel with Id {request.Id} found" };
            return response;
        }

        public async Task<UpdateCarouselResponse> UpdateCarousel(int Id, UpdateCarouselRequest request)
        {
            var response = await client.PutAsJsonAsync($"{config["Api:Routes:Carousel:UpdateCarousel"]}{Id}", request);
            responseMessage = response.ReasonPhrase;
            if (!response.IsSuccessStatusCode)
            {
                return new UpdateCarouselResponse() { Successful = false, Message = $"Error updating carousel | {responseMessage}" };
            }

            var apiResponse = await response.Content.ReadAsStringAsync();
            var updatedItem = JsonConvert.DeserializeObject<UpdateCarouselResponse>(apiResponse);

            return updatedItem;
        }


    }
}
