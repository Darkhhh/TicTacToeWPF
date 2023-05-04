using System.Windows;
using TicTacToeConsoleVersion;

namespace TicTacToeWPFMatDes;

public partial class MainWindow : Window
{
    #region Pages

    private Pages.StartPage _startWindow = new Pages.StartPage();
    private Pages.SettingsPage _gameSettingsWindow = new Pages.SettingsPage();

    #endregion


    public MainWindow()
    {
        InitializeComponent();
        windowContent.Content = _startWindow;
        _startWindow.NextWindowOpened += OpenSettingsWindow;
        _gameSettingsWindow.GameStarted += OpenGameWindow;
    }

    private void OpenSettingsWindow() => windowContent.Content = _gameSettingsWindow;
    private void OpenGameWindow(int size, GameType gameType)
    {
        var page = new Pages.GamePage(size, gameType);
        windowContent.Content = page;
        page.GoBack = OpenSettingsWindow;
    }
}
