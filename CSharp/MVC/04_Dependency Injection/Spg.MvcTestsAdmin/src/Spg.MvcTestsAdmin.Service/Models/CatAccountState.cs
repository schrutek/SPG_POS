using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spg.MvcTestsAdmin.Service.Models
{
    public partial class CatAccountState
    {
        public CatAccountState()
        {
            Pupil = new HashSet<Pupil>();
            Teacher = new HashSet<Teacher>();
        }

        [Key]
        public Guid CatAccountStateId { get; set; }
        [Required]
        [StringLength(1)]
        public string Key { get; set; }
        [Required]
        [StringLength(50)]
        public string ShortName { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidForm { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidTo { get; set; }

        [InverseProperty("P_CatAccountState")]
        public virtual ICollection<Pupil> Pupil { get; set; }
        [InverseProperty("T_CatAccountState")]
        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}
