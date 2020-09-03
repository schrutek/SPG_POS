using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School2000Api.Model
{
    public partial class vorgesetzte
    {
        [Key]
        [StringLength(5)]
        public string V_L_Vorg { get; set; }
        [Key]
        [StringLength(50)]
        public string V_Art { get; set; }
        [Key]
        [StringLength(5)]
        public string V_L_Unt { get; set; }

        [ForeignKey(nameof(V_L_Unt))]
        [InverseProperty(nameof(lehrer.vorgesetzteV_L_UntNavigation))]
        public virtual lehrer V_L_UntNavigation { get; set; }
        [ForeignKey(nameof(V_L_Vorg))]
        [InverseProperty(nameof(lehrer.vorgesetzteV_L_VorgNavigation))]
        public virtual lehrer V_L_VorgNavigation { get; set; }
    }
}
