using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School2000Api.Model
{
    public partial class stunden
    {
        [Key]
        [StringLength(10)]
        public string ST_K_Klasse { get; set; }
        [StringLength(5)]
        public string ST_L_Lehrer { get; set; }
        [StringLength(10)]
        public string ST_G_Fach { get; set; }
        [Key]
        [StringLength(5)]
        public string ST_Stunde { get; set; }
        [StringLength(10)]
        public string ST_R_Raum { get; set; }

        [ForeignKey(nameof(ST_G_Fach))]
        [InverseProperty(nameof(gegenstaende.stunden))]
        public virtual gegenstaende ST_G_FachNavigation { get; set; }
        [ForeignKey(nameof(ST_K_Klasse))]
        [InverseProperty(nameof(klassen.stunden))]
        public virtual klassen ST_K_KlasseNavigation { get; set; }
        [ForeignKey(nameof(ST_L_Lehrer))]
        [InverseProperty(nameof(lehrer.stunden))]
        public virtual lehrer ST_L_LehrerNavigation { get; set; }
        [ForeignKey(nameof(ST_R_Raum))]
        [InverseProperty(nameof(raeume.stunden))]
        public virtual raeume ST_R_RaumNavigation { get; set; }
    }
}
