using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ConsoleApp1.Models
{
    [Table("CatTestState")]
    public partial class CatTestState
    {
        public CatTestState()
        {
            Tests = new HashSet<Test>();
        }

        [Key]
        public Guid CatTestStateId { get; set; }
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
        public DateTime? ValidFrom { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidTo { get; set; }

        [InverseProperty(nameof(Test.TE_CatTestState))]
        public virtual ICollection<Test> Tests { get; set; }
    }
}
