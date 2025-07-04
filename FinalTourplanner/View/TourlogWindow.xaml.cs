using FinalTourplanner.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalTourplanner.View
{
    /// <summary>
    /// Interaktionslogik für TourlogWindow.xaml
    /// </summary>
    public partial class TourlogWindow : Page
    {
        private string tourname = "";
        public AllDataManagement dataManagement;
        public TourlogWindow(AllDataManagement dataManagement)
        {
            InitializeComponent();
            var viewModel = new ViewModel.VMTourlogWindow(dataManagement);
            DataContext = viewModel;
            this.dataManagement = dataManagement;
        }


        private void ButtonDeleteLog_Click(object sender, RoutedEventArgs e)
        {
            var listview = logOutput.FindName("logView") as ListView;
            int indexLog = listview.SelectedIndex;
            if (indexLog >= 0)
            {
                dataManagement.DeleteTourLogBasedOnIndex(indexLog);
                dataManagement.GetLogsBasedOnTourname(tourname);
            }
        }

        /*private void ButtonChangeLog_Click(object sender, RoutedEventArgs e)
        {
            var sharedContext = DataContext as AllDataManagement;
            var listview = logOutput.FindName("logView") as ListView;
            int indexLog = listview.SelectedIndex;
            if (indexLog >= 0)
            {
                ChangeTourLogs changeTourLogs = new ChangeTourLogs();
                changeTourLogs.ShowDialog();
            }
        }*/
    }
}
