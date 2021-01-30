﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelDemoApp.Model;

namespace ViewModelDemoApp.ViewModels
{
    /// <summary>
    /// Viewmodel für MainWindow
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {

        public enum State
        {
            Filtered  = 0,
            Unfiltered = 1
        }

        /// <summary>
        /// Instanz der Personendatenbank.
        /// </summary>
        private readonly PersonDb personDb = PersonDb.FromMockup();
        /// <summary>
        /// Aktuell angezeigter Index der Personenliste.
        /// </summary>
        private int currentIndex = 0;
        /// <summary>
        /// Aktuelle Person.
        /// </summary>
        private Person currentPerson;

        /// <summary>
        /// Konstruktor mit Initialisierungen.
        /// </summary>
        public MainViewModel()
        {
            InitCommands();

            _persons = personDb.Persons.ToList();
            currentPerson = Persons[currentIndex];

            CurrentState = State.Unfiltered;
        }

        /// <summary>
        /// Wird aufgerufen, wenn das Binding aktualisiert werden soll. Das muss beim Schreiben
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Binding Property für alle Felder, die Daten der aktuellen Person ausgeben. Wichtig ist
        /// das Aufrufen von PropertyChanged im setter, da sonst die Bindings nicht aktualisiert
        /// werden!
        /// </summary>
        public Person CurrentPerson
        {
            get => currentPerson;
            // set muss wegen der Liste public sein!
            set
            {
                // Nur die Bindings aktualisieren, wenn sich etwas ändert.
                if (value != currentPerson)
                {
                    currentPerson = value;
                    // Damit die Textfelder neu eingelesen werden, müssen wir mitteilen, dass
                    // CurrentPerson sich geändert hat.
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentPerson)));
                }
            }
        }
        public string SearchText { get; set; }

        public string GoButtonCaption { get; set; }

        public string Status { get; set; }

        public ICommand SearchCommand { get; private set; }
        /// <summary>
        /// Binding Property für den Next Button.
        /// </summary>
        public ICommand NextCommand { get; private set; }
        /// <summary>
        /// Liest alle Personen aus der Datenbank in den Speicher. Sonst können wir nicht mit dem
        /// Index arbeiten, da Collections keinen Indexzugriff ermöglichen.
        /// </summary>
        public IList<Person> Persons 
        { 
            get 
            { 
                return _persons; 
            }
            private set
            {
                _persons = value;
            } 
        }
        private IList<Person> _persons;

        public State CurrentState
        {
            get =>_currentState;
            set
            {
                _currentState = value;
                if (_currentState == State.Filtered)
                {
                    GoButtonCaption = "Filter entfernen";
                }
                if (_currentState == State.Unfiltered)
                {
                    GoButtonCaption = "Go";
                }
                Search();
                Status = $"{_persons.Count} Personen gefunden";

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(GoButtonCaption)));
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Status)));
                }
            }
        }
        private State _currentState = State.Unfiltered;

        /// <summary>
        /// Binding Property für den Previous Button
        /// </summary>
        public ICommand PrevCommand { get; private set; }
        /// <summary>
        /// Binding Property für den Generate Person Button.
        /// </summary>
        public ICommand GeneratePersonCommand { get; private set; }
        /// <summary>
        /// Binding Property für den Delete Person Button.
        /// </summary>
        public ICommand DeletePersonCommand { get; private set; }
        /// <summary>
        /// Initialisiert die Properties für die Buttons. Hier kann die Action, die durchgeführt
        /// werden soll, direkt mitgegeben werden. Bei längeren Methoden sollte aber die Methode
        /// im View Model als private definiert werden und es wird hier einfach der Methodenname
        /// übergeben.
        /// Das Erzeugen des RelayCommand sollte nicht direkt im getter des Binding Properties
        /// geschehen, da sonst immer eine neue Instanz erzeugt wird. Deswegen initialisieren wir
        /// hier vorher.
        /// </summary>
        private void InitCommands()
        {
            NextCommand = new RelayCommand(
                // Action für den Klick
                () =>
                {
                    CurrentPerson = Persons[++currentIndex];
                },
                // Gibt an, wann der Button aktiv sein soll.
                () => currentIndex < Persons.Count - 1
            );

            PrevCommand = new RelayCommand(
                () =>
                {
                    CurrentPerson = Persons[--currentIndex];
                },
                () => currentIndex > 0
            );

            GeneratePersonCommand = new RelayCommand(
                () =>
                {
                    Random rnd = new Random();
                    personDb.Persons.Add(new Person
                    {
                        Firstname = $"Vorname{rnd.Next(1000, 9999 + 1)}",
                        Lastname = $"Zuname{rnd.Next(1000, 9999 + 1)}",
                        Sex = rnd.Next(0, 2) == 0 ? Sex.Male : Sex.Female,
                        DateOfBirth = DateTime.Now.AddDays(-rnd.Next(18 * 365, 25 * 365))
                    });
                    // Bewirkt das neue Auslesen der Liste.
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Persons)));
                }
            );

            DeletePersonCommand = new RelayCommand(
                () =>
                {
                    personDb.Persons.Remove(CurrentPerson);
                    // Bewirkt das neue Auslesen der Liste.
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Persons)));
                }
            );

            SearchCommand = new RelayCommand(
                () => 
                {
                    if (CurrentState == State.Filtered)
                    {
                        CurrentState = State.Unfiltered;
                    }
                    else
                    {
                        CurrentState = State.Filtered;
                    }
                }
            );
        }

        private void Search()
        {
            if (!String.IsNullOrEmpty(SearchText))
            {
                _persons = personDb.Persons
                    .Where(c => c.Lastname.ToLower()
                    .StartsWith(SearchText.ToLower()))
                    .ToList();
                if (_persons.Count >= 1)
                {
                    CurrentPerson = _persons[0];
                }
                SearchText = String.Empty;
            }
            else
            {
                _persons = personDb.Persons.ToList();
            }
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Persons)));
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SearchText)));
            }
        }
    }
}
