using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using School2000Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace School2000Api.Extensions
{
    public static class HostingExtrensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });
        }

        /// <summary>
        /// Setzt den DbContext auf die übergebene SQLite Datenbank. Durch Dependency Injection wird in
        /// den Controllerklassen im Konstruktor der Context automatisch übergeben.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="database"></param>
        public static void ConfigureDatabase(this IServiceCollection services, string database)
        {
            services.AddDbContext<School2000Context>(options =>
                options.UseSqlServer(database)
            );
        }
    }
}
