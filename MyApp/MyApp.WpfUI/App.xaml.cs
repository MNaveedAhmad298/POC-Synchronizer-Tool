using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Data.Repositories;
using MyApp.Core.Entities;
using MyApp.Core.Interfaces;
using MyApp.Services;
using System.Windows;

namespace MyApp.WpfUI
{
    public partial class App : Application
    {
        public static ServiceProvider Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            
            services.AddDbContextFactory<AppDbContext>(options =>
                options.UseSqlServer(
                    "YOUR_CONNECTION_STRING_HERE"));

           
            services.AddScoped(typeof(IGenericCrudRepository<>), typeof(GenericCrudRepository<>));
            services.AddScoped(typeof(ISyncRepository<>), typeof(SyncRepository<>));

            services.AddScoped(typeof(GenericCrudService<>));
            

            Services = services.BuildServiceProvider();

            base.OnStartup(e);
        }
    }
}
