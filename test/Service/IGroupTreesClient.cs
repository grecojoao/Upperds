using System.Threading.Tasks;
using Domain.Models;
using Refit;
using test.Domain.Models;

namespace test.Service
{
    public interface IGroupTreesClient
    {
        [Get("/v1/GroupTrees/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<GroupTreesResponse>> GetByIdAsync(int id);

        [Get("/v1/GroupTrees")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<GroupTreesResponse[]>> GetAllAsync();

        [Post("/v1/GroupTrees")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<int>> PostAsync([Body] GroupTrees groupTrees);

        [Put("/v1/GroupTrees/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<dynamic>> PutAsync(int id, [Body] GroupTrees groupTrees);

        [Delete("/v1/GroupTrees/{id}")]
        [Headers("Authorization: Bearer")]
        Task<ApiResponse<dynamic>> DeleteAsync(int id);
    }
}
