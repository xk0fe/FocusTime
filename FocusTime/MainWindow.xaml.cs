using System.Windows;
using System.Windows.Input;
using FocusTime.Configuration;

namespace FocusTime;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainController _controller;

    public MainWindow(MainController controller)
    {
        InitializeComponent();
        _controller = controller;

        _controller.OnExperienceUpdated += UpdateExperienceBar;
        _controller.OnLevelUpdated += UpdateLevelLabel;
        _controller.OnTimeUpdated += UpdateClockLabel;
        
        MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
    }

    private void UpdateExperienceBar(double experience)
    {
        barExperience.Value = experience;
    }

    private void UpdateLevelLabel(int level)
    {
        lblLevel.Content = $"Lv.{level:00}";
    }

    private void UpdateClockLabel(TimeSpan elapsed)
    {
        lblClock.Content = $"{elapsed.Hours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}";
    }

    private void btnStartPause_Click(object sender, RoutedEventArgs e)
    {
        if (_controller.IsRunning)
        {
            _controller.Pause();
            iconBtnStartPause.Icon = GraphicConstants.START_ICON;
        }
        else
        {
            _controller.Start();
            iconBtnStartPause.Icon = GraphicConstants.PAUSE_ICON;
        }
    }

    private void btnReset_Click(object sender, RoutedEventArgs e)
    {
        _controller.Reset();
        lblClock.Content = "00:00:00";
    }

    private void btnHardReset_Click(object sender, RoutedEventArgs e)
    {
        var resetConfirmation = MessageBox.Show("Are you sure you want to reset all data?", "Confirm Reset", MessageBoxButton.YesNo);
        if (resetConfirmation == MessageBoxResult.Yes)
        {
            _controller.HardReset();
            lblLevel.Content = "Lv.00";
            barExperience.Value = 0;
            btnReset_Click(sender, e);
            iconBtnStartPause.Icon = GraphicConstants.START_ICON;
        }
    }
    
    private void Window_PreviewLostKeyboardFocus(object sender, EventArgs e)
    {
        var window = (Window)sender;
        window.Topmost = true;
    }
    
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 1)
        {
            DragMove();
        }
        else if (e.ClickCount == 2)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
    }
}