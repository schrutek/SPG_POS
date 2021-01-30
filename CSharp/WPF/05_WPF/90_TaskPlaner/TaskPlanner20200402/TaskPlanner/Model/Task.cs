using System;

namespace TaskPlanner.Model
{
    public class Task
    {
        public int Id { get; set; }             // Wird automatisch zum PK und Autoincrement, da es ein int ist.
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int? Grade { get; set; }         // Grade darf NULL sein.
        public Subject Subject { get; set; }
    }

}
