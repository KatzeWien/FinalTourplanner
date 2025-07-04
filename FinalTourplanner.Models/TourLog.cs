using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTourplanner.Models
{
    public class TourLog : INotifyPropertyChanged
    {
        private int id;
        private string nameOfTour;
        private DateTime dateInput;
        private string comment;
        private string difficulty;
        private int distance;
        private TimeSpan totalTime;
        private string rating;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get => id; set { id = value; OnPropertyChanged(nameof(Id)); } }
        public string NameOfTour { get => nameOfTour; set { nameOfTour = value; OnPropertyChanged(nameof(NameOfTour)); } }
        public DateTime DateInput { get => dateInput; set { dateInput = value; OnPropertyChanged(nameof(DateInput)); } }
        public string Comment { get => comment; set { comment = value; OnPropertyChanged(nameof(Comment)); } }
        public string Difficulty { get => difficulty; set { difficulty = value; OnPropertyChanged(nameof(Difficulty)); } }
        public int Distance { get => distance; set { distance = value; OnPropertyChanged(nameof(Distance)); } }
        public TimeSpan TotalTime { get => totalTime; set { totalTime = value; OnPropertyChanged(nameof(TotalTime)); } }
        public string Rating { get => rating; set { rating = value; OnPropertyChanged(nameof(Rating)); } }
        public TourLog(string nameOfTour, DateTime date, string comment, string difficulty, int totalDistance, TimeSpan totalTime, string rating)
        {
            this.NameOfTour = nameOfTour;
            this.DateInput = date;
            this.Comment = comment;
            this.Difficulty = difficulty;
            this.Distance = totalDistance;
            this.TotalTime = totalTime;
            this.Rating = rating;
        }
        public TourLog()
        { }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
