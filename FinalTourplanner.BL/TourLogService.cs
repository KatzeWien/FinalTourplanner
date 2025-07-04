using FinalTourplanner.DL;
using FinalTourplanner.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTourplanner.BL
{
    public class TourLogService
    {
        private TourLogRepository tourLogRepo;
        public TourLogService()
        {
            this.tourLogRepo = new TourLogRepository();
        }
        public void AddTourLog(TourLog tourLog)
        {
            this.tourLogRepo.AddTourLog(tourLog);
        }

        public void DeleteTourLog(TourLog tourLog)
        {
            this.tourLogRepo.DeleteTourLog(tourLog.Id);
        }

        public void UpdateTourLog(TourLog tourLog, int tourLogId)
        {
            this.tourLogRepo.UpdateTourLog(tourLog, tourLogId);
        }

        public ObservableCollection<TourLog> GetTourLogs()
        {
            ObservableCollection<TourLog> tourLogs = new ObservableCollection<TourLog>(this.tourLogRepo.GetAllTourLogs());
            return tourLogs;
        }
    }
}
