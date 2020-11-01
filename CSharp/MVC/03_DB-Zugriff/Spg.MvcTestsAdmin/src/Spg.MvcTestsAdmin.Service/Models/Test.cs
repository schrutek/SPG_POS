using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spg.MvcTestsAdmin.Service.Models
{
    public partial class Test
    {
        [Key]
        public long TE_ID { get; set; }
        [Required]
        [StringLength(8)]
        public string TE_Class { get; set; }
        [Required]
        [StringLength(8)]
        public string TE_Teacher { get; set; }
        [Required]
        [StringLength(8)]
        public string TE_Subject { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime TE_Date { get; set; }
        public long TE_Lesson { get; set; }
        public Guid TE_CatTestStateId { get; set; }

        [ForeignKey(nameof(TE_CatTestStateId))]
        [InverseProperty(nameof(CatTestState.Test))]
        public virtual CatTestState TE_CatTestState { get; set; }
        [ForeignKey(nameof(TE_Class))]
        [InverseProperty(nameof(Schoolclass.Test))]
        public virtual Schoolclass TE_ClassNavigation { get; set; }
        [ForeignKey(nameof(TE_Lesson))]
        [InverseProperty(nameof(Period.Test))]
        public virtual Period TE_LessonNavigation { get; set; }
        [ForeignKey(nameof(TE_Teacher))]
        [InverseProperty(nameof(Teacher.Test))]
        public virtual Teacher TE_TeacherNavigation { get; set; }
    }
}
