using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.TicketShop.Services.Dtos
{
    public class CatEventStateDto
    {
        public string Key { get; set; }

        public string ShortName { get; set; }

        public bool Selected { get; set; }
    }
}
