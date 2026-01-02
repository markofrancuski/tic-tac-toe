using JetBrains.Annotations;
using UnityEngine;

public class TestBoard : MonoBehaviour
{

    void Start()
    {
        int player1Id = 10;
        int player2Id = 20;
        Test1(player1Id, player2Id);
        Test2(player1Id, player2Id);
    }

    private void Test1(int player1Id, int player2Id)
    {
        BoardModel board = new BoardModel();
        board.TrySetPlayer(0, 0, player1Id);
        board.TrySetPlayer(0, 1, player1Id);
        board.TrySetPlayer(0, 2, player1Id);
        BoardModel.BoardState state = board.CheckBoardState(player1Id, player2Id);
        Debug.Log($"Test1 => {state}", this);
    }

    private void Test2(int player1Id, int player2Id)
    {
        BoardModel board = new BoardModel();
        board.TrySetPlayer(0, 0, player1Id);
        board.TrySetPlayer(0, 1, player1Id);
        board.TrySetPlayer(0, 2, player2Id);
        BoardModel.BoardState state = board.CheckBoardState(player1Id, player2Id);
        Debug.Log($"Test2 => {state}", this);
    }


}
