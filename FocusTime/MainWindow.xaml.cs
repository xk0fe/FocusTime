using System.Windows;
using System.Windows.Input;
using FocusTime.Configuration;
using Microsoft.Win32;

namespace FocusTime;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Player _player;
    private Timer _timer;
    
    public MainWindow()
    {
        InitializeComponent();
        this.Topmost = true;

        _timer = new Timer().CallOnTick(OnTimerTick).SetInterval(GameplaySettings.TIMER_INTERVAL_SECONDS);
        _player = new Player(GameplaySettings.DEFAULT_PLAYER_LEVEL, TimeSpan.Zero, GameplaySettings.PLAYER_TARGET_EXPERIENCE);
        barExperience.Maximum = GameplaySettings.PROGRESS_BAR_MAXIMUM;

        MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
        SystemEvents.UserPreferenceChanging += SystemEventsOnUserPreferenceChanging;
        this.SizeToContent = SizeToContent.Width;

    }

    // todo change theme color
    private void SystemEventsOnUserPreferenceChanging(object sender, UserPreferenceChangingEventArgs e)
    {
    }
    
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        _player.Update();
        _player.AddExperience(_timer.TickDelta);
        
        var elapsed = _timer.Elapsed;
        lblLevel.Content = $"Lv.{_player.Level:00}";
        lblClock.Content = $"{elapsed.Hours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}";

        var experience = _player.Experience.TotalSeconds / _player.TargetExperience.TotalSeconds;
        barExperience.Value = experience;
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
    
    private void btnStartPause_Click(object sender, EventArgs e)
    {
        if (!_timer.IsRunning)
        {
            _timer.Start();
            iconBtnStartPause.Icon = GraphicConstants.PAUSE_ICON;
        }
        else
        {
            _timer.Stop();
            iconBtnStartPause.Icon = GraphicConstants.START_ICON;
        }
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        _timer.Reset();
        lblClock.Content = LocaleConstants.DEFAULT_TIME_TEXT;
    }

    private void btnHardReset_Click(object sender, EventArgs e)
    {
        var resetConfirmation = MessageBox.Show(LocaleConstants.DATA_RESET_DESCR, LocaleConstants.DATA_RESET_HEADER, MessageBoxButton.YesNo);
        if (resetConfirmation == MessageBoxResult.No)
        {
            return;
        }
        
        _timer.Stop();
        _player.Reset();
        lblLevel.Content = LocaleConstants.DEFAULT_LEVEL_TEXT;
        barExperience.Value = 0;
        btnReset_Click(sender, e);
        iconBtnStartPause.Icon = GraphicConstants.START_ICON;
    }

    private void Window_PreviewLostKeyboardFocus(object sender, EventArgs e)
    {
        Window window = (Window)sender;
        window.Topmost = true;
    }
}