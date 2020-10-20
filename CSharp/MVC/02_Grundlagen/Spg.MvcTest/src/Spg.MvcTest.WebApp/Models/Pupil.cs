using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.MvcTest.WebApp.Models
{
    public class Pupil
    {
        [Required]
        public int Id { get; set; }

        [Required()]
        [StringLength(maximumLength: 250)]
        public string Class { get; set; }

        public string Lastname { get; set; }

        public string Firstname { get; set; }

        public string Gender { get; set; }
    }
}
