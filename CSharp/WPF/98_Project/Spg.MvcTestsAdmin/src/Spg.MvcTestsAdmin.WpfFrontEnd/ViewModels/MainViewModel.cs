using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using Spg.MvcTestsAdmin.WpfFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Spg.MvcTestsAdmin.WpfFrontEnd.ViewModels
{
    /// <summary>
    /// ViewModel für das MainWindow. Das View Model wird 
    /// mittels Dependency Injection instanziert.
    /// </summary>
    /// <remarks>
    /// Dieses ViewModel verwendet 2 weitere ViewModels, die ebenfalls über DI 
    /// instanziert werden. Der grund dafür ist Reusability. Das MainWindow listet
    /// sowohol die Klassen als auch deren Stunden und Lehrer. Aber man könnte das
    /// SchoolClassViewModel in einem anderen Window, das nur die Klassen bearbeitet
    /// wiederverwenden. Daher die Trennung.
    /// </remarks>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Das ViemModel für die Schulklassen
        /// </summary>
        public SchoolClassViewModel SchoolClassViewModel { get; set; }

        /// <summary>
        /// Das ViewMOdel für die Schulstunden
        /// </summary>
        public LessonViewModel LessonViewModel { get; set; }

        /// <summary>
        /// Die Services werden mittels Constructor Injection übergeben.
        /// </summary>
        /// <remarks>
        /// Hier wird auch auf das Event PropertyChanged aus dem Interface
        /// INotifyPropertyChanged attached. Dadurch bekommt dieses ViewModel
        /// eine Info wenn sich in den anderen beiden ViewModels etwas geändert hat.
        /// </remarks>
        /// <param name="schoolClassViewModel">Das ViewModel für die Schulklassen</param>
        /// <param name="lessonViewModel">Das ViewModel für die Schulstunden</param>
        public MainViewModel(
            SchoolClassViewModel schoolClassViewModel,
            LessonViewModel lessonViewModel)
        {
            SchoolClassViewModel = schoolClassViewModel;
            LessonViewModel = lessonViewModel;

            SchoolClassViewModel.PropertyChanged += SchoolClassViewModel_PropertyChanged;
        }

        /// <summary>
        /// Wird vom SchoolClassViewModel gefeuert wenn eine Schulklasse ausewählt wurde.
        /// Danach können die dazugehörigen Schulstunden geladen werden.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SchoolClassViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.ToLower() == "schoolclassesloaded")
            {
                if (SchoolClassViewModel.CurrentSchoolclass != null)
                {
                    LessonViewModel.GetAllLessonsMethod(SchoolClassViewModel.CurrentSchoolclass.C_ID);
                }
            }
        }
    }
}