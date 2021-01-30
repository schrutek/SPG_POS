using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Spg.Ado.Demo.Models
{
    [Table("Pupil")]
    [Index(nameof(P_Class), Name = "SchoolclassPupil")]
    [Index(nameof(P_Account), Name = "idx_P_Account", IsUnique = true)]
    public partial class Pupil
    {
        [Key]
        public long P_ID { get; set; }
        [Required]
        [StringLength(16)]
        public string P_Account { get; set; }
        [Required]
        [StringLength(100)]
        public string P_Lastname { get; set; }
        [Required]
        [StringLength(100)]
        public string P_Firstname { get; set; }
        [Required]
        [StringLength(8)]
        public string P_Class { get; set; }
        public Guid P_CatAccountStateId { get; set; }

        [ForeignKey(nameof(P_CatAccountStateId))]
        [InverseProperty(nameof(CatAccountState.Pupils))]
        public virtual CatAccountState P_CatAccountState { get; set; }
        [ForeignKey(nameof(P_Class))]
        [InverseProperty(nameof(Schoolclass.Pupils))]
        public virtual Schoolclass P_ClassNavigation { get; set; }
    }
}
