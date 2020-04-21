using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School2000Api.Model
{
    public partial class raeume
    {
        public raeume()
        {
            stunden = new HashSet<stunden>();
        }

        [Key]
        [StringLength(10)]
        public string R_ID { get; set; }
        public short? R_Plaetze { get; set; }

        [InverseProperty("ST_R_RaumNavigation")]
        public virtual ICollection<stunden> stunden { get; set; }
    }
}
