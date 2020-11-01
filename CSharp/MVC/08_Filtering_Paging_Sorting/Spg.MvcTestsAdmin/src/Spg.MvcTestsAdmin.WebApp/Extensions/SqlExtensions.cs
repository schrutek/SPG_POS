using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.MvcTestsAdmin.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.MvcTestsAdmin.WebApp.Extensions
{
    public static class SqlExtensions
    {
        public static void ConfigureMsSql(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TestsAdministratorContext>(optionsBuilder =>
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
