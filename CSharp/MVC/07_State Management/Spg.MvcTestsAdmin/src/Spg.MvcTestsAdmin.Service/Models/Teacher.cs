using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spg.MvcTestsAdmin.Service.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Lesson = new HashSet<Lesson>();
            Schoolclass = new HashSet<Schoolclass>();
            Test = new HashSet<Test>();
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
        [InverseProperty(nameof(CatAccountState.Teacher))]
        public virtual CatAccountState T_CatAccountState { get; set; }
        [InverseProperty("L_TeacherNavigation")]
        public virtual ICollection<Lesson> Lesson { get; set; }
        [InverseProperty("C_ClassTeacherNavigation")]
        public virtual ICollection<Schoolclass> Schoolclass { get; set; }
        [InverseProperty("TE_TeacherNavigation")]
        public virtual ICollection<Test> Test { get; set; }
    }
}
