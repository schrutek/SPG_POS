using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Spg.Services.Dom
{
    public partial class Prices
    {
        [Key]
        public Guid PriceId { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public Guid? LaseChangeUserIdId { get; set; }
        [Required]
        [StringLength(200)]
        public string PriceDescription { get; set; }
        public double PriceGross { get; set; }
        public Guid ContingentId { get; set; }

        [ForeignKey(nameof(ContingentId))]
        [InverseProperty(nameof(Contingents.Prices))]
        public virtual Contingents Contingent { get; set; }
        [ForeignKey(nameof(LaseChangeUserIdId))]
        [InverseProperty(nameof(Users.Prices))]
        public virtual Users LaseChangeUserId { get; set; }
    }
}
