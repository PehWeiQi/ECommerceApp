using ECommerceApp.MVVM;
using ECommerceApp.Services;
using ECommerceApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ECommerceApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<CatalogViewModel>();
            services.AddSingleton<CatalogViewModel>();
            services.AddSingleton<INavigationService,NavigationService>();
            services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => 
            viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

            services.AddSingleton<ProductService>();

            _serviceProvider = services.BuildServiceProvider();
        }
        

        protected override void OnStartup(StartupEventArgs e)
        {
            // Start the app with the MainWindow
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
