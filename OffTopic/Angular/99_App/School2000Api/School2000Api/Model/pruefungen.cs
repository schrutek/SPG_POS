using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School2000Api.Model
{
    public partial class pruefungen
    {
        [Key]
        [Column(TypeName = "datetime")]
        public DateTime P_Datum { get; set; }
        [Key]
        public short P_S_Kandidat { get; set; }
        [Key]
        [StringLength(5)]
        public string P_L_Pruefer { get; set; }
        [Key]
        [StringLength(10)]
        public string P_G_Fach { get; set; }
        [StringLength(3)]
        public string P_Art { get; set; }
        public byte? P_Note { get; set; }

        [ForeignKey(nameof(P_G_Fach))]
        [InverseProperty(nameof(gegenstaende.pruefungen))]
        public virtual gegenstaende P_G_FachNavigation { get; set; }
        [ForeignKey(nameof(P_L_Pruefer))]
        [InverseProperty(nameof(lehrer.pruefungen))]
        public virtual lehrer P_L_PrueferNavigation { get; set; }
        [ForeignKey(nameof(P_S_Kandidat))]
        [InverseProperty(nameof(schueler.pruefungen))]
        public virtual schueler P_S_KandidatNavigation { get; set; }
    }
}
