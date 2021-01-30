using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TaskPlanner.Model;

namespace TaskPlanner.ViewModel
{
    public class MainWindowViewModel
    {
        private readonly TaskContext _context = new TaskContext();

        public List<Subject> Subjects => _context
            .Subjects
            .OrderBy(s => s.Name).ToList();
        
        private Subject _currentSubject;
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
        public Task CurrentTask { get; set; }

        public ObservableCollection<Task> Tasks { get; } = new ObservableCollection<Task>();

        public MainWindowViewModel()
        {
            //SaveCommand = new RelayCommand(SaveToDb);
            SaveCommand = new RelayCommand(() =>
            {
                _context.SaveChanges();
            });

            NewCommand = new RelayCommand(() =>
            {
                Task newTask = new Task
                {
                    Start = CurrentTask.Start,
                    End = CurrentTask.End,
                    Grade = CurrentTask.Grade,
                    Subject = CurrentSubject
                };

                _context.Add(newTask);
                _context.SaveChanges();
                Tasks.Add(newTask);
            });

            DeleteCommand = new RelayCommand(() =>
            {
                _context.Tasks.Remove(CurrentTask);
                _context.SaveChanges();
                Tasks.Remove(CurrentTask);
            });

        }
        public ICommand SaveCommand { get; }
        public ICommand NewCommand { get; }
        public ICommand DeleteCommand { get; }
    }
}
