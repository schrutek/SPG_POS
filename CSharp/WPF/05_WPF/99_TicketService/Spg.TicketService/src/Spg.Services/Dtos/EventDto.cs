using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Services.Dtos
{
    public class EventDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
