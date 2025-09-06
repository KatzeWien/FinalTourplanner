using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace FinalTourplanner.ViewModel
{
    public class VMShowMap : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly string apiKey;
        private readonly HttpClient http = new HttpClient();
        private Uri mapUri;
        public Uri MapUri
        {
            get => mapUri;
            set
            {
                mapUri = value;
                OnPropertyChanged(nameof(MapUri));
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public VMShowMap()
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<VMShowMap>()
                .Build();
            apiKey = config["OpenRouteService:ApiKey"];
            var htmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ressources", "map.html");
            MapUri = new Uri(htmlPath);
        }

        public async Task LoadRoute(string startInput, string destinationInput)
        {
            var start = await GetCoordinates(startInput);
            var destination = await GetCoordinates(destinationInput);
            var geo = await GetDirection(start, destination).ConfigureAwait(false);
            var jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Ressources", "Direction.js");
            File.WriteAllText(jsonPath, $"var directions = {geo};");
            if (File.Exists(jsonPath))
            {
                var info = new FileInfo(jsonPath);
                //MessageBox.Show($"Direction.js zuletzt geändert: {info.LastWriteTime}");
            }
            else
            {
                //MessageBox.Show("Direction.js existiert nicht!");
            }
            MapUri = new Uri(MapUri.GetLeftPart(UriPartial.Path) + "?v=" + DateTime.Now.Ticks);
        }

        private async Task<Double[]> GetCoordinates(string address)
        {
            var url = $"https://api.openrouteservice.org/geocode/search"
                    + $"?api_key={apiKey}"
                    + $"&text={Uri.EscapeDataString(address)}";
            var json = await http.GetStringAsync(url).ConfigureAwait(false);
            using var jsonDoc = JsonDocument.Parse(json);
            var features = jsonDoc.RootElement.GetProperty("features")[0];
            var coordinates = features.GetProperty("geometry").GetProperty("coordinates");
            return new[] { coordinates[0].GetDouble(), coordinates[1].GetDouble() };
        }

        private async Task<string> GetDirection(double[] start, double[] destination)
        {
            /*var url = $"https://api.openrouteservice.org/v2/directions/driving-car"
                    + $"?api_key={apiKey}"
                    + $"&start={start[0]},{start[1]}"
                    + $"&end={destination[0]},{destination[1]}";
            var result = await http.GetStringAsync(url).ConfigureAwait(false);
            return result;*/
            var url = $"https://api.openrouteservice.org/v2/directions/driving-car"
            + $"?api_key={apiKey}"
            + $"&start={start[0].ToString(System.Globalization.CultureInfo.InvariantCulture)},{start[1].ToString(System.Globalization.CultureInfo.InvariantCulture)}"
            + $"&end={destination[0].ToString(System.Globalization.CultureInfo.InvariantCulture)},{destination[1].ToString(System.Globalization.CultureInfo.InvariantCulture)}";


            Debug.WriteLine($"[DEBUG] Requesting: {url}");

            try
            {
                var result = await http.GetStringAsync(url).ConfigureAwait(false);
                Console.WriteLine("[DEBUG] Response received");
                return result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"[ERROR] HTTP error: {ex.Message}");
                throw;
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine("[ERROR] Request timed out");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Unexpected error: {ex.Message}");
                throw;
            }
        }
    }
}
