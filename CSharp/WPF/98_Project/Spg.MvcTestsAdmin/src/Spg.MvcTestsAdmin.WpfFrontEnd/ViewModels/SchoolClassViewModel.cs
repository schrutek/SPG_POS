using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using Spg.MvcTestsAdmin.WpfFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Spg.MvcTestsAdmin.WpfFrontEnd.ViewModels
{
    public class SchoolClassViewModel : ViewModelBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Wird aufgerufen, wenn das Binding aktualisiert werden soll.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Hält die Instanz der Service-Klasse
        /// </summary>
        private readonly ISchoolclassService _schoolclassService;

        /// <summary>
        /// Die benötigten Services werden mittels Constructor Injection übergeben.
        /// </summary>
        /// <remarks>
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
        /// <param name="schoolclassService">Eine Implementierung von ISchoolclassService</param>
        public SchoolClassViewModel(ISchoolclassService schoolclassService)
        {
            _schoolclassService = schoolclassService;

            GetAllSchoolClassesMethod();

            InsertSchoolClassCommand = new DelegateCommand(InsertSchoolClassExecuted, () =>
            {
                return true;
            });
            UpdateSchoolClassCommand = new DelegateCommand(UpdateSchoolClassExecuted, (p) =>
            {
                return p != null ? true : false;
            });
            DeleteSchoolClassCommand = new DelegateCommand(DeleteSchoolClassExecuted, (p) =>
            {
                return p != null ? true : false;
            });
        }

        /// <summary>
        /// Command für Insert der Schulklasse
        /// </summary>
        public ICommand InsertSchoolClassCommand { get; }

        /// <summary>
        /// Command für Update der Schulklasse
        /// </summary>
        public ICommand UpdateSchoolClassCommand { get; }

        /// <summary>
        /// Command für Delete der Schulklasse
        /// </summary>
        public ICommand DeleteSchoolClassCommand { get; }

        /// <summary>
        /// Binding Property für alle Schulklassen
        /// </summary>
        public TaskNotificationHelper<IEnumerable<Schoolclass>> AllSchoolClasses
        {
            get
            {
                return _allSchoolClasses;
            }
            set
            {
                _allSchoolClasses = value;
            }
        }
        private TaskNotificationHelper<IEnumerable<Schoolclass>> _allSchoolClasses;

        /// <summary>
        /// Binding Property für die selektierte Schulklasse
        /// </summary>
        public Schoolclass CurrentSchoolclass
        {
            get => _currentSchoolclass;
            set
            {
                _currentSchoolclass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SchoolClassesLoaded"));
            }
        }
        private Schoolclass _currentSchoolclass;

        /// <summary>
        /// Lädt alle Schulklassen asynchron aus der DB.
        /// </summary>
        /// <remarks>
        /// Achtung! Im Service-Layer laufen alle Methoden asynchron. Es sollte im 
        /// ViewModel oder sogar darunter niemals das Property Result der Klasse Task aufgerufen
        /// werden. Das sollte immer an oberster Ebene passieren, also in diesem Fall im XAML.
        /// Die Klasse TaskNotificationHelper ist hierfür sehr hilfreich. Sie beobachtet den Task
        /// und lefert das Ergebnis wenn er fertig ist. Unsere App bleibt allso bedienbar,
        /// auch bei Methoden mit langen Laufzeiten.
        /// Achtung! Man muss dann natürlich dafür sorgen, dass der User nirgends klicken kann
        /// wo er nicht klicken darf, solange Ladevorgänge nicht abgeschlossen sind.
        /// Ein guter Link dazu:
        /// https://docs.microsoft.com/en-us/archive/msdn-magazine/2014/march/async-programming-patterns-for-asynchronous-mvvm-applications-data-binding
        /// Klener Tipp am Rande:
        /// Werden asynchrone Methoden in einem Konstruktor aufgerufen
        /// (eine Konstruktor kann niemals asyc sein), löst man dies am besten mit einer
        /// Factory-Methode.
        /// </remarks>
        public void GetAllSchoolClassesMethod()
        {
            _allSchoolClasses = new TaskNotificationHelper<IEnumerable<Schoolclass>>(
                _schoolclassService.GetAllAsync());
        }

        /// <summary>
        /// Neue Schulklasse hinzufügen
        /// </summary>
        private void InsertSchoolClassExecuted()
        {
            //TODO: Implementation using: 
            //TaskNotificationHelper<IEnumerable<Schoolclass>>(_schoolclassService.CreateAsync())
        }

        /// <summary>
        /// Ausgewählte Schulklasse ändern
        /// </summary>
        /// <param name="parameter">Die gewählte Klasse<</param>
        private void UpdateSchoolClassExecuted(object parameter)
        {
            //TODO: Implementation using: 
            //TaskNotificationHelper<IEnumerable<Schoolclass>>(_schoolclassService.EditAsync())
        }

        /// <summary>
        /// Ausgewählte Schulklasse löschen
        /// </summary>
        /// <param name="parameter">Die gewählte Klasse</param>
        private void DeleteSchoolClassExecuted(object parameter)
        {
            //TODO: Implementation using: 
            //TaskNotificationHelper<IEnumerable<Schoolclass>>(_schoolclassService.DeleteAsync())
        }
    }
}
