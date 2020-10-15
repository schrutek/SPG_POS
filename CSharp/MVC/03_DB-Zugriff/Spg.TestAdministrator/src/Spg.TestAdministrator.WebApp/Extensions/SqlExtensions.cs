using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Spg.TestAdministrator.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spg.TestAdministrator.WebApp.Extensions
{
    public static class SqlExtensions
    {
        public static void ConfigureSql(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TestsContext>(optionsBuilder => 
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
            });
        }
    }
}
