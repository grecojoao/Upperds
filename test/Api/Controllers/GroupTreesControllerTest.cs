using System.Net;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Login;
using NUnit.Framework;
using Refit;
using test.Service;

namespace test.Api.Controllers
{
    public class GroupTreesControllerTest
    {
        private string _url = "https://upperds-api.azurewebsites.net";
        private User _user = new User() { UserName = "Upperds", Password = "q1w2e3r4!1@2#3$4" };

        [Test]
        public async Task GetById()
        {
            try
            {
                var groupTreesClient = ConfiguregroupTreesClient();
                var groupTrees = CreateGroupTrees();
                var id = groupTreesClient.PostAsync(groupTrees).Result.Content;
                var groupTreesResponse = await groupTreesClient.GetByIdAsync(id);
                await groupTreesClient.DeleteAsync(id);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, groupTreesResponse.StatusCode);
                    Assert.IsTrue(
                        groupTrees.Code == groupTreesResponse.Content.Code
                    && groupTrees.Name == groupTreesResponse.Content.Name
                    && groupTrees.Description == groupTreesResponse.Content.Description
                    && groupTrees.IsDeleted == groupTreesResponse.Content.IsDeleted);
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
                var groupTreesClient = ConfiguregroupTreesClient();
                var groupTrees = CreateGroupTrees();
                var id = groupTreesClient.PostAsync(groupTrees).Result.Content;
                var groupTreesResponse = await groupTreesClient.GetAllAsync();
                await groupTreesClient.DeleteAsync(id);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, groupTreesResponse.StatusCode);
                    Assert.IsTrue(
                       groupTrees.Code == groupTreesResponse.Content[0].Code
                    && groupTrees.Name == groupTreesResponse.Content[0].Name
                    && groupTrees.Description == groupTreesResponse.Content[0].Description
                    && groupTrees.IsDeleted == groupTreesResponse.Content[0].IsDeleted);
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
                var groupTreesClient = ConfiguregroupTreesClient();
                var groupTrees = CreateGroupTrees();
                var response = await groupTreesClient.PostAsync(groupTrees);
                await groupTreesClient.DeleteAsync(response.Content);

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
                var groupTreesClient = ConfiguregroupTreesClient();
                var groupTrees = CreateGroupTrees();
                var id = groupTreesClient.PostAsync(groupTrees).Result.Content;
                groupTrees.Id = id;
                groupTrees.Name = "Limoeiros";
                groupTrees.Description = "Grupo dos Limoeiros";
                var response = await groupTreesClient.PutAsync(id, groupTrees);
                var groupTreesResponse = await groupTreesClient.GetByIdAsync(id);
                await groupTreesClient.DeleteAsync(id);

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                    Assert.IsTrue(groupTrees.Name == groupTreesResponse.Content.Name
                    && groupTrees.Description == groupTreesResponse.Content.Description);
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
                var groupTreesClient = ConfiguregroupTreesClient();
                var groupTrees = CreateGroupTrees();
                var id = groupTreesClient.PostAsync(groupTrees).Result.Content;
                var response = await groupTreesClient.DeleteAsync(id);
                var groupTreesResponse = await groupTreesClient.GetByIdAsync(id);
                await groupTreesClient.DeleteAsync(id);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        private IGroupTreesClient ConfiguregroupTreesClient()
        {
            var loginClient = RestService.For<ILoginClient>(_url);
            var loginResponse = loginClient.Autenticar(_user).Result;

            return RestService.For<IGroupTreesClient>(_url, new RefitSettings()
            {
                AuthorizationHeaderValueGetter = () =>
                    Task.FromResult(loginResponse.Token)
            });
        }

        private GroupTrees CreateGroupTrees() => new GroupTrees { Code = "4500", Name = "Laranjeiras", Description = "Grupo das Laranjeiras" };
    }
}
