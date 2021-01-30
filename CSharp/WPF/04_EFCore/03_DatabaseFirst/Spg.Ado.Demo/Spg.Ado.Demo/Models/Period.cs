using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Spg.Ado.Demo.Models
{
    [Table("Period")]
    public partial class Period
    {
        public Period()
        {
            Lessons = new HashSet<Lesson>();
            Tests = new HashSet<Test>();
        }

        [Key]
        public long P_Nr { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime P_From { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime P_To { get; set; }

        [InverseProperty(nameof(Lesson.L_HourNavigation))]
        public virtual ICollection<Lesson> Lessons { get; set; }
        [InverseProperty(nameof(Test.TE_LessonNavigation))]
        public virtual ICollection<Test> Tests { get; set; }
    }
}
