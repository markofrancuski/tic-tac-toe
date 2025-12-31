using System;
using UnityEngine;

public class GameModel
{
    public const int DefaultTime = 40;
    
    public event Action OnPlayerSwitched;
    public event Action<int> OnTimerChanged;
    public event Action<int> OnPlayerTimedOut;
    public event Action<BoardModel.BoardState> OnGameEnded;

    public BoardModel Board { get; private set; }

    public bool IsTimeUp => RemainingTime <= 0;
    public int RoundTime { get; protected set; }
    public int RemainingTime { get; protected set; }
    public int Player1Id { get; protected set; }
    public int Player2Id { get; protected set; }
    public int CurrentPlayerId { get; protected set; }
    public BoardModel.BoardState CurrentBoardState { get; protected set; }

    public GameModel(int player1Id, int player2Id, int time = DefaultTime)
    {
        Player1Id = player1Id;
        Player2Id = player2Id;
        CurrentPlayerId = Player1Id;
        RoundTime = time;
        RemainingTime = time;

        Board = new BoardModel();
        CurrentBoardState = BoardModel.BoardState.None;
    }

    public void SwitchPlayer()
    {
        CurrentPlayerId = CurrentPlayerId == Player1Id ? Player2Id : Player1Id;
        OnPlayerSwitched?.Invoke();
    }
    public void ResetTimer()
    {
        RemainingTime = RoundTime;
        OnTimerChanged?.Invoke(RemainingTime);
    }
    public void DecrementTime()
    {
        if (RemainingTime > 0)
        {
            RemainingTime--;
            OnTimerChanged?.Invoke(RemainingTime);

            if (RemainingTime == 0)
            {
                int timedOutPlayer = CurrentPlayerId;
                OnPlayerTimedOut?.Invoke(timedOutPlayer);

                CurrentBoardState = timedOutPlayer == Player1Id ? BoardModel.BoardState.Player2Won : BoardModel.BoardState.Player1Won;
                OnGameEnded?.Invoke(CurrentBoardState);
            }
        }
    }

    public bool MakeMove(int row, int col)
    {
        if (CurrentBoardState != BoardModel.BoardState.None)
        {
            return false;
        }

        if (!Board.TrySetPlayer(row, col, CurrentPlayerId))
        {
            return false;
        }

        BoardModel.BoardState newState = Board.CheckBoardState(Player1Id, Player2Id);

        if (newState != BoardModel.BoardState.None)
        {
            CurrentBoardState = newState;
            OnGameEnded?.Invoke(newState);
        }
        else
        {
            SwitchPlayer();
            ResetTimer();
        }

        return true;
    }

    public void Reset()
    {
        Board = new BoardModel();
        CurrentPlayerId = Player1Id;
        CurrentBoardState = BoardModel.BoardState.None;
        RemainingTime = RoundTime;
        OnTimerChanged?.Invoke(RemainingTime);
        OnPlayerSwitched?.Invoke();
    }

    public string GetPlayerSymbol(int playerId)
    {
        if (playerId == Player1Id) return "X";
        if (playerId == Player2Id) return "0";

        return string.Empty;
    }

}
