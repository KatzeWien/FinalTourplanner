using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalTourplanner.BL;
using FinalTourplanner.Models;

namespace FinalTourplanner.ViewModel
{
    public class AllDataManagement
    {
        
        public ObservableCollection<Tour> Tours { get; set; } = new ObservableCollection<Tour>();
        public ObservableCollection<TourLog> Logs { get; set; } = new ObservableCollection<TourLog>();
        public ObservableCollection<string> TourNames { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<TourLog> SpecialLogs { get; set; } = new ObservableCollection<TourLog>();
        public AllDataManagement()
        {
            Tour tour = new Tour("name2", "desc", "from", "to", "bim", 5, new TimeSpan(12, 00, 00));
            //AddTour(tour);
            TourLog tourLog = new TourLog("name2", new DateTime(2024, 10, 27, 12, 00, 00, DateTimeKind.Utc), "comment", "easy", 2, new TimeSpan(10, 00, 00), "it was easy");
            //AddTourLog(tourLog);
            GetAllTourNames();
            GetAllTourLogs();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void GetAllTours()
        {
            TourService tourService = new TourService();
            Tours = tourService.GetTours();
        }

        public void GetAllTourLogs()
        {
            TourLogService tourLogService = new TourLogService();
            Logs = tourLogService.GetTourLogs();
        }
        public void AddTourLog(TourLog tourLog)
        {
            TourLogService tourLogService = new TourLogService();
            tourLogService.AddTourLog(tourLog);
        }

        public void AddTour(Tour tour)
        {
            TourService tourService = new TourService();
            tourService.AddTour(tour);
        }

        public void DeleteTour(Tour tour)
        {
            TourService tourService = new TourService();
            tourService.DeleteTour(tour);
            GetAllTours();
        }

        public void GetAllTourNames()
        {
            GetAllTours();
            if (TourNames.Count > 0)
            {
                TourNames.Clear();
            }
            foreach (var tourname in Tours.Select(p => p.Name))
            {
                TourNames.Add(tourname);
            }
        }

        public void GetLogsBasedOnTourname(string tourname)
        {
            if (SpecialLogs.Count > 0)
            {
                SpecialLogs.Clear();
            }
            foreach (var log in Logs.Where(p => p.NameOfTour == tourname))
            {
                SpecialLogs.Add(log);
            }
        }

        public void DeleteTourLogBasedOnIndex(int index)
        {
            Logs.RemoveAt(index);
        }

        public void DeleteTourLog(TourLog tourLog)
        {
            TourLogService tourLogService = new TourLogService();
            tourLogService.DeleteTourLog(tourLog);
        }
    }
}
