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
        private TourLogRepository tourLogRepo;
        private readonly string reportFolder;
        public ReportService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            this.tourepo = new TourRepository();
            this.tourLogRepo = new TourLogRepository();
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var solutionDir = Directory.GetParent(baseDir)!.Parent!.Parent!.Parent!.FullName;
            this.reportFolder = Path.Combine(solutionDir, "Reports");
            Directory.CreateDirectory(reportFolder);
        }
        public void CreateTourReport(string tourName, string imagePath)
        {
            Tour tour = tourepo.GetSpecificTour(tourName);
            List<TourLog> tourLogs = tourLogRepo.GetSpecificTourLogs(tourName);
            if (tour == null)
            {
                throw new ArgumentNullException($"tour {tourName} not found");
            }
            else if(tourLogs == null)
            {
                throw new ArgumentNullException($"tourlogs for {tourName} not found");
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
                            col.Item().Text("associated tour logs:");
                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                table.Header(header =>
                                {
                                    header.Cell().Text("Date");
                                    header.Cell().Text("Comment");
                                    header.Cell().Text("Difficulty");
                                    header.Cell().Text("Distance");
                                    header.Cell().Text("Total Time");
                                    header.Cell().Text("Rating");
                                });
                                foreach (var log in tourLogs)
                                {
                                    table.Cell().Text(log.DateInput.ToString("dd.MM.yyyy"));
                                    table.Cell().Text(log.Comment);
                                    table.Cell().Text(log.Difficulty);
                                    table.Cell().Text(log.Distance.ToString());
                                    table.Cell().Text(log.TotalTime.ToString(@"hh\:mm"));
                                    table.Cell().Text(log.Rating);
                                }
                            });
                        });
                    });
                });
                doc.GeneratePdf(filePath);
            }
        }
        public void CreateSummarizeReport(string tourName)
        {
            List<TourLog> logs = tourLogRepo.GetAllTourLogs();
            if (logs == null)
            {
                throw new ArgumentNullException($"tour {tourName} not found");
            }
            else
            {
                double averageTicks = logs.Average(p => p.TotalTime.Ticks);
                TimeSpan averageTime = TimeSpan.FromTicks((long)averageTicks);
                double averageDistance = logs.Average(p => p.Distance);
                var filePath = Path.Combine(reportFolder, $"Summarize_{tourName}_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
                var doc = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Header().Text($"Summarize of: {tourName}").Bold();
                        page.Content().Column(col =>
                        {
                            col.Item().Text($"Average Time: {averageTime.ToString(@"hh\:mm")}");
                            col.Item().Text($"Average Distance: {averageDistance}");
                        });
                    });
                });
                doc.GeneratePdf(filePath);
            }
        }
    }
}
