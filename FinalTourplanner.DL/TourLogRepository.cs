using FinalTourplanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTourplanner.DL
{
    public class TourLogRepository
    {
        private readonly MyDBContext context;
        public TourLogRepository()
        {
            this.context = GetDBContext.GetConnection("MyDbString");
        }
        public TourLogRepository(MyDBContext testContext)
        {
            this.context = testContext;
        }
        public void AddTourLog(TourLog tourLog)
        {
            this.context.TourLog.Add(tourLog);
            this.context.SaveChanges();
        }

        public void DeleteTourLog(int tourLogId)
        {
            var entry = this.context.TourLog.Find(tourLogId);
            if (entry != null)
            {
                context.TourLog.Remove(entry);
                context.SaveChanges();
            }
        }

        public List<TourLog> GetAllTourLogs()
        {
            return context.TourLog.ToList();
        }

        public void UpdateTourLog(TourLog tourLog, int tourLogId)
        {
            var entry = this.context.TourLog.Find(tourLogId);
            if (entry != null)
            {
                //context.TourLog.Entry(entry).CurrentValues.SetValues(tourLog);
                entry.DateInput = tourLog.DateInput;
                entry.Comment = tourLog.Comment;
                entry.Difficulty = tourLog.Difficulty;
                entry.Distance = tourLog.Distance;
                entry.TotalTime = tourLog.TotalTime;
                entry.Rating = tourLog.Rating;
                context.SaveChanges();
            }
        }

        public List<TourLog> GetSpecificTourLogs(string tourName)
        {
            return context.TourLog.Where(p => p.NameOfTour == tourName).ToList();
        }
    }
}
