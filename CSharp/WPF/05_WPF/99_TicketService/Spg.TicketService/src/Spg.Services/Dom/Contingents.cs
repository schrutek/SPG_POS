using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Spg.Services.Dom
{
    public partial class Contingents
    {
        public Contingents()
        {
            Bookings = new HashSet<Bookings>();
            Prices = new HashSet<Prices>();
        }

        [Key]
        public Guid ContingentId { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public Guid? LaseChangeUserIdId { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public int Seats { get; set; }
        public Guid ShowId { get; set; }

        [ForeignKey(nameof(LaseChangeUserIdId))]
        [InverseProperty(nameof(Users.Contingents))]
        public virtual Users LaseChangeUserId { get; set; }
        [ForeignKey(nameof(ShowId))]
        [InverseProperty(nameof(Shows.Contingents))]
        public virtual Shows Show { get; set; }
        [InverseProperty("Contingent")]
        public virtual ICollection<Bookings> Bookings { get; set; }
        [InverseProperty("Contingent")]
        public virtual ICollection<Prices> Prices { get; set; }
    }
}
