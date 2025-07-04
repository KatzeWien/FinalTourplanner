using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTourplanner.Models
{
    public class Tour : INotifyPropertyChanged
    {
        private string name;
        private string description;
        private string fromInput;
        private string toInput;
        private string transportType;
        private int distance;
        private TimeSpan estimatedTime;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name
        { get => name; set { if (name != value) { name = value; OnPropertyChanged(nameof(Name)); } } }
        public string Description
        { get => description; set { if (description != value) { description = value; OnPropertyChanged(nameof(Description)); } } }
        public string FromInput
        { get => fromInput; set { if (fromInput != value) { fromInput = value; OnPropertyChanged(nameof(FromInput)); } } }
        public string ToInput
        { get => toInput; set { if (toInput != value) { toInput = value; OnPropertyChanged(nameof(ToInput)); } } }
        public string TransportType
        { get => transportType; set { if (transportType != value) { transportType = value; OnPropertyChanged(nameof(TransportType)); } } }
        public int Distance
        { get => distance; set { if (distance != value) { distance = value; OnPropertyChanged(nameof(Distance)); } } }
        public TimeSpan EstimatedTime
        { get => estimatedTime; set { if (estimatedTime != value) { estimatedTime = value; OnPropertyChanged(nameof(EstimatedTime)); } } }
        public Tour(string name, string description, string from, string to, string transportType, int tourDistance, TimeSpan estimatedTime)
        {
            this.Name = name;
            this.Description = description;
            this.FromInput = from;
            this.ToInput = to;
            this.TransportType = transportType;
            this.Distance = tourDistance;
            this.EstimatedTime = estimatedTime;
        }
        public Tour()
        { }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
