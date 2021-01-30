using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ConsoleApp1.Models
{
    [Table("Schoolclass")]
    [Index(nameof(C_ClassTeacher), Name = "TeacherSchoolclass")]
    public partial class Schoolclass
    {
        public Schoolclass()
        {
            Lessons = new HashSet<Lesson>();
            Pupils = new HashSet<Pupil>();
            Tests = new HashSet<Test>();
        }

        [Key]
        [StringLength(8)]
        public string C_ID { get; set; }
        [Required]
        [StringLength(8)]
        public string C_Department { get; set; }
        [StringLength(8)]
        public string C_ClassTeacher { get; set; }

        [ForeignKey(nameof(C_ClassTeacher))]
        [InverseProperty(nameof(Teacher.Schoolclasses))]
        public virtual Teacher C_ClassTeacherNavigation { get; set; }
        [InverseProperty(nameof(Lesson.L_ClassNavigation))]
        public virtual ICollection<Lesson> Lessons { get; set; }
        [InverseProperty(nameof(Pupil.P_ClassNavigation))]
        public virtual ICollection<Pupil> Pupils { get; set; }
        [InverseProperty(nameof(Test.TE_ClassNavigation))]
        public virtual ICollection<Test> Tests { get; set; }
    }
}
