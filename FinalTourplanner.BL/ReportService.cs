using FinalTourplanner.DL;
using FinalTourplanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Text;
using System.Threading.Tasks;

namespace FinalTourplanner.BL
{
    public class ReportService
    {
        private TourRepository tourepo;
        private readonly string reportFolder;
        public ReportService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            this.tourepo = new TourRepository();
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var solutionDir = Directory.GetParent(baseDir)!.Parent!.Parent!.Parent!.FullName;
            this.reportFolder = Path.Combine(solutionDir, "Reports");
            Directory.CreateDirectory(reportFolder);
        }
        public void CreateTourReport(string tourName, string imagePath)
        {
            Tour tour = tourepo.GetSpecificTour(tourName);
            if (tour == null)
            {
                throw new ArgumentNullException($"tour {tourName} not found");
            }
            else
            {
                var filePath = Path.Combine(reportFolder, $"{tourName}_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
                var doc = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Header().Text($"Tourreport: {tourName}").Bold();
                        page.Content().Column(col =>
                        {
                            col.Item().Text($"Description: {tour.Description}");
                            col.Item().Text($"Start Point: {tour.FromInput}");
                            col.Item().Text($"Destination Point: {tour.ToInput}");
                            col.Item().Text($"Transport Type: {tour.TransportType}");
                            col.Item().Text($"Distance: {tour.Distance}");
                            col.Item().Text($"Estimated Time: {tour.EstimatedTime}");
                            col.Item().Image(imagePath).FitWidth();
                        });
                    });
                });
                doc.GeneratePdf(filePath);
            }
        }
    }
}
