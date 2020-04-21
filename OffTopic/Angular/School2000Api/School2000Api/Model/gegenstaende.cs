using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School2000Api.Model
{
    public partial class gegenstaende
    {
        public gegenstaende()
        {
            pruefungen = new HashSet<pruefungen>();
            stunden = new HashSet<stunden>();
        }

        [Key]
        [StringLength(10)]
        public string G_ID { get; set; }
        [StringLength(50)]
        public string G_Bez { get; set; }

        [InverseProperty("P_G_FachNavigation")]
        public virtual ICollection<pruefungen> pruefungen { get; set; }
        [InverseProperty("ST_G_FachNavigation")]
        public virtual ICollection<stunden> stunden { get; set; }
    }
}
