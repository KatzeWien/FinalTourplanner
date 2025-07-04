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
using FinalTourplanner.ViewModel;

namespace FinalTourplanner.View
{
    /// <summary>
    /// Interaktionslogik für AddTour.xaml
    /// </summary>
    public partial class AddTour : Window
    {
        public AddTour(AllDataManagement management)
        {
            InitializeComponent();
            var viewModel = new ViewModel.VMAddTour(management);
            viewModel.SaveSucceeded += (s, e) =>
            {
                this.DialogResult = true;
                this.Close();
            };
            this.DataContext = viewModel;
        }
    }
}
