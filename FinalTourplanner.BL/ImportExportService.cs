using FinalTourplanner.DL;
using FinalTourplanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalTourplanner.BL
{
    public class ImportExportService
    {
        private TourRepository tourepo;
        public ImportExportService()
        {
            this.tourepo = new TourRepository();
        }

        public void ImportTourData(string path)
        {
            var json = File.ReadAllText(path);
            Tour input = JsonSerializer.Deserialize<Tour>(json);
            if (input != null)
            {
                tourepo.AddTour(input);
            }
        }

        public void ExportTourData(string path, string tourName)
        {
            Tour tour = tourepo.GetSpecificTour(tourName);
            if (tour != null)
            {
                try
                {
                    var json = JsonSerializer.Serialize(tour, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(path, json);
                    Console.WriteLine("Tour erfolgreich exportiert.");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Zugriffsfehler: {ex.Message}");
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine($"Verzeichnis nicht gefunden: {ex.Message}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Ein-/Ausgabefehler: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}");
                }
            }
        }
    }
}
