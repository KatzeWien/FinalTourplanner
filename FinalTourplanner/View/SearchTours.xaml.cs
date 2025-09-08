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
    /// Interaktionslogik für SearchTours.xaml
    /// </summary>
    public partial class SearchTours : UserControl
    {

        //für search zum funktionieren
        public SearchTours()
        {
            InitializeComponent();
        }
        public void Init(AllDataManagement management)
        {
            DataContext = new VMSearchTours(management);
        }
    }
}
