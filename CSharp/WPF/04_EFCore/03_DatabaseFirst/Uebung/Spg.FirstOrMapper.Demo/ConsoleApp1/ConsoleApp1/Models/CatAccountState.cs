using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ConsoleApp1.Models
{
    [Table("CatAccountState")]
    public partial class CatAccountState
    {
        public CatAccountState()
        {
            Pupils = new HashSet<Pupil>();
            Teachers = new HashSet<Teacher>();
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

        [InverseProperty(nameof(Pupil.P_CatAccountState))]
        public virtual ICollection<Pupil> Pupils { get; set; }
        [InverseProperty(nameof(Teacher.T_CatAccountState))]
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
