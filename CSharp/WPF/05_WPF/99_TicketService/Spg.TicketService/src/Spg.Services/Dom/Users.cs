using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Spg.Services.Dom
{
    public partial class Users
    {
        public Users()
        {
            BookingsLaseChangeUserId = new HashSet<Bookings>();
            BookingsUser = new HashSet<Bookings>();
            Contingents = new HashSet<Contingents>();
            Events = new HashSet<Events>();
            InverseLaseChangeUserId = new HashSet<Users>();
            Prices = new HashSet<Prices>();
            Shows = new HashSet<Shows>();
        }

        [Key]
        public Guid UserId { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public Guid? LaseChangeUserIdId { get; set; }
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

        [ForeignKey(nameof(LaseChangeUserIdId))]
        [InverseProperty(nameof(Users.InverseLaseChangeUserId))]
        public virtual Users LaseChangeUserId { get; set; }
        [InverseProperty(nameof(Bookings.LaseChangeUserId))]
        public virtual ICollection<Bookings> BookingsLaseChangeUserId { get; set; }
        [InverseProperty(nameof(Bookings.User))]
        public virtual ICollection<Bookings> BookingsUser { get; set; }
        [InverseProperty("LaseChangeUserId")]
        public virtual ICollection<Contingents> Contingents { get; set; }
        [InverseProperty("LaseChangeUserId")]
        public virtual ICollection<Events> Events { get; set; }
        [InverseProperty(nameof(Users.LaseChangeUserId))]
        public virtual ICollection<Users> InverseLaseChangeUserId { get; set; }
        [InverseProperty("LaseChangeUserId")]
        public virtual ICollection<Prices> Prices { get; set; }
        [InverseProperty("LaseChangeUserId")]
        public virtual ICollection<Shows> Shows { get; set; }
    }
}
