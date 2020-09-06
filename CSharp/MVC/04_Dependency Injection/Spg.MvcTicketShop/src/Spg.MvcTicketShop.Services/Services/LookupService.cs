using Spg.MvcTicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spg.MvcTicketShop.Services.Services
{
    public class LookupService
    {
        private readonly TicketShopContext _context;

        public LookupService(TicketShopContext context)
        {
            _context = context;
        }

        public List<CatEventStates> GetAllValidCatEventStates()
        {
            return _context.CatEventStates.ToList();
        }
    }
}
