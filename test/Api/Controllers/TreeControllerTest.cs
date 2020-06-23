using System.Net;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Login;
using NUnit.Framework;
using Refit;
using test.Domain.Models;
using test.Service;

namespace test.Api.Controllers
{
    public class TreeControllerTest
    {
        private LoginResponse _loginResponse;
        private string _url = "https://upperds-api.azurewebsites.net";
        private User _user = new User() { UserName = "Upperds", Password = "q1w2e3r4!1@2#3$4" };

        [Test]
        public async Task GetById()
        {
            try
            {
                var species = CreateSpecies();
                var groupTrees = CreateGroupTrees();

                ConfigureLoginClient();
                var speciesClient = ConfigureSpeciesClient();
                var groupTreesClient = ConfiguregroupTreesClient();
                var treeClient = ConfigureTreeClient();

                var speciesId = speciesClient.PostAsync(species).Result.Content;
                var groupTreesId = groupTreesClient.PostAsync(groupTrees).Result.Content;

                var tree = new Tree
                {
                    Code = "5000",
                    Description = "Laranjeira",
                    Age = 365,
                    SpeciesId = speciesId,
                    GroupTreesId = groupTreesId
                };

                var treeId = treeClient.PostAsync(tree).Result.Content;
                var treeResponse = await treeClient.GetByIdAsync(treeId);

                tree.Id = treeId;

                await DeleteTree(tree);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, treeResponse.StatusCode);
                    Assert.IsTrue(
                       tree.Code == treeResponse.Content.Code
                    && tree.Description == treeResponse.Content.Description
                    && tree.Age == treeResponse.Content.Age
                    && tree.SpeciesId == treeResponse.Content.SpeciesId
                    && tree.GroupTreesId == treeResponse.Content.GroupTreesId);
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
                var species = CreateSpecies();
                var groupTrees = CreateGroupTrees();

                ConfigureLoginClient();
                var speciesClient = ConfigureSpeciesClient();
                var groupTreesClient = ConfiguregroupTreesClient();
                var treeClient = ConfigureTreeClient();

                var speciesId = speciesClient.PostAsync(species).Result.Content;
                var groupTreesId = groupTreesClient.PostAsync(groupTrees).Result.Content;

                var tree = new Tree
                {
                    Code = "5000",
                    Description = "Laranjeira",
                    Age = 365,
                    SpeciesId = speciesId,
                    GroupTreesId = groupTreesId
                };

                var treeId = treeClient.PostAsync(tree).Result.Content;
                var treesResponse = await treeClient.GetAllAsync();

                tree.Id = treeId;

                await DeleteTree(tree);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, treesResponse.StatusCode);
                    Assert.IsTrue(
                       tree.Code == treesResponse.Content[0].Code
                    && tree.Description == treesResponse.Content[0].Description
                    && tree.Age == treesResponse.Content[0].Age
                    && tree.SpeciesId == treesResponse.Content[0].SpeciesId
                    && tree.GroupTreesId == treesResponse.Content[0].GroupTreesId);
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
                var species = CreateSpecies();
                var groupTrees = CreateGroupTrees();

                ConfigureLoginClient();
                var speciesClient = ConfigureSpeciesClient();
                var groupTreesClient = ConfiguregroupTreesClient();
                var treeClient = ConfigureTreeClient();

                var speciesId = speciesClient.PostAsync(species).Result.Content;
                var groupTreesId = groupTreesClient.PostAsync(groupTrees).Result.Content;

                var tree = new Tree
                {
                    Code = "5000",
                    Description = "Laranjeira",
                    Age = 365,
                    SpeciesId = speciesId,
                    GroupTreesId = groupTreesId
                };

                var treeId = treeClient.PostAsync(tree).Result.Content;
                var treeResponse = await treeClient.GetByIdAsync(treeId);
                tree.Id = treeResponse.Content.Id;

                await DeleteTree(tree);

                Assert.AreEqual(HttpStatusCode.OK, treeResponse.StatusCode);
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
                var species = CreateSpecies();
                var groupTrees = CreateGroupTrees();

                ConfigureLoginClient();
                var speciesClient = ConfigureSpeciesClient();
                var groupTreesClient = ConfiguregroupTreesClient();
                var treeClient = ConfigureTreeClient();

                var speciesId = speciesClient.PostAsync(species).Result.Content;
                var groupTreesId = groupTreesClient.PostAsync(groupTrees).Result.Content;

                var tree = new Tree
                {
                    Code = "5000",
                    Description = "Laranjeira",
                    Age = 365,
                    SpeciesId = speciesId,
                    GroupTreesId = groupTreesId
                };

                var treeId = treeClient.PostAsync(tree).Result.Content;
                tree.Id = treeId;
                tree.Age = 620;
                var response = await treeClient.PutAsync(treeId, tree);
                var treeResponse = await treeClient.GetByIdAsync(treeId);

                await DeleteTree(tree);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                    Assert.IsTrue(
                       tree.Code == treeResponse.Content.Code
                    && tree.Description == treeResponse.Content.Description
                    && tree.Age == treeResponse.Content.Age
                    && tree.SpeciesId == treeResponse.Content.SpeciesId
                    && tree.GroupTreesId == treeResponse.Content.GroupTreesId);
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
                var species = CreateSpecies();
                var groupTrees = CreateGroupTrees();

                ConfigureLoginClient();
                var speciesClient = ConfigureSpeciesClient();
                var groupTreesClient = ConfiguregroupTreesClient();
                var treeClient = ConfigureTreeClient();

                var speciesId = speciesClient.PostAsync(species).Result.Content;
                var groupTreesId = groupTreesClient.PostAsync(groupTrees).Result.Content;

                var tree = new Tree
                {
                    Code = "5000",
                    Description = "Laranjeira",
                    Age = 365,
                    SpeciesId = speciesId,
                    GroupTreesId = groupTreesId
                };

                var treeId = treeClient.PostAsync(tree).Result.Content;
                tree.Id = treeId;
                var response = await DeleteTree(tree);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        private void ConfigureLoginClient()
        {
            var loginClient = RestService.For<ILoginClient>(_url);
            _loginResponse = loginClient.Autenticar(_user).Result;
        }

        private ITreeClient ConfigureTreeClient()
            => RestService.For<ITreeClient>(_url, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () =>
                    Task.FromResult(_loginResponse.Token)
            });

        private ISpeciesClient ConfigureSpeciesClient()
            => RestService.For<ISpeciesClient>(_url, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () =>
                    Task.FromResult(_loginResponse.Token)
            });

        private IGroupTreesClient ConfiguregroupTreesClient()
            => RestService.For<IGroupTreesClient>(_url, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () =>
                    Task.FromResult(_loginResponse.Token)
            });

        private Species CreateSpecies() => new Species { Code = "4000", Description = "Laranjeira" };

        private GroupTrees CreateGroupTrees() => new GroupTrees { Code = "4500", Name = "Laranjeiras", Description = "Grupo das Laranjeiras" };

        private async Task<ApiResponse<dynamic>> DeleteTree(Tree tree)
        {
            var treeClient = ConfigureTreeClient();
            var speciesClient = ConfigureSpeciesClient();
            var groupTreesClient = ConfiguregroupTreesClient();

            var response = await treeClient.DeleteAsync(tree.Id);
            await speciesClient.DeleteAsync((int)tree.SpeciesId);
            await groupTreesClient.DeleteAsync((int)tree.GroupTreesId);
            return response;
        }
    }
}
