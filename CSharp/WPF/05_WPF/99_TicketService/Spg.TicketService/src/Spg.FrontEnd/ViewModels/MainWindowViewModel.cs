using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Spg.FrontEnd.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Aktuell ausgewählter Menüpunkt.
        /// </summary>
        public string ActiveMenuItem
        {
            get => _activeMenuItem;
            private set
            {
                if (_activeMenuItem != value)
                {
                    _activeMenuItem = value;
                    OnPropertyChanged(nameof(ActiveMenuItem));
                }
            }
        }
        private string _activeMenuItem = "Events";

        /// <summary>
        /// Command-Binding-Property für das Menü
        /// </summary>
        public ICommand ActivateMenuItem { get; private set; }

        /// <summary>
        /// Erstellt eine neue Instanz von <vode>MainWindowViewModel</vode>.
        /// </summary>
        public MainWindowViewModel()
        {
            ActivateMenuItem = new RelayCommand((property => ActiveMenuItem = property?.ToString()));
        }
    }
}
