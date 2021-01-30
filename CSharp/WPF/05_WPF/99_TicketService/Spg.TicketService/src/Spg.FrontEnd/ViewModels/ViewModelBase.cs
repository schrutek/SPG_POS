using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Spg.FrontEnd.ViewModels
{
    /// <summary>
    /// Stellt die Basisklasse für die ViewModel-Klassen dar.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Dieses Event aus dem Interface muss impelemntiert werden.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Um die View zu informieren, wenn sich ein Property ändert
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Setzt den Wert eines Properties und ruft das PropertyChanged Event auf, um die Bindings
        /// zu aktualisieren.
        /// </summary>
        /// <typeparam name="T">Typ des Properties.</typeparam>
        /// <param name="propertyName">Name des Properties. Wird mit nameof(Property) im Aufruf ermittelt.</param>
        /// <param name="value">Wert, auf den das Property gesetzt wird.</param>
        protected void SetProperty<T>(string propertyName, T value)
        {
            PropertyInfo prop = GetType().GetProperty(propertyName);
            prop.SetValue(this, value);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
