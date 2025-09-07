using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FinalTourplanner.Models;
using FinalTourplanner.Commands;
using FinalTourplanner.View;
using FinalTourplanner.BL;

namespace FinalTourplanner.ViewModel
{
    public class VMTourlogWindow : INotifyPropertyChanged
    {
        public AllDataManagement DataManagement { get; }
        public ObservableCollection<string> TourNames { get; }
        public ObservableCollection<TourLog> SpecialLogs { get; set; }
        private string selectedItem;
        //private int selectedIndex;
        private TourLog choosenTourLog;
        public string SelectedItem
        {
            get => selectedItem; set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem)); DataManagement.GetLogsBasedOnTourname(SelectedItem);
            }
        }
        //public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; OnPropertyChanged(nameof(SelectedIndex)); } }
        public TourLog ChoosenTourLog { get => choosenTourLog; set { choosenTourLog = value; OnPropertyChanged(nameof(ChoosenTourLog)); } }
        public VMTourlogWindow(AllDataManagement dataManagement)
        {
            this.DataManagement = dataManagement;
            this.TourNames = DataManagement.TourNames;
            this.SpecialLogs = DataManagement.SpecialLogs;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand AddLogs
        {
            get
            {
                return new Command(obj =>
                {
                    if (SelectedItem != "")
                    {
                        AddTourLog addTour = new AddTourLog(DataManagement, SelectedItem);
                        addTour.ShowDialog();
                    }
                });
            }
        }

        public ICommand RemoveLogs
        {
            get
            {
                return new Command(obj =>
                {
                    /*if (SelectedIndex >= 0)
                    {
                        DataManagement.DeleteTourLogBasedOnIndex(SelectedIndex);
                        DataManagement.GetLogsBasedOnTourname(SelectedItem);
                    }*/
                    if (ChoosenTourLog != null)
                    {
                        //DataManagement.DeleteTourLogBasedOnIndex(SelectedIndex);
                        DataManagement.DeleteTourLog(ChoosenTourLog);
                        DataManagement.GetAllTourLogs();
                        DataManagement.GetLogsBasedOnTourname(SelectedItem);
                    }
                });
            }
        }

        public ICommand ChangeLog
        {
            get
            {
                return new Command(obj =>
                {
                    /*TourLog searchedRow = SpecialLogs[SelectedIndex];
                    int index = DataManagement.Logs.IndexOf(searchedRow);*/
                    ChangeTourLogs changeTourLogs = new ChangeTourLogs(DataManagement, ChoosenTourLog);
                    changeTourLogs.ShowDialog();
                });
            }
        }

        public ICommand CreateReport
        {
            get
            {
                return new Command(obj =>
                {
                    if (SelectedItem != null)
                    {
                        ReportService reportService = new ReportService();
                        reportService.CreateSummarizeReport(SelectedItem);
                    }
                });
            }
        }
    }
}
