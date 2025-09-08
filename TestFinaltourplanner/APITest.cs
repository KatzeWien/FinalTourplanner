using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text.Json;

namespace TestFinaltourplanner
{
    public class APITest
    {
        private string apiKey;
        private HttpClient http;
        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets(typeof(APITest).Assembly)
                .Build();

            apiKey = config["OpenRouteService:ApiKey"];
            http = new HttpClient();
        }
        [TearDown]
        public void Cleanup()
        {
            http?.Dispose();
        }
        [Test]
        public void ShouldLoadApiKeyFromUserSecrets()
        {
            Assert.That(apiKey, Is.Not.Null.And.Not.Empty, "Key wurde geladen");
        }
        [Test]
        public async Task GetCoordinates_ShouldReturnValidCoordinatesForVienna()
        {
            var address = "Wien, Österreich";
            var url = $"https://api.openrouteservice.org/geocode/search"
                    + $"?api_key={apiKey}"
                    + $"&text={Uri.EscapeDataString(address)}";

            var json = await http.GetStringAsync(url).ConfigureAwait(false);
            using var jsonDoc = JsonDocument.Parse(json);
            var features = jsonDoc.RootElement.GetProperty("features")[0];
            var coordinates = features.GetProperty("geometry").GetProperty("coordinates");
            var result = new[] { coordinates[0].GetDouble(), coordinates[1].GetDouble() };

            // Wien liegt ungefähr bei 16.37 / 48.20
            Assert.That(result[0], Is.InRange(16.3, 16.5), "Koordinate innerhalb erwarteter Range");
            Assert.That(result[1], Is.InRange(48.1, 48.3), "Koordinate innerhalb erwarteter Range");
        }
    }
}
