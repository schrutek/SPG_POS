using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spg.MvcTicketShop.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.MvcTicketShop.FrontEnd.Extensions
{
    public static class SqlExtensions
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static void ConfigureMsSql(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TicketShopContext>(optionsBuilder =>
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder
                        .UseLoggerFactory(MyLoggerFactory)
                        .UseSqlServer(connectionString);
                }
            });
        }
    }
}
