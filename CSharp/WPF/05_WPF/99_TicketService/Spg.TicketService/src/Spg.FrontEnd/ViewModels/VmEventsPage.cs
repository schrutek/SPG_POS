using Spg.Services.Dtos;
using Spg.Services.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Windows;
using System.Windows.Input;

namespace Spg.FrontEnd.ViewModels
{
    public class VmEventsPage : ViewModelBase
    {
        /// <summary>
        /// Binding-Property für die Lite der anzuzeigenden Events
        /// </summary>
        public List<EventDto> Events { get; set; }

        /// <summary>
        /// Binding-Property für die Lite der anzuzeigenden Shows, wenn ein Event ausgewählt wurde.
        /// Siehe <see cref="VmEventsPage.EventSelected"/>
        /// </summary>
        public IEnumerable<ShowDto> ShowsByEvent { get; set; }

        /// <summary>
        /// Binding-Property für das ausgewählte Event
        /// </summary>
        public EventDto EventSelected
        {
            get => _eventSelected;
            set
            {
                _eventSelected = value;
                GetAllShowsByEvent();
                OnPropertyChanged(nameof(EventSelected));
            }
        }
        private EventDto _eventSelected;

        /// <summary>
        /// Command-Binding-Property für den Load-Button
        /// </summary>
        public ICommand LoadEvents { get; private set; }

        /// <summary>
        /// Command-Binding-Property für den New-Button
        /// </summary>
        public ICommand CreateEvent { get; private set; }

        /// <summary>
        /// Command-Binding-Property für den Update-Button
        /// </summary>
        public ICommand UpdateEvent { get; private set; }

        /// <summary>
        /// Command-Binding-Property für den Delete-Button
        /// </summary>
        public ICommand DeleteEvent { get; private set; }

        /// <summary>
        /// Command-Binding-Property für den CreateShow-Button
        /// </summary>
        public ICommand CreateShow { get; private set; }

        /// <summary>
        /// Ruft <ode>InitCommands</ode> auf.
        /// </summary>
        public VmEventsPage()
        {
            InitCommands();

            GetAllEvents();
        }

        /// <summary>
        /// Command-Binding-Property ob der Load-Button aktiv ist, oder nicht. (Nur zu Demo-Zwecken)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool LoadEventsCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Diese Methode lädt nur alle Events, ohne sich vorher anzumelden. (Nur zu Demo-Zwecken)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private void LoadEventsExecuted()
        {
            try
            {
                GetAllEvents();
            }
            catch (AuthenticationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Command-Binding-Property ob der New-Button aktiv ist, oder nicht.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CreateEventCanExecute()
        {
            return true;
        }

        /// <summary>
        /// Die eingentliche Create-Logik. Hier ist noch nichts implementiert!
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private void CreateEventExecuted()
        {
            //TODO: Implementierung ...
        }

        /// <summary>
        /// Command-Binding-Property ob der Update-Button aktiv ist, oder nicht.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool UpdateEventCanExecute(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Die eingentliche Update-Logik. Hier ist noch nichts implementiert!
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private void UpdateEventExecuted(object parameter)
        {
            //TODO: Implementierung ...
        }

        /// <summary>
        /// Command-Binding-Property ob der Delete-Button aktiv ist, oder nicht.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool DeleteEventCanExecute(object parameter)
        {
            if (parameter != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Die eingentliche Delete-Logik. Hier ist noch nichts implementiert!
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private void DeleteEventExecuted(object parameter)
        {
            //TODO: Implementierung ...
        }

        /// <summary>
        /// Command-Binding-Property ob der New-Button aktiv ist, oder nicht.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CreateShowCanExecute(object parameter)
        {
            if (EventSelected != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Die eingentliche Create-Logik. Hier ist noch nichts implementiert!
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private void CreateShowExecuted(object parameter)
        {
            //TODO: Implementierung ...
        }

        /// <summary>
        /// Holt asynchron Event-Daten von der REST-API ab
        /// </summary>
        /// <returns></returns>
        private void GetAllEvents()
        {
            SetProperty("Events", (EventService.Instance.GetAllEvents()).ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetAllShowsByEvent()
        {
            SetProperty("ShowsByEvent", (EventService.Instance.GetShowsByEvent(_eventSelected.Id)).ToList());
        }

        /// <summary>
        /// Initialisiert das Command-Binding für die Buttons
        /// </summary>
        private void InitCommands()
        {
            LoadEvents = new RelayCommand(
                // Action für den Klick
                () => LoadEventsExecuted(),
                // Gibt an, wann der Button aktiv sein soll.
                () => LoadEventsCanExecute()
            );

            CreateEvent = new RelayCommand(
                // Action für den Klick
                () => CreateEventExecuted(),
                // Gibt an, wann der Button aktiv sein soll.
                () => CreateEventCanExecute()
            );

            UpdateEvent = new RelayCommand(
                // Action für den Klick
                (parameter) => UpdateEventExecuted(parameter),
                // Gibt an, wann der Button aktiv sein soll.
                (parameter) => UpdateEventCanExecute(parameter)
            );

            DeleteEvent = new RelayCommand(
                // Action für den Klick
                (parameter) => DeleteEventExecuted(parameter),
                // Gibt an, wann der Button aktiv sein soll.
                (parameter) => DeleteEventCanExecute(parameter)
            );

            CreateShow = new RelayCommand(
                // Action für den Klick
                (parameter) => CreateShowExecuted(parameter),
                // Gibt an, wann der Button aktiv sein soll.
                (parameter) => CreateShowCanExecute(parameter)
            );
        }
    }
}
