namespace TicTacToeConsoleVersion;

public static class ComputerPlayer
{
    public static (int, (int, int)) MinMax(GameBoard board, bool isMaximizing)
    {
        if (board.IsGameOver()) return (Convert.ToInt32(board.GetGameResult()), (1, -1));

        var bestMove = (-1, -1);
        int bestValue;
        BoardValue symbol;
        if (isMaximizing)
        {
            bestValue = -int.MaxValue;
            symbol = BoardValue.X;
        }
        else
        {
            bestValue = int.MaxValue;
            symbol = BoardValue.O;
        }

        foreach (var move in board.GetAvailableMoves())
        {
            board.SetValue(move, symbol);
            var hypotheticalValue = MinMax(board, !isMaximizing).Item1;
            board.ClearValue(move);

            if (isMaximizing && hypotheticalValue > bestValue)
            {
                bestValue = hypotheticalValue;
                bestMove = move;
            }

            if (!isMaximizing && hypotheticalValue < bestValue)
            {
                bestValue = hypotheticalValue;
                bestMove = move;
            }
        }

        return (bestValue, bestMove);
    }


    /// <summary>
    /// Optimized game solving
    /// </summary>
    /// <param name="board">Game board</param>
    /// <param name="depth">Number of remained free cells</param>
    /// <param name="isMaximizing">True if X player, false if O player</param>
    /// <param name="alpha">-inf by default, shouldn't be changed</param>
    /// <param name="beta">inf by default, shouldn't be changed</param>
    /// <returns>Game board score and cell, where to place move</returns>
    public static (int, (int, int)) MinMaxAlphaBeta(GameBoard board, bool isMaximizing, int depth = 10,
        int alpha = -int.MaxValue, int beta = int.MaxValue)
    {
        var bestMove = (-1, -1);
        if (depth == 0 || board.IsGameOver()) return (Convert.ToInt32(board.GetGameResult()), bestMove);

        
        var symbol = isMaximizing ? BoardValue.X : BoardValue.O;

        foreach (var move in board.GetAvailableMoves())
        {
            board.SetValue(move, symbol);
            var result = MinMaxAlphaBeta(board, !isMaximizing, depth: depth - 1, alpha: alpha, beta: beta);
            board.ClearValue(move);
            if (isMaximizing && result.Item1 > alpha)
            {
                alpha = result.Item1;
                bestMove = move;
            }

            if (!isMaximizing && result.Item1 < beta)
            {
                beta = result.Item1;
                bestMove = move;
            }

            if (alpha >= beta) break;
        }

        return isMaximizing ? (alpha, bestMove) : (beta, bestMove);
    }



    #region Async

    public static async Task<(int, (int, int))> AsyncMinMax(GameBoard board, bool isMaximizing)
    {
        return MinMax(board, isMaximizing);
    }

    public static async Task<(int, (int, int))> AsyncMinMaxAlphaBeta(GameBoard board, bool isMaximizing, int depth = 10,
        int alpha = -int.MaxValue, int beta = int.MaxValue)
    {
        return MinMaxAlphaBeta(board, isMaximizing, depth: depth, alpha: alpha, beta: beta);
    }

    #endregion
}