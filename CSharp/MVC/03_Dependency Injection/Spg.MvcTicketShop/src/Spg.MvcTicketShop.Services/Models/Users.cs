using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spg.MvcTicketShop.Services.Models
{
    public partial class Users
    {
        public Users()
        {
            BookingsLaseChangeUser = new HashSet<Bookings>();
            BookingsUser = new HashSet<Bookings>();
            CatEventStates = new HashSet<CatEventStates>();
            Contingents = new HashSet<Contingents>();
            Events = new HashSet<Events>();
            InverseLaseChangeUser = new HashSet<Users>();
            Prices = new HashSet<Prices>();
            Shows = new HashSet<Shows>();
        }

        [Key]
        public Guid UserId { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public Guid? LaseChangeUserId { get; set; }
        public DateTime RegisterDateTime { get; set; }
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(200)]
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        [Required]
        [StringLength(200)]
        public string Role { get; set; }

        [ForeignKey(nameof(LaseChangeUserId))]
        [InverseProperty(nameof(Users.InverseLaseChangeUser))]
        public virtual Users LaseChangeUser { get; set; }
        [InverseProperty(nameof(Bookings.LaseChangeUser))]
        public virtual ICollection<Bookings> BookingsLaseChangeUser { get; set; }
        [InverseProperty(nameof(Bookings.User))]
        public virtual ICollection<Bookings> BookingsUser { get; set; }
        [InverseProperty("LaseChangeUser")]
        public virtual ICollection<CatEventStates> CatEventStates { get; set; }
        [InverseProperty("LaseChangeUser")]
        public virtual ICollection<Contingents> Contingents { get; set; }
        [InverseProperty("LaseChangeUser")]
        public virtual ICollection<Events> Events { get; set; }
        [InverseProperty(nameof(Users.LaseChangeUser))]
        public virtual ICollection<Users> InverseLaseChangeUser { get; set; }
        [InverseProperty("LaseChangeUser")]
        public virtual ICollection<Prices> Prices { get; set; }
        [InverseProperty("LaseChangeUser")]
        public virtual ICollection<Shows> Shows { get; set; }
    }
}
