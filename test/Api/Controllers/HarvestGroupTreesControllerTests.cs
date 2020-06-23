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
    public class HarvestGroupTreesControllerTests
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
                var groupTreesClient = ConfigureGroupTreesClient();
                var harvestGroupTreesClient = ConfigureHarvestGroupTreesClient();

                var groupTrees = CreateGroupTrees();
                var groupTreesId = groupTreesClient.PostAsync(groupTrees).Result.Content;

                var harvestGroupTrees = new HarvestGroupTrees
                {
                    Information = "Colheita de Laranjeiras",
                    GrossWeight = 100d,
                    Date = DateTime.UtcNow,
                    GroupId = groupTreesId
                };

                var harvestGroupTreesId = harvestGroupTreesClient.PostAsync(harvestGroupTrees).Result.Content;
                var harvestGroupTreesResponse = await harvestGroupTreesClient.GetByIdAsync(harvestGroupTreesId);

                await harvestGroupTreesClient.DeleteAsync(harvestGroupTreesId);
                await groupTreesClient.DeleteAsync(groupTreesId);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, harvestGroupTreesResponse.StatusCode);
                    Assert.IsTrue(
                       harvestGroupTrees.Information == harvestGroupTreesResponse.Content.Information
                    && harvestGroupTrees.GrossWeight == harvestGroupTreesResponse.Content.GrossWeight
                    && harvestGroupTrees.Date == harvestGroupTreesResponse.Content.Date
                    && harvestGroupTrees.GroupId == harvestGroupTreesResponse.Content.GroupId);
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
                var groupTreesClient = ConfigureGroupTreesClient();
                var harvestGroupTreesClient = ConfigureHarvestGroupTreesClient();

                var groupTrees = CreateGroupTrees();
                var groupTreesId = groupTreesClient.PostAsync(groupTrees).Result.Content;

                var harvestGroupTrees = new HarvestGroupTrees
                {
                    Information = "Colheita de Laranjeiras",
                    GrossWeight = 100,
                    Date = DateTime.UtcNow,
                    GroupId = groupTreesId
                };

                var harvestGroupTreesId = harvestGroupTreesClient.PostAsync(harvestGroupTrees).Result.Content;
                var harvestGroupTreesResponse = await harvestGroupTreesClient.GetAllAsync();

                await harvestGroupTreesClient.DeleteAsync(harvestGroupTreesId);
                await groupTreesClient.DeleteAsync(groupTreesId);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, harvestGroupTreesResponse.StatusCode);
                    Assert.IsTrue(
                       harvestGroupTrees.Information == harvestGroupTreesResponse.Content[0].Information
                    && harvestGroupTrees.GrossWeight == harvestGroupTreesResponse.Content[0].GrossWeight
                    && harvestGroupTrees.Date == harvestGroupTreesResponse.Content[0].Date
                    && harvestGroupTrees.GroupId == harvestGroupTreesResponse.Content[0].GroupId);
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
                var groupTreesClient = ConfigureGroupTreesClient();
                var harvestGroupTreesClient = ConfigureHarvestGroupTreesClient();

                var groupTrees = CreateGroupTrees();
                var groupTreesId = groupTreesClient.PostAsync(groupTrees).Result.Content;

                var harvestGroupTrees = new HarvestGroupTrees
                {
                    Information = "Colheita de Laranjeiras",
                    GrossWeight = 100,
                    Date = DateTime.UtcNow,
                    GroupId = groupTreesId
                };

                var response = await harvestGroupTreesClient.PostAsync(harvestGroupTrees);

                await harvestGroupTreesClient.DeleteAsync(response.Content);
                await groupTreesClient.DeleteAsync(groupTreesId);

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
                var groupTreesClient = ConfigureGroupTreesClient();
                var harvestGroupTreesClient = ConfigureHarvestGroupTreesClient();

                var groupTrees = CreateGroupTrees();
                var groupTreesId = groupTreesClient.PostAsync(groupTrees).Result.Content;

                var harvestGroupTrees = new HarvestGroupTrees
                {
                    Information = "Colheita de Laranjeiras",
                    GrossWeight = 100,
                    Date = DateTime.UtcNow,
                    GroupId = groupTreesId
                };

                var harvestGroupTreesId = harvestGroupTreesClient.PostAsync(harvestGroupTrees).Result.Content;

                harvestGroupTrees.Id = harvestGroupTreesId;
                harvestGroupTrees.GrossWeight = 240;

                var response = await harvestGroupTreesClient.PutAsync(harvestGroupTreesId, harvestGroupTrees);
                var harvestGroupTreesResponse = await harvestGroupTreesClient.GetByIdAsync(harvestGroupTreesId);

                await harvestGroupTreesClient.DeleteAsync(harvestGroupTreesId);
                await groupTreesClient.DeleteAsync(groupTreesId);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                    Assert.IsTrue(
                       harvestGroupTrees.Information == harvestGroupTreesResponse.Content.Information
                    && harvestGroupTrees.GrossWeight == harvestGroupTreesResponse.Content.GrossWeight
                    && harvestGroupTrees.Date == harvestGroupTreesResponse.Content.Date
                    && harvestGroupTrees.GroupId == harvestGroupTreesResponse.Content.GroupId);
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
                var groupTreesClient = ConfigureGroupTreesClient();
                var harvestGroupTreesClient = ConfigureHarvestGroupTreesClient();

                var groupTrees = CreateGroupTrees();
                var groupTreesId = groupTreesClient.PostAsync(groupTrees).Result.Content;

                var harvestGroupTrees = new HarvestGroupTrees
                {
                    Information = "Colheita de Laranjeiras",
                    GrossWeight = 100,
                    Date = DateTime.UtcNow,
                    GroupId = groupTreesId
                };

                var harvestGroupTreesId = harvestGroupTreesClient.PostAsync(harvestGroupTrees).Result.Content;
                var response = await harvestGroupTreesClient.DeleteAsync(harvestGroupTreesId);

                await groupTreesClient.DeleteAsync(groupTreesId);

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

        private IHarvestGroupTreesClient ConfigureHarvestGroupTreesClient()
            => RestService.For<IHarvestGroupTreesClient>(_url, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () =>
                    Task.FromResult(_loginResponse.Token)
            });

        private IGroupTreesClient ConfigureGroupTreesClient()
            => RestService.For<IGroupTreesClient>(_url, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () =>
                    Task.FromResult(_loginResponse.Token)
            });

        private GroupTrees CreateGroupTrees() => new GroupTrees { Code = "4500", Name = "Laranjeiras", Description = "Grupo das Laranjeiras" };
    }
}
