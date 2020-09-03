using Spg.MvcTicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Spg.MvcTicketShop.Services.Dtos
{
    public class EventDto
    {
        public Guid EventId { get; set; }

        public DateTime? LastChangeDate { get; set; }

        public Guid? LaseChangeUserId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime OnlineFrom { get; set; }

        public DateTime OnlineTo { get; set; }

        public Guid? CatEventStateId { get; set; }

        public virtual CatEventStates CatEventState { get; set; }

        public virtual ICollection<ShowDto> Shows { get; set; }

        public bool HasMore { get; set; }
    }
}
