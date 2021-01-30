using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TaskPlaner.Model;

namespace TaskPlaner.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly TaskContext _dbContext = new TaskContext(); // immutable

        public List<Subject> Subjects => _dbContext
            .Subjects
            .OrderBy(c => c.Name).ToList();

        public Subject CurrentSubject 
        {
            get => _currentSubject;
            set
            {
                _currentSubject = value;

                Tasks.Clear();
                var tasks = _dbContext.Tasks.Where(c => c.Subject == _currentSubject).ToList();
                foreach (Task item in tasks)
                {
                    Tasks.Add(item);
                }
            }
        }
        private Subject _currentSubject;

        public event PropertyChangedEventHandler PropertyChanged;

        public Task CurrentTask
        {
            get=> _currentTask;
            set
            {
                _currentTask = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentTask)));
            }
        }
        private Task _currentTask;

        /// <summary>
        /// Die Observble Collection funktionieert wie ein Cache. Sie wird nur 
        /// einmal erstellt und dann befüllt und geleert. Sie existiert quasi 
        /// parallel zur DB, muss daher aber genau gepflegt werden. 
        /// Vorteil: Performanter  als immer alle Daten runterladen.
        /// </summary>
        public ObservableCollection<Task> Tasks { get; } = new System.Collections.ObjectModel.ObservableCollection<Task>();

        public ICommand NewCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand DeleteSubjectCommand { get; set; }

        public MainWindowViewModel()
        {
            NewCommand = new RelayCommand(
                (parameter) => NewExecuted(parameter),
                (parameter) => NewCanExecute(parameter)
            );

            SaveCommand = new RelayCommand(
                (parameter) => SaveExecuted(parameter),
                (parameter) => SaveCanExecute(parameter)
            );

            DeleteCommand = new RelayCommand(
                (parameter) => DeleteExecuted(parameter),
                (parameter) => DeleteCanExecute(parameter)
            );

            DeleteSubjectCommand = new RelayCommand(
                (parameter) => DeleteSubjectExecuted(parameter),
                (parameter) => DeleteSubjectCanExecute(parameter)
            );
        }

        public bool NewCanExecute(object parameter)
        {
            return CurrentSubject != null;
        }

        public void NewExecuted(object parameter)
        {
            CurrentTask = new Task() { Subject = CurrentSubject };

            //Task newTask = new Task()
            //{
            //    Start = CurrentTask.Start,
            //    End = CurrentTask.End,
            //    Grade = CurrentTask.Grade,
            //    Subject = CurrentSubject
            //};

            //_dbContext.Add(newTask);
            //_dbContext.SaveChanges();
            //Tasks.Add(newTask);

            //Tasks.Add(newTask);
        }

        public bool SaveCanExecute(object parameter)
        {
            return CurrentTask != null;
        }

        public void SaveExecuted(object parameter)
        {
            try
            {
                if (_dbContext.Entry(CurrentTask).State == EntityState.Detached)
                {
                    _dbContext.Add(CurrentTask);
                    _dbContext.SaveChanges();
                    Tasks.Add(CurrentTask);
                }
                else
                {
                    _dbContext.SaveChanges();
                }
            }
            catch (DbUpdateException ex)
            { 
                // TODO: Exception Handling
            }
        }

        public bool DeleteCanExecute(object parameter)
        {
            return CurrentTask != null;
        }

        public void DeleteExecuted(object parameter)
        {
            _dbContext.Tasks.Remove(CurrentTask);
            _dbContext.SaveChanges();

            Tasks.Remove(CurrentTask);
        }

        public bool DeleteSubjectCanExecute(object parameter)
        {
            return CurrentSubject != null;
        }

        public void DeleteSubjectExecuted(object parameter)
        {
            _dbContext.Subjects.Remove(CurrentSubject);
            _dbContext.SaveChanges();
            CurrentSubject = null;

            if (_currentSubject != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Subjects)));
            }

            Tasks.Remove(CurrentTask);
        }
    }
}