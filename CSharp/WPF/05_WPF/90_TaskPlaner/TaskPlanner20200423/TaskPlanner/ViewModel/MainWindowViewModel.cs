using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TaskPlanner.Model;

namespace TaskPlanner.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly TaskContext _context = new TaskContext();

        public List<Subject> Subjects => _context
            .Subjects
            .OrderBy(s => s.Name).ToList();

        private Subject _currentSubject;

        public event PropertyChangedEventHandler PropertyChanged;

        public Subject CurrentSubject
        {
            get => _currentSubject;
            set
            {
                _currentSubject = value;
                Tasks.Clear();
                var tasks = _context.Tasks.Where(t => t.Subject == _currentSubject).ToList();
                foreach (var t in tasks) { Tasks.Add(t); }
            }
        }

        private Task _currentTask;
        public Task CurrentTask
        {
            get => _currentTask;
            set
            {
                _currentTask = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentTask)));
            }
        }

        public ObservableCollection<Task> Tasks { get; } = new ObservableCollection<Task>();

        public MainWindowViewModel()
        {
            SaveCommand = new RelayCommand(() =>
            {
                try
                {
                    if (_context.Entry(CurrentTask).State == EntityState.Detached)
                    {
                        _context.Add(CurrentTask);
                        _context.SaveChanges();
                        Tasks.Add(CurrentTask);
                    }
                    else
                    {
                        _context.SaveChanges();
                    }
                }
                catch (DbUpdateException)
                {
                    // TODO: Exception Handling

                }

            }, () => CurrentTask != null);

            NewCommand = new RelayCommand(() =>
            {
                CurrentTask = new Task { Subject = CurrentSubject };
            }, () => CurrentSubject != null);

            DeleteCommand = new RelayCommand(() =>
            {
                try
                {
                    _context.Tasks.Remove(CurrentTask);
                    _context.SaveChanges();
                    Tasks.Remove(CurrentTask);
                }
                catch (DbUpdateException)
                {
                    // TODO: Exception Handling
                }
            }, () => CurrentTask != null);

            DeleteSubjectCommand = new RelayCommand(() =>
            {
                _context.Subjects.Remove(_currentSubject);
                _context.SaveChanges();
                CurrentSubject = null;
                // Unsauber
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Subjects)));
                // Besser: Für Subjects auch eine ObservableCollection, die
                // 1) Im Konstruktor befüllt wird
                // 2) Hier CurrentSubject entfernt wird.
            }, () => CurrentSubject != null);

        }
        public ICommand SaveCommand { get; }
        public ICommand NewCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand DeleteSubjectCommand { get; set; }
    }
}
