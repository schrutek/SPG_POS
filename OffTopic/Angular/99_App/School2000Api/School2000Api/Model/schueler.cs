using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School2000Api.Model
{
    public partial class schueler
    {
        public schueler()
        {
            klassenK_S_KlasprNavigation = new HashSet<klassen>();
            klassenK_S_KlasprstvNavigation = new HashSet<klassen>();
            pruefungen = new HashSet<pruefungen>();
        }

        [Key]
        public short S_SCHNR { get; set; }
        public string S_Name { get; set; }
        [StringLength(50)]
        public string S_Vorname { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? S_Gebdat { get; set; }
        [StringLength(50)]
        public string S_Adresse { get; set; }
        [StringLength(10)]
        public string S_K_Klasse { get; set; }

        [ForeignKey(nameof(S_K_Klasse))]
        [InverseProperty(nameof(klassen.schueler))]
        public virtual klassen S_K_KlasseNavigation { get; set; }
        [InverseProperty(nameof(klassen.K_S_KlasprNavigation))]
        public virtual ICollection<klassen> klassenK_S_KlasprNavigation { get; set; }
        [InverseProperty(nameof(klassen.K_S_KlasprstvNavigation))]
        public virtual ICollection<klassen> klassenK_S_KlasprstvNavigation { get; set; }
        [InverseProperty("P_S_KandidatNavigation")]
        public virtual ICollection<pruefungen> pruefungen { get; set; }
    }
}
