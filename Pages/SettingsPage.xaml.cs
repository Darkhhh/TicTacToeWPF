using System.Windows;
using System.Windows.Controls;
using TicTacToeConsoleVersion;

namespace TicTacToeWPFMatDes.Pages;

public partial class SettingsPage : UserControl
{
    public Action<int, GameType>? GameStarted;
    private GameType _gameType = GameType.PlayerVsAi;
    private int _size = 3;


    public SettingsPage() => InitializeComponent();


    private void RadioButton_Checked(object sender, RoutedEventArgs e)
    {
        RadioButton pressed = (RadioButton)sender;

        var content = pressed.Content.ToString();
        if (content == "Player vs AI") _gameType = GameType.PlayerVsAi;
        if (content == "AI vs AI") _gameType = GameType.AiVsAi;

        if (content == "3 X 3") _size = 3;
        if (content == "4 X 4") _size = 4;
        if (content == "5 X 5") _size = 5;
    }


    private void startGameButton_Click(object sender, RoutedEventArgs e) => GameStarted?.Invoke(_size, _gameType);
}
