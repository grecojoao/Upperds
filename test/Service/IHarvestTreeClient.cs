using System.Threading.Tasks;
using Domain.Models;
using Refit;
using test.Domain.Models;

namespace test.Service
{
    public interface IHarvestTreeClient
    {
        [Get("/v1/HarvestTree/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<HarvestTreeResponse>> GetByIdAsync(int id);

        [Get("/v1/HarvestTree")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<HarvestTreeResponse[]>> GetAllAsync();

        [Post("/v1/HarvestTree")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<int>> PostAsync([Body] HarvestTree species);

        [Put("/v1/HarvestTree/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<dynamic>> PutAsync(int id, [Body] HarvestTree species);

        [Delete("/v1/HarvestTree/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<dynamic>> DeleteAsync(int id);
    }
}
