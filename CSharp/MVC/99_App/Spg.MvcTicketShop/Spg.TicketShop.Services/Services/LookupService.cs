using Spg.TicketShop.Services.Dtos;
using Spg.TicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spg.TicketShop.Services.Services
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
