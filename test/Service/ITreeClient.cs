using System.Threading.Tasks;
using Domain.Models;
using Refit;
using test.Domain.Models;

namespace test.Service
{
    public interface ITreeClient
    {
        [Get("/v1/Tree/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<TreeResponse>> GetByIdAsync(int id);

        [Get("/v1/Tree")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<TreeResponse[]>> GetAllAsync();

        [Post("/v1/Tree")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<int>> PostAsync([Body] Tree tree);

        [Put("/v1/Tree/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<dynamic>> PutAsync(int id, [Body] Tree tree);

        [Delete("/v1/Tree/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<dynamic>> DeleteAsync(int id);
    }
}
