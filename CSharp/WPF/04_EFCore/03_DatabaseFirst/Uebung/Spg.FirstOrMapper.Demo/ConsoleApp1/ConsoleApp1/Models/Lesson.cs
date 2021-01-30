using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ConsoleApp1.Models
{
    [Table("Lesson")]
    [Index(nameof(L_Hour), Name = "PeriodLesson")]
    [Index(nameof(L_Class), Name = "SchoolclassLesson")]
    [Index(nameof(L_Teacher), Name = "TeacherLesson")]
    [Index(nameof(L_Untis_ID), Name = "idx_L_Untis_ID")]
    public partial class Lesson
    {
        [Key]
        public long L_ID { get; set; }
        public long? L_Untis_ID { get; set; }
        [Required]
        [StringLength(8)]
        public string L_Class { get; set; }
        [Required]
        [StringLength(8)]
        public string L_Teacher { get; set; }
        [Required]
        [StringLength(8)]
        public string L_Subject { get; set; }
        [StringLength(8)]
        public string L_Room { get; set; }
        public long? L_Day { get; set; }
        public long? L_Hour { get; set; }

        [ForeignKey(nameof(L_Class))]
        [InverseProperty(nameof(Schoolclass.Lessons))]
        public virtual Schoolclass L_ClassNavigation { get; set; }
        [ForeignKey(nameof(L_Hour))]
        [InverseProperty(nameof(Period.Lessons))]
        public virtual Period L_HourNavigation { get; set; }
        [ForeignKey(nameof(L_Teacher))]
        [InverseProperty(nameof(Teacher.Lessons))]
        public virtual Teacher L_TeacherNavigation { get; set; }
    }
}
