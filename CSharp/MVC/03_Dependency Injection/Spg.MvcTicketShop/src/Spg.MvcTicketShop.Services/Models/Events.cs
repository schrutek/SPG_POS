using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spg.MvcTicketShop.Services.Models
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
        public Guid? LaseChangeUserId { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name = "Eventname")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime OnlineFrom { get; set; }
        public DateTime OnlineTo { get; set; }
        public Guid? CatEventStateId { get; set; }

        [ForeignKey(nameof(CatEventStateId))]
        [InverseProperty(nameof(CatEventStates.Events))]
        public virtual CatEventStates CatEventState { get; set; }
        [ForeignKey(nameof(LaseChangeUserId))]
        [InverseProperty(nameof(Users.Events))]
        public virtual Users LaseChangeUser { get; set; }
        [InverseProperty("Event")]
        public virtual ICollection<Shows> Shows { get; set; }
    }
}
