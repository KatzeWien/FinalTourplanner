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

namespace FinalTourplanner.ViewModel
{
    public class VMAddTour : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SaveSucceeded;
        public AllDataManagement dataManagement { get; }
        private string nameInput;
        private string descriptionInput;
        private string fromInput;
        private string toInput;
        private string transportTypeInput;
        private string tourDistanceInput;
        private string estimatedTimeInput;
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
        public VMAddTour(AllDataManagement dataManagement)
        {
            this.dataManagement = dataManagement;
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand AddTour
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
                    Tour tour = new Tour(NameInput, DescriptionInput, FromInput, ToInput, TransportTypeInput, distance, estimatedTime);
                    dataManagement.AddTour(tour);
                    dataManagement.GetAllTourNames();
                    SaveSucceeded?.Invoke(this, EventArgs.Empty);
                });
            }
        }
    }
}
