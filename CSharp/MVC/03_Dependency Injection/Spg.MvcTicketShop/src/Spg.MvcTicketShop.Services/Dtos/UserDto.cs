using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Spg.MvcTicketShop.Services.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }

        public DateTime? LastChangeDate { get; set; }

        public Guid? LaseChangeUserId { get; set; }

        public DateTime RegisterDateTime { get; set; }

        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        public string EMail { get; set; }
    }
}
