using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardModel
{

    public enum BoardState
    {
        None,
        Player1Won,
        Player2Won,
        Draw
    }

    // <row, column>
    public event Action<int, int> OnCellChanged; 

    public BoardState State { get; protected set; }
    public int Rows { get; protected set; } = 3;
    public int Columns { get; protected set; } = 3;
    public BoardCellModel[,] Cells { get; protected set; }

    public BoardModel()
    {
        Cells = new BoardCellModel[Rows, Columns];

        for (int row = 0; row < Cells.GetLength(0); row++)
        {
            for (int col = 0; col < Cells.GetLength(1); col++)
            {
                Cells[row, col] = new BoardCellModel();
            }
        }
    }

    public bool TrySetPlayer(int row, int col, int playerId)
    {
        if (row < 0 || row >= Rows || col <0 || col >= Columns)
        {
            return false;
        }

        bool success = Cells[row, col].SetPlayer(playerId);
        if (success)
        {
            OnCellChanged?.Invoke(row, col);
        }

        return success;
    }

    public BoardState CheckBoardState(int player1Id, int player2Id)
    {
        for (int row = 0; row < Rows; row++)
        {
            int winnerId = CheckLine(Cells[row, 0], Cells[row, 1], Cells[row, 2]);
            if (winnerId != -1)
            {
                return winnerId == player1Id ? BoardState.Player1Won : BoardState.Player2Won;
            }
        }
        for (int col = 0; col < Columns; col++)
        {
            int winnerId = CheckLine(Cells[0, col], Cells[1, col], Cells[2, col]);
            if (winnerId != -1)
            {
                return winnerId == player1Id ? BoardState.Player1Won : BoardState.Player2Won;
            }
        }
    
        int diag1Winner = CheckLine(Cells[0, 0], Cells[1, 1], Cells[2, 2]);
        if (diag1Winner != -1)
            return diag1Winner == player1Id ? BoardState.Player1Won : BoardState.Player2Won;


        int diag2Winner = CheckLine(Cells[0, 2], Cells[1, 1], Cells[2, 0]);
        if (diag2Winner != -1)
            return diag2Winner == player1Id ? BoardState.Player1Won : BoardState.Player2Won;

        if (IsBoardFull())
        {
            return BoardState.Draw;
        }

        return BoardState.None;
    }

    public bool IsBoardFull()
    {
        for (int row = 0; row < Cells.GetLength(0); row++)
        {
            for(int col = 0; col < Cells.GetLength(1); col++)
            {
                if (Cells[row,col].IsEmpty)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private int CheckLine(BoardCellModel cell1, BoardCellModel cell2, BoardCellModel cell3)
    {
        if (cell1.IsEmpty || cell2.IsEmpty || cell3.IsEmpty)
        {
            return -1;
        }

        if (cell1.PlayerId == cell2.PlayerId && cell2.PlayerId == cell3.PlayerId)
        {
            return cell1.PlayerId;
        }

        return -1;
    }
}
