using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Spg.CodeFirstDemo._4BAIF.Domain.Model
{
    /// <summary>
    /// hat: SchoolclassNavigation ; TeacherNavigation ; PeriodNavigation
    /// </summary>
    public class Test
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(8)]
        public string Subject { get; set; } = string.Empty;

        public DateTime Date { get; set; }


        [Required]
        [MaxLength(8)]
        public string SchoolclassId { get; set; } = default!;

        public Schoolclass SchoolclassNavigation { get; set; } = default!;

        [Required]
        [MaxLength(8)]
        public string TeacherId { get; set; } = default!;

        public Teacher TeacherNavigation { get; set; } = default!;

        public long PeriodId { get; set; }

        public Period PeriodNavigation { get; set; } = default!;
    }
}
