using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TicTacToeConsoleVersion;

namespace TicTacToeWPFMatDes.Pages;

public partial class GamePage : UserControl
{
    #region Go To Settings Page Action

    public Action? GoBack;

    private void Button_Click(object sender, RoutedEventArgs e) => GoBack?.Invoke();

    #endregion


    #region Board Info

    private Button[,] _cellButtons = new Button[0, 0];
    private int _boardSize;
    private GameBoard _gameBoard = new GameBoard(3);

    #endregion


    #region Game Info

    private GameType _type;
    private bool _useOptimizedAlgorithmForAi = false;
    private int _steps = 0;
    private BoardValue _currentBoardValue = BoardValue.X;

    private bool AiMove => _steps % 2 != 0;
    private bool GameFinished { get; set; } = false;

    #endregion


    #region Constructing Game

    public GamePage(int boardSize, GameType gameType)
    {
        InitializeComponent();
        _type = gameType;
        _boardSize = boardSize;
        if (boardSize > 3) _useOptimizedAlgorithmForAi = true;
        CreateBoard(boardSize);
        StartGame();
    }

    private void CreateBoard(int boardSize)
    {
        _gameBoard = new GameBoard(boardSize);
        _cellButtons = new Button[boardSize, boardSize];

        for (int i = 0; i < boardSize; i++)
        {
            GameBoardGrid.RowDefinitions.Add(new RowDefinition());
            GameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                var button = new Button() { Margin = new Thickness(5, 5, 5, 5), Width = 50, Height = 50};
                button.Click += CellButtonClick;
                GameBoardGrid.Children.Add(button);
                Grid.SetColumn(button, j);
                Grid.SetRow(button, i);
                _cellButtons[i, j] = button;
            }
        }
    }

    private async void StartGame()
    {
        if (_type == GameType.AiVsAi)
        {
            if (_boardSize > 3)
            {
                SetValue((0, 0), BoardValue.X);
                SetValue((0, 1), BoardValue.O);
                _steps = 2;
            }

            if (SetValue(await Task.Run(() => AsyncComputerAiMove()))) NextPlayerMove();
        }
    }

    #endregion






    private void FinishGame()
    {
        GameFinished = true;
        var result = _gameBoard.GetGameResult();

        if (result == GameResult.Draw) GameResultTextBlock.Text = "Draw";
        if (result == GameResult.XWin) GameResultTextBlock.Text = "X Wins!";
        if (result == GameResult.OWin) GameResultTextBlock.Text = "O Wins!";
    }


    private void CellButtonClick(object sender, RoutedEventArgs e)
    {
        if (_type == GameType.AiVsAi || _type == GameType.PlayerVsAi && AiMove || GameFinished) return;

        var clickedButton = (Button)sender;
        var cellPosition = (-1, -1);

        for (int i = 0; i < _boardSize; i++)
        {
            for (int j = 0; j < _boardSize; j++)
            {
                if (_cellButtons[i, j] == clickedButton)
                {
                    cellPosition = (i, j);
                    break;
                }
            }
        }

        if (SetValue(cellPosition)) NextPlayerMove();
    }


    private bool SetValue((int, int) cellPosition)
    {
        if (!_gameBoard.SetValue(cellPosition, _currentBoardValue)) return false;

        if (_currentBoardValue == BoardValue.X) _cellButtons[cellPosition.Item1, cellPosition.Item2].Content = new UserControls.IconX();
        else _cellButtons[cellPosition.Item1, cellPosition.Item2].Content = new UserControls.IconO();

        if (_gameBoard.IsGameOver())
        {
            FinishGame();
            return false;
        }
        return true;
    }

    private bool SetValue((int, int) cellPosition, BoardValue boardValue)
    {
        if (!_gameBoard.SetValue(cellPosition, boardValue)) return false;

        if (boardValue == BoardValue.X) _cellButtons[cellPosition.Item1, cellPosition.Item2].Content = new UserControls.IconX();
        else _cellButtons[cellPosition.Item1, cellPosition.Item2].Content = new UserControls.IconO();

        if (_gameBoard.IsGameOver())
        {
            FinishGame();
            return false;
        }
        return true;
    }



    private async void NextPlayerMove()
    {
        if (GameFinished) return;

        _steps++;
        _currentBoardValue = _currentBoardValue == BoardValue.X ? BoardValue.O : BoardValue.X;


        if ((_type == GameType.PlayerVsAi && AiMove || _type == GameType.AiVsAi) && !GameFinished)
        {
            //if (SetValue(ComputerAiMove())) NextPlayerMove();
            if (SetValue(await Task.Run(() => AsyncComputerAiMove()))) NextPlayerMove();
        }

    }

    //private void ComputerMove()
    //{
    //    if (_gameBoard.IsGameOver()) return;

    //    var emptyCells = _gameBoard.GetAvailableMoves();
    //    var secondPlayerMove = _steps % 2 != 0;

    //    if (secondPlayerMove && (_type is GameType.PlayerVsAi or GameType.AiVsAi) || !secondPlayerMove && _type == GameType.AiVsAi)
    //    {
    //        var isMaximizing = !secondPlayerMove;
    //        var res = _useOptimizedAlgorithmForAi ?
    //                ComputerPlayer.MinMaxAlphaBeta(_gameBoard, emptyCells.Count, isMaximizing) :
    //                ComputerPlayer.MinMax(_gameBoard, isMaximizing);
    //        _gameBoard.SetValue(res.Item2, _currentBoardValue);
    //        _cellButtons[res.Item2.Item1, res.Item2.Item2].Content = _currentBoardValue;
    //    }

    //    NextPlayerMove();
    //}


    //private (int, int) ComputerAiMove()
    //{
    //    var emptyCells = _gameBoard.GetAvailableMoves();
    //    var secondPlayerMove = _steps % 2 != 0;

    //    if (secondPlayerMove && (_type is GameType.PlayerVsAi or GameType.AiVsAi) || 
    //        !secondPlayerMove && _type == GameType.AiVsAi)
    //    {
    //        var isMaximizing = !secondPlayerMove;
    //        var res = _useOptimizedAlgorithmForAi ?
    //                ComputerPlayer.MinMaxAlphaBeta(_gameBoard, emptyCells.Count, isMaximizing) :
    //                ComputerPlayer.MinMax(_gameBoard, isMaximizing);

    //        return res.Item2;
    //    }

    //    return (-1, -1);
    //}

    private async Task<(int, int)> AsyncComputerAiMove()
    {
        if (_type == GameType.AiVsAi) Thread.Sleep(500);

        var emptyCells = _gameBoard.GetAvailableMoves();
        var secondPlayerMove = _steps % 2 != 0;

        if (secondPlayerMove && (_type is GameType.PlayerVsAi or GameType.AiVsAi) || 
            !secondPlayerMove && _type == GameType.AiVsAi)
        {
            var isMaximizing = !secondPlayerMove;

            var res = (0, (-1, -1));
            res = _useOptimizedAlgorithmForAi
                //? await ComputerPlayer.AsyncMinMaxAlphaBeta(_gameBoard, emptyCells.Count, isMaximizing)
                ? await ComputerPlayer.AsyncMinMaxAlphaBeta(_gameBoard, isMaximizing)
                : await ComputerPlayer.AsyncMinMax(_gameBoard, isMaximizing);

            return res.Item2;
        }

        return (-1, -1);
    }
}
