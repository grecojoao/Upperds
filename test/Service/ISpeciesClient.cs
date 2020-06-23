using System.Threading.Tasks;
using Domain.Models;
using Refit;
using test.Domain.Models;

namespace test.Service
{
    public interface ISpeciesClient
    {
        [Get("/v1/Species/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<SpeciesResponse>> GetByIdAsync(int id);

        [Get("/v1/Species")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<SpeciesResponse[]>> GetAllAsync();

        [Post("/v1/Species")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<int>> PostAsync([Body] Species species);

        [Put("/v1/Species/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<dynamic>> PutAsync(int id, [Body] Species species);

        [Delete("/v1/Species/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<dynamic>> DeleteAsync(int id);
    }
}
