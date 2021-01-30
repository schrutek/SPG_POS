using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.CodeFirstDemo._4BAIF.Domain.Model
{
    public enum DayOfWeek { Monday = 1, Tuesday, Wednesday, Thursday, Friday }

    /// <summary>
    /// hat: PeriodNavigation ; TeacherNavigation ; SchoolclassNavigation
    /// </summary>
    public class Lesson
    {
        public int Id { get; init; }

        public string Subject { get; set; } = string.Empty;

        public string Room { get; set; } = string.Empty;

        public DayOfWeek? Day { get; set; }



        public long PeriodId { get; set; }

        public Period PeriodNavigation { get; set; } = default!;

        public string TeacherId { get; set; } = default!;

        public Teacher TeacherNavigation { get; set; } = default!;

        public string SchoolclassId { get; set; } = default!;

        public Schoolclass SchoolclassNavigation { get; set; } = default!;
    }
}
