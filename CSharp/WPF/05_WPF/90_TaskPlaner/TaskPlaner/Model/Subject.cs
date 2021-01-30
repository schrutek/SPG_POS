using System;
using System.Collections.Generic;
using System.Text;

namespace TaskPlaner.Model
{
    public class Subject
    {
        public string Nr { get; set; }

        public string Name { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
