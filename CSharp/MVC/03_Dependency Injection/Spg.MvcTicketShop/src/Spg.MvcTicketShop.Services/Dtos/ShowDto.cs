using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.MvcTicketShop.Services.Dtos
{
    public class ShowDto
    {
        public Guid ShowId { get; set; }
        
        public DateTime? LastChangeDate { get; set; }
        
        public Guid? LaseChangeUserId { get; set; }
        
        public DateTime CheckIn { get; set; }
        
        public DateTime Start { get; set; }
        
        public DateTime End { get; set; }
        
        public Guid EventId { get; set; }

        public virtual EventDto Event { get; set; }
    }
}
