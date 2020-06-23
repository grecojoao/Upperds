using System.Net;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Login;
using NUnit.Framework;
using Refit;
using test.Service;

namespace test.Api.Controllers
{
    public class SpeciesControllerTest
    {
        private string _url = "https://upperds-api.azurewebsites.net";
        private User _user = new User() { UserName = "Upperds", Password = "q1w2e3r4!1@2#3$4" };

        [Test]
        public async Task GetById()
        {
            try
            {
                var speciesClient = ConfigureSpeciesClient();
                var species = CreateSpecies();
                var id = speciesClient.PostAsync(species).Result.Content;
                var speciesResponse = await speciesClient.GetByIdAsync(id);
                await speciesClient.DeleteAsync(id);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, speciesResponse.StatusCode);
                    Assert.IsTrue(species.Code == speciesResponse.Content.Code && species.Description == speciesResponse.Content.Description);
                });
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task GetAll()
        {
            try
            {
                var speciesClient = ConfigureSpeciesClient();
                var species = CreateSpecies();
                var id = speciesClient.PostAsync(species).Result.Content;
                var speciesResponse = await speciesClient.GetAllAsync();
                await speciesClient.DeleteAsync(id);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, speciesResponse.StatusCode);
                    Assert.IsTrue(species.Code == speciesResponse.Content[0].Code && species.Description == speciesResponse.Content[0].Description);
                });
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task Post()
        {
            try
            {
                var speciesClient = ConfigureSpeciesClient();
                var response = await speciesClient.PostAsync(new Species { Code = "4000", Description = "Laranjeira" });
                await speciesClient.DeleteAsync(response.Content);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task Put()
        {
            try
            {
                var speciesClient = ConfigureSpeciesClient();
                var species = CreateSpecies();
                var id = speciesClient.PostAsync(species).Result.Content;
                species.Id = id;
                species.Description = "Limoeiro";
                var response = await speciesClient.PutAsync(id, species);
                var speciesResponse = await speciesClient.GetByIdAsync(id);
                await speciesClient.DeleteAsync(id);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                    Assert.IsTrue(species.Description == speciesResponse.Content.Description);
                });
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task Delete()
        {
            try
            {
                var speciesClient = ConfigureSpeciesClient();
                var species = CreateSpecies();
                var id = speciesClient.PostAsync(species).Result.Content;
                var response = await speciesClient.DeleteAsync(id);
                var speciesResponse = await speciesClient.GetByIdAsync(id);
                await speciesClient.DeleteAsync(id);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        private ISpeciesClient ConfigureSpeciesClient()
        {
            var loginClient = RestService.For<ILoginClient>(_url);
            var loginResponse = loginClient.Autenticar(_user).Result;

            return RestService.For<ISpeciesClient>(_url, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () =>
                    Task.FromResult(loginResponse.Token)
            });
        }

        private Species CreateSpecies() => new Species { Code = "4000", Description = "Laranjeira" };
    }
}
