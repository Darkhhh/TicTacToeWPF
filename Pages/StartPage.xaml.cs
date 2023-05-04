using System.Windows;
using System.Windows.Controls;

namespace TicTacToeWPFMatDes.Pages;

public partial class StartPage : UserControl
{
    public Action? NextWindowOpened;

    public StartPage() => InitializeComponent();

    private void StartButton_Click(object sender, RoutedEventArgs e) => NextWindowOpened?.Invoke();
}
