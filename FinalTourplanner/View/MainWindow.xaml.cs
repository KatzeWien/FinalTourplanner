﻿using FinalTourplanner.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalTourplanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AllDataManagement AllDataManagement { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            AllDataManagement = new AllDataManagement();
            MainFrame.DataContext = AllDataManagement;
        }
    }
}