using CustomersDesktop.Api;
using CustomersDesktop.ViewModels;
using CustomersDesktop.Views.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CustomersDesktop.ViewModels.Dialogs;

namespace CustomersDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddRefitClient<ICustomerApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://localhost:7026"));

            services.AddSingleton<MainWindow>();

            services.AddSingleton<MainWindowViewModel>();
            services.AddScoped<CreateCustomerDialogViewModel>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
