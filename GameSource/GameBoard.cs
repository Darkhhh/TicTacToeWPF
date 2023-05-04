namespace TicTacToeConsoleVersion;

public class GameBoard
{
    #region Private Values

    private BoardValue[,] _board;

    private int _size;

    public BoardValue[,] Board => _board;

    #endregion


    #region Constructors

    public GameBoard(int size)
    {
        _size = size;
        _board = new BoardValue[_size, _size];
        for (var i = 0; i < _size; i++)
        {
            for (var j = 0; j < _size; j++) _board[i, j] = BoardValue.None;
        }
    }

    public static GameBoard CopyGameBoard(GameBoard original)
    {
        var copy = new GameBoard(original._size);

        for (var i = 0; i < original._size; i++)
        {
            for (var j = 0; j < original._size; j++)
            {
                copy._board[i, j] = original._board[i, j];
            }
        }
        return copy;
    }

    public void Reset()
    {
        for (var i = 0; i < _size; i++)
        {
            for (var j = 0; j < _size; j++) _board[i, j] = BoardValue.None;
        }
    }

    #endregion

    
    #region Override

    public override string ToString()
    {
        var s = "";
        for (var i = 0; i < _size; i++) s += i + "\t";
        s += "\n" + string.Join("", Enumerable.Repeat('=', _size * 6)) + "\n";

        for (var i = 0; i < _size; i++)
        {
            for (var j = 0; j < _size; j++)
            {
                s += (_board[i,j] != BoardValue.None ? _board[i,j].ToString() : "_") + "\t";
            }
            s += $"| {i}\n";
        }
        s += "\n" + string.Join("", Enumerable.Repeat('=', _size * 6)) + "\n";
        return s;
    }

    #endregion


    #region Game Intermediate

    public bool SetValue((int, int) place, BoardValue boardValue)
    {
        if (place.Item1 < 0 || place.Item1 > _size || place.Item2 < 0 || place.Item2 > _size)
            throw new Exception("Incorrect position indices");

        if (_board[place.Item1, place.Item2] != BoardValue.None) return false;
        _board[place.Item1, place.Item2] = boardValue;
        return true;
    }


    public void ClearValue((int, int) place)
    {
        if (place.Item1 < 0 || place.Item1 > _size || place.Item2 < 0 || place.Item2 > _size)
            throw new Exception("Incorrect position indices");

        _board[place.Item1, place.Item2] = BoardValue.None;
    }
    

    public List<(int, int)> GetAvailableMoves()
    {
        var moves = new List<(int, int)>();
        for (var i = 0; i < _size; i++)
        {
            for (var j = 0; j < _size; j++)
            {
                if (_board[i, j] == BoardValue.None) moves.Add((i, j));
            }
        }
        return moves;
    }

    #endregion
    
    
    #region Getting Result

    public bool HasWon(BoardValue playerValue)
    {
        //for (var i = 0; i < _size; i++)
        //{
        //    var amountInColumn = 0;
        //    var amountInRow = 0;
        //    for (var j = 0; j < _size; j++)
        //    {
        //        if (_board[i, j] != playerValue) continue;
        //        amountInColumn++;
        //        amountInRow++;
        //    }

        //    if (amountInRow == _size || amountInColumn == _size) return true;
        //}

        //int mainDiagonal = 0, secondaryDiagonal = 0;
        //for (var i = 0; i < _size; i++)
        //{
        //    if (_board[i, i] == playerValue) mainDiagonal++;
        //    if (_board[i, _size - 1 - i] == playerValue) secondaryDiagonal++;
        //}

        //return mainDiagonal == _size || secondaryDiagonal == _size;

        int mainDiagonal = 0, secondaryDiagonal = 0;
        for (var i = 0; i < _size; i++)
        {
            if (_board[i, i] == playerValue) mainDiagonal++;
            if (_board[i, _size - 1 - i] == playerValue) secondaryDiagonal++;
        }
        if (mainDiagonal == _size || secondaryDiagonal == _size) return true;


        for (var i = 0; i < _size; i++)
        {
            var amountInRow = 0;
            var amountInColumn = 0;
            for (var j = 0; j < _size; j++)
            {
                if (_board[i, j] == playerValue) amountInRow++;
                if (_board[j, i] == playerValue) amountInColumn++;
            }
            if (amountInRow == _size || amountInColumn == _size) return true;
        }
        return false;
    }


    public bool IsGameOver()
    {
        return HasWon(BoardValue.O) || HasWon(BoardValue.X) || GetAvailableMoves().Count == 0;
    }
    
    
    public GameResult GetGameResult()
    {
        if (HasWon(BoardValue.X)) return GameResult.XWin;
        if (HasWon(BoardValue.O)) return GameResult.OWin;
        return GetAvailableMoves().Count == 0 ? GameResult.Draw : GameResult.None;
    }

    #endregion
}