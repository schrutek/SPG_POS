using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ConsoleApp1.Models
{
    [Table("Teacher")]
    [Index(nameof(T_Account), Name = "idx_T_Account", IsUnique = true)]
    public partial class Teacher
    {
        public Teacher()
        {
            Lessons = new HashSet<Lesson>();
            Schoolclasses = new HashSet<Schoolclass>();
            Tests = new HashSet<Test>();
        }

        [Key]
        [StringLength(8)]
        public string T_ID { get; set; }
        [Required]
        [StringLength(100)]
        public string T_Lastname { get; set; }
        [Required]
        [StringLength(100)]
        public string T_Firstname { get; set; }
        [StringLength(255)]
        public string T_Email { get; set; }
        [Required]
        [StringLength(100)]
        public string T_Account { get; set; }
        public Guid T_CatAccountStateId { get; set; }

        [ForeignKey(nameof(T_CatAccountStateId))]
        [InverseProperty(nameof(CatAccountState.Teachers))]
        public virtual CatAccountState T_CatAccountState { get; set; }
        [InverseProperty(nameof(Lesson.L_TeacherNavigation))]
        public virtual ICollection<Lesson> Lessons { get; set; }
        [InverseProperty(nameof(Schoolclass.C_ClassTeacherNavigation))]
        public virtual ICollection<Schoolclass> Schoolclasses { get; set; }
        [InverseProperty(nameof(Test.TE_TeacherNavigation))]
        public virtual ICollection<Test> Tests { get; set; }
    }
}
