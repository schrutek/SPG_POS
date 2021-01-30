using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Services.Dtos
{
    public class GuestDto
    {
        public DateTime RegisterDateTime { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }
    }
}
