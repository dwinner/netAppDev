using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ASPNETCoreSample.IntegrationTest;

public class AspnetCoreSampleTest(WebApplicationFactory<Startup> factory) : IClassFixture<WebApplicationFactory<Startup>>
{
   [Fact]
   public async Task ReturnHelloWorld()
   {
      // arrange
      var client = factory.CreateClient();

      // act
      var response = await client.GetAsync("/");

      // assert
      response.EnsureSuccessStatusCode();
      var responseString = await response.Content.ReadAsStringAsync();
      Assert.Equal("Hello World!", responseString);
   }
}