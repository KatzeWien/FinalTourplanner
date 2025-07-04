using FinalTourplanner.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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

namespace FinalTourplanner.View
{
    /// <summary>
    /// Interaktionslogik für TourDetails.xaml
    /// </summary>
    public partial class TourDetails : Window
    {
        public TourDetails(AllDataManagement management, Tour tour)
        {
            InitializeComponent();
            var viewModel = new ViewModel.VMTourDetails(management, tour);
            viewModel.SaveSucceeded += (s, e) =>
            {
                this.DialogResult = true;
                this.Close();
            };
            this.DataContext = viewModel;
        }

        public void ButtonCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
