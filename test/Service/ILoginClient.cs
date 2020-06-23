using System.Threading.Tasks;
using Domain.Models.Login;
using Refit;
using test.Domain.Models;

namespace test.Service
{
    public interface ILoginClient
    {
        [Post("/v1/Login")]
        Task<LoginResponse> Autenticar(User user);
    }
}
