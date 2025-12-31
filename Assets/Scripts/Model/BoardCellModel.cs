using UnityEngine;

public class BoardCellModel
{
    private const int DefaultPlayerId = -1;

    public bool IsEmpty => PlayerId == DefaultPlayerId;

    public int PlayerId { get; protected set; } = DefaultPlayerId;

    public BoardCellModel(int playerId = DefaultPlayerId)
    {
        SetPlayer(playerId);
    }

    public void SetPlayer(int playerId)
    {
        PlayerId = playerId;
    }
}
