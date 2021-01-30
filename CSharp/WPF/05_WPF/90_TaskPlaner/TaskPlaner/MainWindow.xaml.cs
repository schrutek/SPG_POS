﻿using System;
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
using TaskPlaner.Model;

namespace TaskPlaner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            using (TaskContext db = new TaskContext())
            {
                // muss natürlich zuerst aufgerufen werden. aber nur einmal, 
                // sonst wird die db bei jedem Start gelöscht, erstellt und initial geseedet.
                //db.Database.EnsureDeleted(); 

                // Seeden nur, wenn die DB neu erstellt wurde
                if (db.Database.EnsureCreated())
                {
                    db.Seed();
                }
            }
            
            InitializeComponent();
        }
    }
}
