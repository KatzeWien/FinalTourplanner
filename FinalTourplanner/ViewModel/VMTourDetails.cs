using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FinalTourplanner.Models;
using FinalTourplanner.Commands;
using FinalTourplanner.BL;
using System.Windows.Navigation;

namespace FinalTourplanner.ViewModel
{
    public class VMTourDetails : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SaveSucceeded;
        public AllDataManagement DataManagement { get; }
        public Tour Tour { get; }
        private string nameInput;
        private string descriptionInput;
        private string fromInput;
        private string toInput;
        private string transportTypeInput;
        private string tourDistanceInput;
        private string estimatedTimeInput;
        private string oldName;
        public string NameInput
        {
            get => nameInput;
            set
            {
                nameInput = value;
                OnPropertyChanged(nameof(NameInput));
            }
        }
        public string DescriptionInput
        {
            get => descriptionInput;
            set
            {
                descriptionInput = value;
                OnPropertyChanged(nameof(DescriptionInput));
            }
        }
        public string FromInput
        {
            get => fromInput;
            set
            {
                fromInput = value;
                OnPropertyChanged(nameof(FromInput));
            }
        }
        public string ToInput
        {
            get => toInput;
            set
            {
                toInput = value;
                OnPropertyChanged(nameof(ToInput));
            }
        }
        public string TransportTypeInput
        {
            get => transportTypeInput;
            set
            {
                transportTypeInput = value;
                OnPropertyChanged(nameof(TransportTypeInput));
            }
        }
        public string TourDistanceInput
        {
            get => tourDistanceInput;
            set
            {
                tourDistanceInput = value;
                OnPropertyChanged(nameof(TourDistanceInput));
            }
        }
        public string EstimatedTimeInput
        {
            get => estimatedTimeInput;
            set
            {
                estimatedTimeInput = value;
                OnPropertyChanged(nameof(EstimatedTimeInput));
            }
        }
        public VMTourDetails(AllDataManagement dataManagement, Tour tour)
        {
            this.DataManagement = dataManagement;
            this.Tour = tour;
            this.NameInput = Tour.Name;
            this.DescriptionInput = Tour.Description;
            this.FromInput = Tour.FromInput;
            this.ToInput = Tour.ToInput;
            this.TransportTypeInput = Tour.TransportType;
            this.TourDistanceInput = Tour.Distance.ToString();
            this.EstimatedTimeInput = Tour.EstimatedTime.ToString();
            this.oldName = Tour.Name;
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand DeleteTour
        {
            get
            {
                return new Command(obj =>
                {
                    DataManagement.DeleteTour(Tour);
                    DataManagement.GetAllTourNames();
                    SaveSucceeded?.Invoke(this, EventArgs.Empty);
                });
            }
        }

        public ICommand ChangeTour
        {
            get
            {
                return new Command(obj =>
                {
                    if (!int.TryParse(TourDistanceInput, out int distance))
                    {
                        MessageBox.Show("Only numbers are valid");
                        return;
                    }
                    TimeSpan estimatedTime;
                    if (EstimatedTimeInput.Contains(":"))
                    {
                        if (!TimeSpan.TryParse(EstimatedTimeInput, out estimatedTime))
                        {
                            MessageBox.Show("Only valid time hh:mm or hh!");
                            return;
                        }
                    }
                    else
                    {
                        if (int.TryParse(EstimatedTimeInput, out int hours))
                        {
                            estimatedTime = TimeSpan.FromHours(hours);
                        }
                        else
                        {
                            MessageBox.Show("Only valid time hh:mm or hh!");
                            return;
                        }
                    }
                    if (NameInput == "" || DescriptionInput == "" || FromInput == "" || ToInput == "" || TransportTypeInput == "")
                    {
                        MessageBox.Show("All fields needs inputs");
                        return;
                    }
                    Tour updatedTour = new Tour(NameInput, DescriptionInput, FromInput, ToInput, TransportTypeInput, distance, estimatedTime);
                    TourService tourService = new TourService();
                    tourService.UpdateTour(updatedTour, oldName);
                    DataManagement.GetAllTourNames();
                    SaveSucceeded?.Invoke(this, EventArgs.Empty);
                });
            }
        }
    }
}
