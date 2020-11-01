using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spg.MvcTestsAdmin.Service.Models
{
    public partial class CatTestState
    {
        public CatTestState()
        {
            Test = new HashSet<Test>();
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

        [InverseProperty("TE_CatTestState")]
        public virtual ICollection<Test> Test { get; set; }
    }
}
