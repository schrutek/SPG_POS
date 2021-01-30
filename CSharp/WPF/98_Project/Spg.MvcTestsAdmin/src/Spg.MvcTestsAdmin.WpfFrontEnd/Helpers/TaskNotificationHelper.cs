using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.WpfFrontEnd.Helpers
{
    /// <summary>
    /// Beobachtet einen asynchronen Aufruf (Task) und meldet des Status des Tasks
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public sealed class TaskNotificationHelper<TResult> : INotifyPropertyChanged
    {
        /// <summary>
        /// Gibt den laufenden Task zurück.
        /// </summary>
        public Task<TResult> Task { get; private set; }

        /// <summary>
        /// Im Konstruktor wird ein Task übergeben, 
        /// also jede Methode die GetAwaiter() implementiert
        /// </summary>
        /// <param name="task"></param>
        public TaskNotificationHelper(Task<TResult> task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                var _ = WatchTaskAsync(task);
            }
        }

        /// <summary>
        /// Hier wird der Task beobachtet
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch
            { }

            var propertyChanged = PropertyChanged;

            if (propertyChanged == null)
                return;
            
            propertyChanged(this, new PropertyChangedEventArgs("Status"));
            propertyChanged(this, new PropertyChangedEventArgs("IsCompleted"));
            propertyChanged(this, new PropertyChangedEventArgs("IsNotCompleted"));
            
            if (task.IsCanceled)
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsCanceled"));
            }
            else if (task.IsFaulted)
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsFaulted"));
                propertyChanged(this, new PropertyChangedEventArgs("Exception"));
                propertyChanged(this, new PropertyChangedEventArgs("InnerException"));
                propertyChanged(this, new PropertyChangedEventArgs("ErrorMessage"));
            }
            else
            {
                propertyChanged(this, new PropertyChangedEventArgs("IsSuccessfullyCompleted"));
                propertyChanged(this, new PropertyChangedEventArgs("Result"));
            }
        }

        public TResult Result
        {
            get
            {
                return (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(TResult);
            }
        }

        public TaskStatus Status { get { return Task.Status; } }
        public bool IsCompleted { get { return Task.IsCompleted; } }
        public bool IsNotCompleted { get { return !Task.IsCompleted; } }
        public bool IsSuccessfullyCompleted
        {
            get
            {
                return Task.Status == TaskStatus.RanToCompletion;
            }
        }

        public bool IsCanceled { get { return Task.IsCanceled; } }
        public bool IsFaulted { get { return Task.IsFaulted; } }
        public AggregateException Exception { get { return Task.Exception; } }
        public Exception InnerException
        {
            get
            {
                return (Exception == null) ? null : Exception.InnerException;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return (InnerException == null) ? null : InnerException.Message;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
