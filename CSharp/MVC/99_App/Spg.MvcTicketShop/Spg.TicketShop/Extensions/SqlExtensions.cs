using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.TicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.TicketShop.Extensions
{
    public static class SqlExtensions
    {
        public static void ConfigureMsSql(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TicketShopContext>(optionsBuilder =>
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder
                        .UseSqlServer(connectionString);
                }
            });
        }
    }
}
