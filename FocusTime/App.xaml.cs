using System.IO;
using System.Windows;
using FocusTime.Constants;
using FocusTime.Models.JSON;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FocusTime;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private MainController _mainController;

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var configurationPath = Path.Combine(AppContext.BaseDirectory, AppConstants.APP_CONFIGURATION_DIRECTORY);
        
        var builder = new ConfigurationBuilder()
            .SetBasePath(configurationPath)
            .AddJsonFile("gameplaySettings.json", optional: false, reloadOnChange: true);
        
        var configuration = builder.Build();

        var appSettings = new JsonGameplaySettings();
        configuration.GetSection("GameplaySettings").Bind(appSettings);

        var appBuilder = Host.CreateApplicationBuilder();
        appBuilder.Services.AddSingleton(appSettings);

        var host = appBuilder.Build();
        
        _mainController = new MainController(host.Services);
        
        var mainWindow = new MainWindow(_mainController);
        mainWindow.Show();
    }
}