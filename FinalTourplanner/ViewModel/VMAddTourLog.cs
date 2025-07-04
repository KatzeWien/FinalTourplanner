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
    public class VMAddTourLog : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SaveSucceeded;
        public AllDataManagement dataManagement { get; }
        private string tourName;
        private string dateInput;
        private string commentInput;
        private string difficultyInput;
        private string totalDistanceInput;
        private string totalTimeInput;
        private string ratingInput;
        public string DateInput { get => dateInput; set { dateInput = value; OnPropertyChanged(nameof(DateInput)); } }
        public string CommentInput { get => commentInput; set { commentInput = value; OnPropertyChanged(nameof(CommentInput)); } }
        public string DifficultyInput { get => difficultyInput; set { difficultyInput = value; OnPropertyChanged(nameof(DifficultyInput)); } }
        public string TotalDistanceInput { get => totalDistanceInput; set { totalDistanceInput = value; OnPropertyChanged(nameof(TotalDistanceInput)); } }
        public string TotalTimeInput { get => totalTimeInput; set { totalTimeInput = value; OnPropertyChanged(nameof(TotalTimeInput)); } }
        public string RatingInput { get => ratingInput; set { ratingInput = value; OnPropertyChanged(nameof(RatingInput)); } }


        public VMAddTourLog(AllDataManagement management, string tourName)
        {
            this.dataManagement = management;
            this.tourName = tourName;
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand AddTourLog
        {
            get
            {
                return new Command(obj =>
                {
                    int totalDistance;
                    TimeSpan totalTime;
                    DateTime date;
                    if (!int.TryParse(TotalDistanceInput, out totalDistance))
                    {
                        MessageBox.Show("Only numbers are valid");
                        return;
                    }
                    if (TotalTimeInput.Contains(":"))
                    {
                        if (!TimeSpan.TryParse(TotalTimeInput, out totalTime))
                        {
                            MessageBox.Show("Only valid time hh:mm or hh!");
                            return;
                        }
                    }
                    else
                    {
                        if (int.TryParse(TotalTimeInput, out int hours))
                        {
                            totalTime = TimeSpan.FromHours(hours);
                        }
                        else
                        {
                            MessageBox.Show("Only valid time hh:mm or hh!");
                            return;
                        }
                    }
                    if (!DateTime.TryParse(DateInput, out date))
                    {
                        MessageBox.Show("Only valid date");
                        return;
                    }
                    else
                    {
                        date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
                    }
                    if (CommentInput == "" || DifficultyInput == "" || RatingInput == "")
                    {
                        MessageBox.Show("All fields needs inputs");
                        return;
                    }
                    TourLog tourlog = new TourLog(this.tourName, date, CommentInput, DifficultyInput, totalDistance, totalTime, RatingInput);
                    dataManagement.AddTourLog(tourlog);
                    dataManagement.GetLogsBasedOnTourname(this.tourName);
                    dataManagement.GetAllTourLogs();
                    SaveSucceeded?.Invoke(this, EventArgs.Empty);
                });
            }
        }
    }
}
