using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Spg.MvcTicketShop.Services.Models
{
    [Index(nameof(ContingentId))]
    [Index(nameof(LaseChangeUserId))]
    public partial class Prices
    {
        [Key]
        public Guid PriceId { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public Guid? LaseChangeUserId { get; set; }
        [Required]
        [StringLength(200)]
        public string PriceDescription { get; set; }
        public double PriceGross { get; set; }
        public Guid ContingentId { get; set; }

        [ForeignKey(nameof(ContingentId))]
        [InverseProperty(nameof(Contingents.Prices))]
        public virtual Contingents Contingent { get; set; }
        [ForeignKey(nameof(LaseChangeUserId))]
        [InverseProperty(nameof(Users.Prices))]
        public virtual Users LaseChangeUser { get; set; }
    }
}
