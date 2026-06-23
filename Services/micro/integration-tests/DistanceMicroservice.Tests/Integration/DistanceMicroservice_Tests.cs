namespace DistanceMicroservice.Tests.Integration;

public class DistanceMicroserviceTests
{
   [Fact]
   public async Task CallFakeGoogle_ReturnsResults()
   {
      // Arrange
      var addresses = new Addresses(
         "123 Main St, Anytown, CA",
         "456 Lincoln Ave, Anytown, CA"
      );
      Routes expectedRoutes = new([
         new Route(123, "days"),
         new Route(124, "minutes")
      ]);
      var fakeResponseBody = JsonSerializer.Serialize(expectedRoutes);
      var expectedMessage = "Number of routes found: 2";

      var fakeMessage = new HttpResponseMessage(HttpStatusCode.OK)
      {
         Content = new StringContent(fakeResponseBody, Encoding.UTF8, "application/json")
      };

      using var siteApp = new DistanceMicroserviceApp(fakeHttpResponseMessage: fakeMessage);
      using var httpClient = siteApp.CreateClient();

      // Act
      var res = await httpClient.PostAsJsonAsync("/getdistanceinfo", addresses);
      res.EnsureSuccessStatusCode();
      var body = await res.Content.ReadAsStringAsync();
      var result = JsonSerializer.Deserialize<DiscoveredRoutes>(body, new JsonSerializerOptions
      {
         PropertyNameCaseInsensitive = true
      });

      // Assert
      result.ShouldNotBeNull();
      result.Routes.ShouldBe(expectedRoutes.routes);
      result.Message.ShouldBe(expectedMessage);
   }

   [Fact]
   public async Task CallFakeGoogle_ReturnsNull()
   {
      // Arrange
      var addresses = new Addresses(
         "123 Main St, Anytown, CA",
         "456 Lincoln Ave, Anytown, CA"
      );
      Routes expectedRoutes = new([]);
      var fakeResponseBody = "";
      var expectedMessage = "Error deserializing response to Routes.";

      var fakeMessage = new HttpResponseMessage(HttpStatusCode.OK)
      {
         Content = new StringContent(fakeResponseBody, Encoding.UTF8, "application/json")
      };

      using var siteApp = new DistanceMicroserviceApp(fakeHttpResponseMessage: fakeMessage);
      using var httpClient = siteApp.CreateClient();

      // Act
      var res = await httpClient.PostAsJsonAsync("/getdistanceinfo", addresses);
      res.EnsureSuccessStatusCode();
      var body = await res.Content.ReadAsStringAsync();
      var result = JsonSerializer.Deserialize<DiscoveredRoutes>(body, new JsonSerializerOptions
      {
         PropertyNameCaseInsensitive = true
      });

      // Assert
      result.ShouldNotBeNull();
      result.Routes.ShouldBe(expectedRoutes.routes);
      result.Message.ShouldBe(expectedMessage);
   }

   [Fact]
   public async Task CallFakeGoogle_Returns500()
   {
      // Arrange
      var addresses = new Addresses(
         "123 Main St, Anytown, CA",
         "456 Lincoln Ave, Anytown, CA"
      );
      Routes expectedRoutes = new([]);
      var fakeResponseBody = "";
      var expectedMessage = "Error: 500 - Internal Server Error";

      var fakeMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
      {
         Content = new StringContent(fakeResponseBody, Encoding.UTF8, "application/json")
      };

      using var siteApp = new DistanceMicroserviceApp(fakeHttpResponseMessage: fakeMessage);
      using var httpClient = siteApp.CreateClient();

      // Act
      var res = await httpClient.PostAsJsonAsync("/getdistanceinfo", addresses);
      res.EnsureSuccessStatusCode();
      var body = await res.Content.ReadAsStringAsync();
      var result = JsonSerializer.Deserialize<DiscoveredRoutes>(body, new JsonSerializerOptions
      {
         PropertyNameCaseInsensitive = true
      });

      // Assert
      result.ShouldNotBeNull();
      result.Routes.ShouldBe(expectedRoutes.routes);
      result.Message.ShouldBe(expectedMessage);
   }
}