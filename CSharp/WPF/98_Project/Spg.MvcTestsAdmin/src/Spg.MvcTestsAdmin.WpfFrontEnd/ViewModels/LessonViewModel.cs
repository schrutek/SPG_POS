using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using Spg.MvcTestsAdmin.WpfFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Spg.MvcTestsAdmin.WpfFrontEnd.ViewModels
{
    public class LessonViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Wird aufgerufen, wenn das Binding aktualisiert werden soll.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Hält die Instanz der Service-Klasse
        /// </summary>
        private readonly ILessonService _lessonService;

        /// <summary>
        /// Der Service wird mittels DI instanziert.
        /// </summary>
        /// <remarks>
        /// TODO: Implementation
        /// Im Konstruktor werden auch Command Bindings für die Buttons
        /// erstellt und instanziert.
        /// 
        /// Hier kann die Action, die durchgeführt werden soll, direkt mitgegeben werden. 
        /// Bei längeren Methoden sollte aber die Methode im View Model als private definiert 
        /// werden und es wird hier einfach der Methodenname übergeben.
        /// 
        /// Das Erzeugen des RelayCommand sollte nicht direkt im getter des Binding Properties
        /// geschehen, da sonst immer eine neue Instanz erzeugt wird. Deswegen initialisieren wir
        /// hier vorher. 
        /// 
        /// Die CanExecute-Delegates machen wir hier von der Existenz des Parameters abhängig.
        /// </remarks>
        /// <param name="lessonService">Eine Implementierung von ILessonService</param>
        public LessonViewModel(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        /// <summary>
        /// Binding Property für alle Schulstunden
        /// </summary>
        public TaskNotificationHelper<IEnumerable<Lesson>> AllLessons
        {
            get => _allLessons;
            private set
            {
                _allLessons = value;
            }
        }
        private TaskNotificationHelper<IEnumerable<Lesson>> _allLessons;

        /// <summary>
        /// Binding Property für die selektierte Schulklasse
        /// </summary>
        /// <remarks>
        /// Achtung! value ist null wenn man in der Liste der Schulklassen eine andere
        /// Schulklasse auswählt, weil dann die Liste der Schulstunden neu geladen wird 
        /// und dann natürlich dann keine mehr selektiert ist. 
        /// "_currentLesson = value" == null!!
        /// </remarks>
        public Lesson CurrentLesson
        {
            get => _currentLesson;
            set
            {
                _currentLesson = value;
                ClassTeacher = _currentLesson?.L_TeacherNavigation;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClassTeacher)));
            }
        }
        private Lesson _currentLesson;

        /// <summary>
        /// Binding Property für den Klassenvorstand
        /// </summary>
        public Teacher ClassTeacher { get; set; }

        /// <summary>
        /// Lädt alle Stunden einer Klasse aus der DB.
        /// </summary>
        /// <remarks>
        /// Hier muss natürlich das Event PropertyChanged aufgerufen werden,
        /// damit das MainViewModel weiß dass der Ladevorgang abgeschlossen ist
        /// </remarks>
        public void GetAllLessonsMethod(string currentSchoolclassId)
        {
            _allLessons = new TaskNotificationHelper<IEnumerable<Lesson>>(
                _lessonService.GetAllBySchoolclassAsync(currentSchoolclassId));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AllLessons)));
        }
    }
}
