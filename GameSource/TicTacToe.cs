namespace TicTacToeConsoleVersion;

public class TicTacToe
{
    //private GameBoard _gameBoard;
    //private GameType _type;
    //private bool _useOptimizedAlgorithmForAi;
    //private int _steps = 0;

    //public TicTacToe(int boardSize, GameType gameType, bool useOptimizedAlgorithmForAi = true)
    //{
    //    _gameBoard = new GameBoard(boardSize);
    //    _type = gameType;
    //    _useOptimizedAlgorithmForAi = useOptimizedAlgorithmForAi;
    //}

    //public void PrintBoard() => Console.WriteLine(_gameBoard);

    //public bool IsGameOver(out GameResult result)
    //{
    //    result = _gameBoard.GetGameResult();
    //    return _gameBoard.IsGameOver();
    //}

    //public void NextMove()
    //{
    //    PrintBoard();
    //    var emptyCells = _gameBoard.GetAvailableMoves();
    //    var symbol = _steps % 2 == 0 ? BoardValue.X : BoardValue.O;
    //    var secondPlayerMove = _steps % 2 != 0;

    //    if (secondPlayerMove)
    //    {
    //        if (_type is GameType.PlayerVsAi or GameType.AiVsAi)
    //        {
    //            var res = _useOptimizedAlgorithmForAi ? 
    //                ComputerPlayer.MinMaxAlphaBeta(_gameBoard, emptyCells.Count, false) : 
    //                ComputerPlayer.MinMax(_gameBoard, false);
    //            _gameBoard.SetValue(res.Item2, symbol);
    //        }
    //        if(_type == GameType.PlayerVsPlayer) PlayerMove(symbol);
    //    }
    //    else
    //    {
    //        if(_type is GameType.PlayerVsAi or GameType.PlayerVsPlayer) PlayerMove(symbol);
    //        if (_type == GameType.AiVsAi)
    //        {
    //            var res = _useOptimizedAlgorithmForAi ? 
    //                ComputerPlayer.MinMaxAlphaBeta(_gameBoard, emptyCells.Count, true) : 
    //                ComputerPlayer.MinMax(_gameBoard, true);
    //            _gameBoard.SetValue(res.Item2, symbol);
    //        }
    //    }

    //    _steps++;
    //}

    //private void PlayerMove(BoardValue symbol)
    //{
    //    Console.WriteLine($"Player {symbol} move. Pick a cell (Row, Column): ");
    //    //TODO Check picked cell on correct
    //    var row = Convert.ToInt32(Console.ReadLine());
    //    var col = Convert.ToInt32(Console.ReadLine());

    //    _gameBoard.SetValue((row, col), symbol);
    //}
}