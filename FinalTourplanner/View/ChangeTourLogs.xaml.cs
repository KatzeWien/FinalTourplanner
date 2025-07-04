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
using System.Windows.Shapes;
using FinalTourplanner.Models;
using FinalTourplanner.ViewModel;

namespace FinalTourplanner.View
{
    /// <summary>
    /// Interaktionslogik für ChangeTourLogs.xaml
    /// </summary>
    public partial class ChangeTourLogs : Window
    {
        public ChangeTourLogs(AllDataManagement dataManagement, TourLog tourLog)
        {
            InitializeComponent();
            var viewModel = new ViewModel.VMChangeTourLog(dataManagement, tourLog);
            viewModel.SaveSucceeded += (s, e) =>
            {
                this.DialogResult = true;
                this.Close();
            };
            this.DataContext = viewModel;
        }
    }
}
