using System.Windows;

namespace FocusTime;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private MainController _mainController;

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        _mainController = new MainController();
        
        var mainWindow = new MainWindow(_mainController);
        mainWindow.Show();
    }
}