namespace TicTacToeConsoleVersion;

public enum BoardValue
{
    X, O, None
}


public enum GameResult
{
    XWin = 1, OWin = -1, Draw = 0, None = 0
}


public enum GameType
{
    PlayerVsPlayer, PlayerVsAi, AiVsAi
}