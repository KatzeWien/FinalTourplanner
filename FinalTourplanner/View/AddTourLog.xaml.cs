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
    /// Interaktionslogik für AddTourLog.xaml
    /// </summary>
    public partial class AddTourLog : Window
    {
        public AddTourLog(AllDataManagement management, string tourName)
        {
            InitializeComponent();
            var viewModel = new ViewModel.VMAddTourLog(management, tourName);
            viewModel.SaveSucceeded += (s, e) =>
            {
                this.DialogResult = true;
                this.Close();
            };
            this.DataContext = viewModel;
        }
    }
}
