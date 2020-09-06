using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spg.MvcTicketShop.Services.Models
{
    public partial class Bookings
    {
        [Key]
        public Guid BookingId { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public Guid? LaseChangeUserId { get; set; }
        public Guid ContingentId { get; set; }
        public Guid UserId { get; set; }
        public DateTime BookingDateTime { get; set; }
        public int TicketCount { get; set; }
        public string TicketState { get; set; }

        [ForeignKey(nameof(ContingentId))]
        [InverseProperty(nameof(Contingents.Bookings))]
        public virtual Contingents Contingent { get; set; }
        [ForeignKey(nameof(LaseChangeUserId))]
        [InverseProperty(nameof(Users.BookingsLaseChangeUser))]
        public virtual Users LaseChangeUser { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.BookingsUser))]
        public virtual Users User { get; set; }
    }
}
