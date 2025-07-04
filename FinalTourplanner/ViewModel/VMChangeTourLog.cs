using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FinalTourplanner.BL;
using FinalTourplanner.Commands;
using FinalTourplanner.Models;

namespace FinalTourplanner.ViewModel
{
    public class VMChangeTourLog : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SaveSucceeded;
        public AllDataManagement DataManagement { get; }
        private TourLog choosenTour;
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
        public VMChangeTourLog(AllDataManagement dataManagement, TourLog choosenTour)
        {
            this.DataManagement = dataManagement;
            this.choosenTour = choosenTour;
            /*
            DateInput = dataManagement.Logs[indexOfTour].DateInput.ToString();
            CommentInput = dataManagement.Logs[indexOfTour].Comment;
            DifficultyInput = dataManagement.Logs[indexOfTour].Difficulty;
            TotalDistanceInput = dataManagement.Logs[indexOfTour].Distance.ToString();
            TotalTimeInput = dataManagement.Logs[indexOfTour].TotalTime.ToString();
            RatingInput = dataManagement.Logs[indexOfTour].Rating;*/
            DateInput = choosenTour.DateInput.ToString();
            CommentInput = choosenTour.Comment;
            DifficultyInput = choosenTour.Difficulty;
            TotalDistanceInput = choosenTour.Distance.ToString();
            TotalTimeInput = choosenTour.TotalTime.ToString();
            RatingInput = choosenTour.Rating;
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand ChangeTourLog
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
                    /*DataManagement.Logs[indexOfTour].DateInput = date;
                    DataManagement.Logs[indexOfTour].Comment = CommentInput;
                    DataManagement.Logs[indexOfTour].Difficulty = DifficultyInput;
                    DataManagement.Logs[indexOfTour].Distance = totalDistance;
                    DataManagement.Logs[indexOfTour].TotalTime = totalTime;
                    DataManagement.Logs[indexOfTour].Rating = RatingInput;*/
                    TourLogService tourLogService = new TourLogService();
                    TourLog tourLog = new TourLog(choosenTour.NameOfTour, date, CommentInput, DifficultyInput, totalDistance, totalTime, RatingInput);
                    tourLogService.UpdateTourLog(tourLog, choosenTour.Id);
                    DataManagement.GetAllTourLogs();
                    DataManagement.GetLogsBasedOnTourname(choosenTour.NameOfTour);
                    SaveSucceeded?.Invoke(this, EventArgs.Empty);
                });
            }
        }
    }
}
