using System;
using System.Collections.Generic;
using System.Text;

namespace TaskPlaner.Model
{
    public class Task
    {
        public int Id { get; set; } // wird automatisch zum PK und AutoIncrement wird angelegt (weil int)

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int? Grade
        {
            get => _grade;
            set => _grade = (value == null) || (value >= 1 && value <= 5) ?
                    value :
                    throw new ArgumentException($"Ungültige Note: {value}");
        }
        private int? _grade;

        public Subject Subject { get; set; }
    }
}
