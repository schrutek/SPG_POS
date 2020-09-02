using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School2000Api.Model
{
    public partial class lehrer
    {
        public lehrer()
        {
            InverseL_L_ChefNavigation = new HashSet<lehrer>();
            klassen = new HashSet<klassen>();
            pruefungen = new HashSet<pruefungen>();
            stunden = new HashSet<stunden>();
            vorgesetzteV_L_UntNavigation = new HashSet<vorgesetzte>();
            vorgesetzteV_L_VorgNavigation = new HashSet<vorgesetzte>();
        }

        [Key]
        [StringLength(5)]
        public string L_ID { get; set; }
        [StringLength(50)]
        public string L_Name { get; set; }
        [StringLength(50)]
        public string L_Vorname { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? L_Gebdat { get; set; }
        public float? L_Gehalt { get; set; }
        [StringLength(5)]
        public string L_L_Chef { get; set; }

        [ForeignKey(nameof(L_L_Chef))]
        [InverseProperty(nameof(lehrer.InverseL_L_ChefNavigation))]
        public virtual lehrer L_L_ChefNavigation { get; set; }
        [InverseProperty(nameof(lehrer.L_L_ChefNavigation))]
        public virtual ICollection<lehrer> InverseL_L_ChefNavigation { get; set; }
        [InverseProperty("K_L_KlavstNavigation")]
        public virtual ICollection<klassen> klassen { get; set; }
        [InverseProperty("P_L_PrueferNavigation")]
        public virtual ICollection<pruefungen> pruefungen { get; set; }
        [InverseProperty("ST_L_LehrerNavigation")]
        public virtual ICollection<stunden> stunden { get; set; }
        [InverseProperty(nameof(vorgesetzte.V_L_UntNavigation))]
        public virtual ICollection<vorgesetzte> vorgesetzteV_L_UntNavigation { get; set; }
        [InverseProperty(nameof(vorgesetzte.V_L_VorgNavigation))]
        public virtual ICollection<vorgesetzte> vorgesetzteV_L_VorgNavigation { get; set; }
    }
}
