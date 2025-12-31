using UnityEngine;

public class GameModel
{
    public const int DefaultTime = 40;
    public int RoundTime { get; protected set; }
    public int RemainingTime { get; protected set; }

    public int Player1Id { get; protected set; }
    public int Player2Id { get; protected set; }
    public int CurrentPlayerId { get; protected set; }

    public GameModel(int player1Id, int player2Id, int time = DefaultTime)
    {
        Player1Id = player1Id;
        Player2Id = player2Id;
        CurrentPlayerId = Player1Id;
        RoundTime = time;
    }

    public void SwitchPlayer()
    {
        if (CurrentPlayerId == Player1Id)
        {
            CurrentPlayerId = Player2Id;
        }
        else
        {
            CurrentPlayerId = Player1Id;
        }
    }

    public void ResetTimer()
    {
        RemainingTime = RoundTime;
    }
}
