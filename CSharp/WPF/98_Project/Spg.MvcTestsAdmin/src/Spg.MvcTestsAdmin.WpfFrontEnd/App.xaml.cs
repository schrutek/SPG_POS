using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spg.MvcTestsAdmin.Service.Infrastructure;
using Spg.MvcTestsAdmin.Service.Interfaces;
using Spg.MvcTestsAdmin.Service.Services;
using Spg.MvcTestsAdmin.WpfFrontEnd.ViewModels;

namespace Spg.MvcTestsAdmin.WpfFrontEnd
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Einen ServiceProvider erstellen
        /// </summary>
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Den ServiceProvider mit allen notwendigen Services befüllen
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureServices(IServiceCollection services)
        {
            string connectionString = LoadConnectionString();
            services.ConfigureSql(connectionString);

            services.AddSingleton<MainWindow>();
            services.AddSingleton<SchoolClassViewModel>();
            services.AddSingleton<LessonViewModel>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<ISchoolclassService, SchoolclassService>();
            services.AddTransient<ILessonService, LessonService>();
            services.AddTransient<ITeacherService, TeacherService>();
        }

        /// <summary>
        /// Aus dem ServiceProvider den Service für das MainWindow herausnehmen
        /// und damit das Fenster anzeigen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        /// <summary>
        /// ConnectionString zur DB aus der appsettings.json laden
        /// </summary>
        /// <returns></returns>
        private string LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            return configuration.GetConnectionString("Databbase");
        }
    }
}
