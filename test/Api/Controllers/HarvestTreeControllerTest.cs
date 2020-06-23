using System;
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
    public class HarvestTreeControllerTests
    {
        private LoginResponse _loginResponse;
        private string _url = "https://upperds-api.azurewebsites.net";
        private User _user = new User() { UserName = "Upperds", Password = "q1w2e3r4!1@2#3$4" };

        [Test]
        public async Task GetById()
        {
            try
            {
                ConfigureLoginClient();

                var tree = CreateTree();

                var harvestTree = new HarvestTree
                {
                    Information = "Colheita de Laranjeiras",
                    GrossWeight = 100d,
                    Date = DateTime.UtcNow,
                    TreeId = tree.Id
                };

                var harvestTreeClient = ConfigureHarvestTreeClient();
                var harvestGroupTreesId = harvestTreeClient.PostAsync(harvestTree).Result.Content;
                var harvestGroupTreesResponse = await harvestTreeClient.GetByIdAsync(harvestGroupTreesId);

                await harvestTreeClient.DeleteAsync(harvestGroupTreesId);
                await DeleteTree(tree);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, harvestGroupTreesResponse.StatusCode);
                    Assert.IsTrue(
                       harvestTree.Information == harvestGroupTreesResponse.Content.Information
                    && harvestTree.GrossWeight == harvestGroupTreesResponse.Content.GrossWeight
                    && harvestTree.Date == harvestGroupTreesResponse.Content.Date
                    && harvestTree.TreeId == harvestGroupTreesResponse.Content.TreeId);
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
                ConfigureLoginClient();

                var tree = CreateTree();

                var harvestTree = new HarvestTree
                {
                    Information = "Colheita de Laranjeiras",
                    GrossWeight = 100d,
                    Date = DateTime.UtcNow,
                    TreeId = tree.Id
                };

                var harvestTreeClient = ConfigureHarvestTreeClient();
                var harvestGroupTreesId = harvestTreeClient.PostAsync(harvestTree).Result.Content;
                var harvestGroupTreesResponse = await harvestTreeClient.GetAllAsync();

                await harvestTreeClient.DeleteAsync(harvestGroupTreesId);
                await DeleteTree(tree);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, harvestGroupTreesResponse.StatusCode);
                    Assert.IsTrue(
                       harvestTree.Information == harvestGroupTreesResponse.Content[0].Information
                    && harvestTree.GrossWeight == harvestGroupTreesResponse.Content[0].GrossWeight
                    && harvestTree.Date == harvestGroupTreesResponse.Content[0].Date
                    && harvestTree.TreeId == harvestGroupTreesResponse.Content[0].TreeId);
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
                ConfigureLoginClient();

                var tree = CreateTree();

                var harvestTree = new HarvestTree
                {
                    Information = "Colheita de Laranjeiras",
                    GrossWeight = 100d,
                    Date = DateTime.UtcNow,
                    TreeId = tree.Id
                };

                var harvestTreeClient = ConfigureHarvestTreeClient();
                var response = await harvestTreeClient.PostAsync(harvestTree);

                await harvestTreeClient.DeleteAsync(response.Content);
                await DeleteTree(tree);

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
                ConfigureLoginClient();

                var tree = CreateTree();

                var harvestTree = new HarvestTree
                {
                    Information = "Colheita de Laranjeiras",
                    GrossWeight = 100d,
                    Date = DateTime.UtcNow,
                    TreeId = tree.Id
                };

                var harvestTreeClient = ConfigureHarvestTreeClient();
                var harvestTreeId = harvestTreeClient.PostAsync(harvestTree).Result.Content;
                harvestTree.Id = harvestTreeId;
                harvestTree.GrossWeight = 240d;

                var response = await harvestTreeClient.PutAsync(harvestTreeId, harvestTree);
                var harvestGroupTreesResponse = await harvestTreeClient.GetByIdAsync(harvestTreeId);
                await harvestTreeClient.DeleteAsync(harvestTreeId);
                await DeleteTree(tree);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                    Assert.IsTrue(
                       harvestTree.Information == harvestGroupTreesResponse.Content.Information
                    && harvestTree.GrossWeight == harvestGroupTreesResponse.Content.GrossWeight
                    && harvestTree.Date == harvestGroupTreesResponse.Content.Date
                    && harvestTree.TreeId == harvestGroupTreesResponse.Content.TreeId);
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
                ConfigureLoginClient();

                var tree = CreateTree();

                var harvestTree = new HarvestTree
                {
                    Information = "Colheita de Laranjeiras",
                    GrossWeight = 100d,
                    Date = DateTime.UtcNow,
                    TreeId = tree.Id
                };

                var harvestTreeClient = ConfigureHarvestTreeClient();
                var harvestGroupTreesId = harvestTreeClient.PostAsync(harvestTree).Result.Content;
                var response = await harvestTreeClient.DeleteAsync(harvestGroupTreesId);
                await DeleteTree(tree);

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

        private IHarvestTreeClient ConfigureHarvestTreeClient()
        {
            return RestService.For<IHarvestTreeClient>(_url, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () =>
                    Task.FromResult(_loginResponse.Token)
            });
        }

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

        private ITreeClient ConfigureTreeClient()
            => RestService.For<ITreeClient>(_url, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () =>
                    Task.FromResult(_loginResponse.Token)
            });

        private Tree CreateTree()
        {
            var species = new Species { Code = "4000", Description = "Laranjeira" };
            var groupTrees = new GroupTrees { Code = "4500", Name = "Laranjeiras", Description = "Grupo das Laranjeiras" };

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
            return tree;
        }

        private async Task DeleteTree(Tree tree)
        {
            var treeClient = ConfigureTreeClient();
            var speciesClient = ConfigureSpeciesClient();
            var groupTreesClient = ConfiguregroupTreesClient();

            await treeClient.DeleteAsync(tree.Id);
            await speciesClient.DeleteAsync((int)tree.SpeciesId);
            await groupTreesClient.DeleteAsync((int)tree.GroupTreesId);
        }
    }
}
