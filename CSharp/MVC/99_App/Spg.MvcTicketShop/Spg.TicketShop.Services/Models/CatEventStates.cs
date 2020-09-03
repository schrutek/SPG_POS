using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spg.TicketShop.Services.Models
{
    public partial class CatEventStates
    {
        public CatEventStates()
        {
            Events = new HashSet<Events>();
        }

        [Key]
        public Guid CatEventStateId { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public Guid? LaseChangeUserId { get; set; }
        [Required]
        [StringLength(10)]
        public string Key { get; set; }
        [Required]
        [StringLength(150)]
        public string ShortName { get; set; }
        public string Description { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        [ForeignKey(nameof(LaseChangeUserId))]
        [InverseProperty(nameof(Users.CatEventStates))]
        public virtual Users LaseChangeUser { get; set; }
        [InverseProperty("CatEventState")]
        public virtual ICollection<Events> Events { get; set; }
    }
}
