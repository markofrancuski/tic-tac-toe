using UnityEngine;

public class BoardCellModel
{
    private const int DefaultPlayerId = -1;

    public bool IsEmpty => PlayerId == DefaultPlayerId;

    public int PlayerId { get; protected set; } = DefaultPlayerId;

    public BoardCellModel()
    {
    }

    public bool SetPlayer(int playerId)
    {
        if (!IsEmpty) return false;
        if (playerId < 0) return false;
        
        PlayerId = playerId;
        return true;
    }
}
