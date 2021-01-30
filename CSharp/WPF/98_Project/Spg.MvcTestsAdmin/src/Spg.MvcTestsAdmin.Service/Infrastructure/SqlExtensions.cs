using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Spg.MvcTestsAdmin.Service.Infrastructure
{
    public static class SqlExtensions
    {
        public static void ConfigureSql(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TestsAdministratorContext>(optionBuilder =>
            {
                if (!optionBuilder.IsConfigured)
                {
                    optionBuilder.UseSqlServer(connectionString);
                }
            });
        }
    }
}
