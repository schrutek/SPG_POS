using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskPlanner.Model;

namespace TaskPlanner.ViewModel
{
    public class MainWindowViewModel
    {
        private readonly TaskContext _context = new TaskContext();

        public List<Subject> Subjects => _context.Subjects.Include(s => s.Tasks).OrderBy(s => s.Name).ToList();
        public Subject CurrentSubject { get; set; }
    }
}
