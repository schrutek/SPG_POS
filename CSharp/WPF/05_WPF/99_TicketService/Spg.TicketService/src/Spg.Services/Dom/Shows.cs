using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Spg.Services.Dom
{
    public partial class Shows
    {
        public Shows()
        {
            Contingents = new HashSet<Contingents>();
        }

        [Key]
        public Guid ShowId { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public Guid? LaseChangeUserIdId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid EventId { get; set; }

        [ForeignKey(nameof(EventId))]
        [InverseProperty(nameof(Events.Shows))]
        public virtual Events Event { get; set; }
        [ForeignKey(nameof(LaseChangeUserIdId))]
        [InverseProperty(nameof(Users.Shows))]
        public virtual Users LaseChangeUserId { get; set; }
        [InverseProperty("Show")]
        public virtual ICollection<Contingents> Contingents { get; set; }
    }
}
