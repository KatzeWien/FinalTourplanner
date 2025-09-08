using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalTourplanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalTourplanner.DL
{
    public class TourRepository
    {
        private readonly MyDBContext context;
        public TourRepository()
        {
            this.context = GetDBContext.GetConnection();
        }
        public void AddTour(Tour tour)
        {
            this.context.Tour.Add(tour);
            this.context.SaveChanges();
        }

        public void DeleteTour(string name)
        {
            var entry = this.context.Tour.Find(name);
            if (entry != null)
            {
                context.Tour.Remove(entry);
                context.SaveChanges();
            }
        }

        public List<Tour> GetAllTours()
        {
            return context.Tour.ToList();
        }

        public void UpdateTour(Tour tour, string oldTourName)
        {
            var entry = this.context.Tour.Find(oldTourName);
            if (entry != null)
            {
                context.Tour.Entry(entry).CurrentValues.SetValues(tour);
                context.SaveChanges();
            }
        }

        public Tour GetSpecificTour(string name)
        {
            Tour entry = this.context.Tour.Find(name);
            if (entry != null)
            {
                return entry;
            }
            else
            {
                return null;
            }
        }


        //case insensitive suche mit Ilike
        public List<Tour> GetToursByName(string? text)
        {
            IQueryable<Tour> q = context.Tour.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(text))
            {
                var pattern = $"%{text.Trim()}%";
                q = q.Where(t => EF.Functions.ILike(t.Name, pattern));
            }


            return q.OrderBy(t => t.Name)
                    .Take(200)
                    .ToList();
        }

    }
}
