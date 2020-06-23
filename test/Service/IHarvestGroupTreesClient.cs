using System.Threading.Tasks;
using Domain.Models;
using Refit;
using test.Domain.Models;

namespace test.Service
{
    public interface IHarvestGroupTreesClient
    {
        [Get("/v1/HarvestGroupTrees/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<HarvestGroupTreesResponse>> GetByIdAsync(int id);

        [Get("/v1/HarvestGroupTrees")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<HarvestGroupTreesResponse[]>> GetAllAsync();

        [Post("/v1/HarvestGroupTrees")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<int>> PostAsync([Body] HarvestGroupTrees harvestGroupTrees);

        [Put("/v1/HarvestGroupTrees/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<dynamic>> PutAsync(int id, [Body] HarvestGroupTrees harvestGroupTrees);

        [Delete("/v1/HarvestGroupTrees/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<dynamic>> DeleteAsync(int id);
    }
}
