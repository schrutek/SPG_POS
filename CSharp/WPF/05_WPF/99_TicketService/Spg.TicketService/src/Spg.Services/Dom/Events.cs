using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Spg.Services.Dom
{
    public partial class Events
    {
        public Events()
        {
            Shows = new HashSet<Shows>();
        }

        [Key]
        public Guid EventId { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public Guid? LaseChangeUserIdId { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime OnlineFrom { get; set; }
        public DateTime OnlineTo { get; set; }

        [ForeignKey(nameof(LaseChangeUserIdId))]
        [InverseProperty(nameof(Users.Events))]
        public virtual Users LaseChangeUserId { get; set; }
        [InverseProperty("Event")]
        public virtual ICollection<Shows> Shows { get; set; }
    }
}
