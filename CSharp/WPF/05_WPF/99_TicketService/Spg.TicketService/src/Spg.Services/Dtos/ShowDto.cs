using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Services.Dtos
{
    public class ShowDto
    {
        public Guid Id { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public Guid EventId { get; set; }
    }
}
