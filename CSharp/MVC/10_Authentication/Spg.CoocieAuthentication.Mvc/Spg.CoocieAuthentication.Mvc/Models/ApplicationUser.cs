using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.CoocieAuthentication.Mvc.Models
{
    public class ApplicationUser
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }
    }
}
