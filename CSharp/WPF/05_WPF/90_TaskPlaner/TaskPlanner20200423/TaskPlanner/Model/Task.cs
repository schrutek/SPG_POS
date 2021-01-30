using System;

namespace TaskPlanner.Model
{
    public class Task
    {
        public int Id { get; set; }             // Wird automatisch zum PK und Autoincrement, da es ein int ist.
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        private int? _grade;                   // Grade darf NULL sein.
        public int? Grade
        {
            get => _grade;
            set => _grade = value == null || (value >= 1 && value <= 5) ?
                    value :
                    throw new ArgumentException($"Ungültige Note: {value}");
        }
        public Subject Subject { get; set; }
    }

}
