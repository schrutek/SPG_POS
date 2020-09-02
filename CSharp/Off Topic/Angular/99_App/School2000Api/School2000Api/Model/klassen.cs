using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School2000Api.Model
{
    public partial class klassen
    {
        public klassen()
        {
            schueler = new HashSet<schueler>();
            stunden = new HashSet<stunden>();
        }

        [Key]
        [StringLength(10)]
        public string K_ID { get; set; }
        [StringLength(50)]
        public string K_Bez { get; set; }
        public short? K_S_Klaspr { get; set; }
        public short? K_S_Klasprstv { get; set; }
        [StringLength(5)]
        public string K_L_Klavst { get; set; }

        [ForeignKey(nameof(K_L_Klavst))]
        [InverseProperty(nameof(lehrer.klassen))]
        public virtual lehrer K_L_KlavstNavigation { get; set; }
        [ForeignKey(nameof(K_S_Klaspr))]
        [InverseProperty("klassenK_S_KlasprNavigation")]
        public virtual schueler K_S_KlasprNavigation { get; set; }
        [ForeignKey(nameof(K_S_Klasprstv))]
        [InverseProperty("klassenK_S_KlasprstvNavigation")]
        public virtual schueler K_S_KlasprstvNavigation { get; set; }
        [InverseProperty("S_K_KlasseNavigation")]
        public virtual ICollection<schueler> schueler { get; set; }
        [InverseProperty("ST_K_KlasseNavigation")]
        public virtual ICollection<stunden> stunden { get; set; }
    }
}
