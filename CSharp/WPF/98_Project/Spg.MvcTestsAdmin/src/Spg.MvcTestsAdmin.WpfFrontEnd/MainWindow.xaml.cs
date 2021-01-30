using Microsoft.Extensions.Logging;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using Spg.MvcTestsAdmin.Service.Services;
using Spg.MvcTestsAdmin.WpfFrontEnd.ViewModels;
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

namespace Spg.MvcTestsAdmin.WpfFrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Über den Konstruktor erhät das MainWindow sein MainViewModel.
        /// (Constructor Injection)
        /// </summary>
        /// <param name="dataContext"></param>
        public MainWindow(MainViewModel dataContext)
        {
            DataContext = dataContext;
            InitializeComponent();
        }
    }
}
