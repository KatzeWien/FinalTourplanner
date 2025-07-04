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
    public class TourService
    {
        private TourRepository tourepo;
        public TourService()
        {
            this.tourepo = new TourRepository();
        }

        public void AddTour(Tour tour)
        {
            this.tourepo.AddTour(tour);
        }

        public void DeleteTour(Tour tour)
        {
            this.tourepo.DeleteTour(tour.Name);
        }

        public void UpdateTour(Tour tour, string oldTourName)
        {
            this.tourepo.UpdateTour(tour, oldTourName);
        }

        public ObservableCollection<Tour> GetTours()
        {
            ObservableCollection<Tour> tours = new ObservableCollection<Tour>(this.tourepo.GetAllTours());
            return tours;
        }
    }
}
