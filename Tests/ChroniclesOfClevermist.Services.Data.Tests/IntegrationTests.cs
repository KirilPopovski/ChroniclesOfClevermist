namespace ChroniclesOfClevermist.Services.Data.Tests
{
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Web;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Xunit;

    public class IntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public IntegrationTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task TestHomePageReturnsWelcomeText()
        {
            var client = this.factory.CreateClient();
            var response = await client.GetAsync("/");
            Assert.Contains("Welcome", response.Content.ReadAsStringAsync().Result);
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/News/All")]
        [InlineData("/Contacts/SiteMap")]
        [InlineData("/QuestionsAndAnswers/All")]
        [InlineData("/Contacts/About")]
        [InlineData("/Contacts/History")]
        public async Task AllPagesShouldReturnSuccessStatusCode(string path)
        {
            var client = this.factory.CreateClient();
            var response = await client.GetAsync(path);
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
