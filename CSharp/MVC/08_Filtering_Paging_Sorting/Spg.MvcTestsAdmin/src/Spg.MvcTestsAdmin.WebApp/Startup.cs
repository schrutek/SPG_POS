using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Models;
using Spg.MvcTestsAdmin.Service.Repositories;
using Spg.MvcTestsAdmin.Service.Services;
using Spg.MvcTestsAdmin.WebApp.Extensions;

namespace Spg.MvcTestsAdmin.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.ConfigureMsSql($"{Configuration["AppSettings:Database"]}");

            services.AddScoped<IRepositoryBase<Lesson, long>, RepositoryBase<Lesson, long>>();
            services.AddScoped<IRepositoryBase<Schoolclass, string>, RepositoryBase<Schoolclass, string>>();
            services.AddScoped<IReadOnlyRepositoryBase<Lesson>, ReadOnlyRepositoryBase<Lesson>>();
            services.AddScoped<IReadOnlyRepositoryBase<Schoolclass>, ReadOnlyRepositoryBase<Schoolclass>>();

            services.AddScoped<ISchoolclassService, SchoolclassService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IPeriodService, PeriodService>();
            services.AddScoped<ITeacherService, TeacherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
