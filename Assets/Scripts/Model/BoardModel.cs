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
        if (row > Rows || col > Columns)
        {
            return false;
        }

        if (!Cells[row,col].IsEmpty)
        {
            return false;
        }

        Cells[row,col].SetPlayer(playerId);
        return true;
    }

    public BoardState CheckBoardState(int player1Id, int player2Id)
    {
        List<BoardCellModel> cells = new();

        // 1. Sve sa strane
        if (!Cells[0,0].IsEmpty)
        {

            if (!Cells[0,1].IsEmpty && !Cells[0, 2].IsEmpty)
            {
                cells.Clear();
                cells.Add(Cells[0, 0]);
                cells.Add(Cells[0, 1]);
                cells.Add(Cells[0, 2]);

                if (HasPlayerWon(cells, player1Id))
                {
                    return BoardState.Player1Won;
                }
                if (HasPlayerWon(cells, player2Id))
                {
                    return BoardState.Player2Won;
                }
            }

            if (!Cells[1, 0].IsEmpty && !Cells[2, 0].IsEmpty)
            {
                cells.Clear();
                cells.Add(Cells[0, 0]);
                cells.Add(Cells[1, 0]);
                cells.Add(Cells[2, 0]);

                if (HasPlayerWon(cells, player1Id))
                {
                    return BoardState.Player1Won;
                }
                if (HasPlayerWon(cells, player2Id))
                {
                    return BoardState.Player2Won;
                }
            }

        }

        if (!Cells[2, 2].IsEmpty)
        {
            if (!Cells[2, 0].IsEmpty && !Cells[2,1].IsEmpty)
            {
                cells.Clear();
                cells.Add(Cells[2, 0]);
                cells.Add(Cells[2, 1]);
                cells.Add(Cells[2, 2]);
                if (HasPlayerWon(cells, player1Id))
                {
                    return BoardState.Player1Won;
                }
                if (HasPlayerWon(cells, player2Id))
                {
                    return BoardState.Player2Won;
                }
            }

            if (!Cells[0, 2].IsEmpty && !Cells[1, 2].IsEmpty)
            {
                cells.Clear();
                cells.Add(Cells[0, 2]);
                cells.Add(Cells[1, 2]);
                cells.Add(Cells[2, 2]);
                if (HasPlayerWon(cells, player1Id))
                {
                    return BoardState.Player1Won;
                }
                if (HasPlayerWon(cells, player2Id))
                {
                    return BoardState.Player2Won;
                }
            }
        }

        // 2. centar vertikala i horizontala
        // 3. dijagonale
        if (!Cells[1,1].IsEmpty)
        {
            // Centralna Vertikala
            if (!Cells[0, 1].IsEmpty && !Cells[2, 1].IsEmpty)
            {
                cells.Clear();
                cells.Add(Cells[0, 1]);
                cells.Add(Cells[1, 1]);
                cells.Add(Cells[2, 1]);
                if (HasPlayerWon(cells, player1Id))
                {
                    return BoardState.Player1Won;
                }
                if (HasPlayerWon(cells, player2Id))
                {
                    return BoardState.Player2Won;
                }
            }

            // Centralna Horizontala
            if (!Cells[1, 0].IsEmpty && !Cells[1, 2].IsEmpty)
            {
                cells.Clear();
                cells.Add(Cells[1, 0]);
                cells.Add(Cells[1, 1]);
                cells.Add(Cells[1, 2]);
                if (HasPlayerWon(cells, player1Id))
                {
                    return BoardState.Player1Won;
                }
                if (HasPlayerWon(cells, player2Id))
                {
                    return BoardState.Player2Won;
                }
            }

            // Dijagonala od gornjeg levog coska
            if (!Cells[0, 0].IsEmpty && !Cells[2, 2].IsEmpty)
            {
                cells.Clear();
                cells.Add(Cells[0, 0]);
                cells.Add(Cells[1, 1]);
                cells.Add(Cells[2, 2]);
                if (HasPlayerWon(cells, player1Id))
                {
                    return BoardState.Player1Won;
                }
                if (HasPlayerWon(cells, player2Id))
                {
                    return BoardState.Player2Won;
                }
            }
            // Dijagonala od gornjeg desnog coska
            if (!Cells[0, 2].IsEmpty && !Cells[2, 0].IsEmpty)
            {
                cells.Clear();
                cells.Add(Cells[0, 2]);
                cells.Add(Cells[1, 1]);
                cells.Add(Cells[2, 0]);
                if (HasPlayerWon(cells, player1Id))
                {
                    return BoardState.Player1Won;
                }
                if (HasPlayerWon(cells, player2Id))
                {
                    return BoardState.Player2Won;
                }
            }
        }

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

    private bool HasPlayerWon(List<BoardCellModel> cells, int playerId)
    {
        for (int i = 0; i < cells.Count; i++) 
        {
            if (cells[i].IsEmpty)
            {
                return false;
            }

            if (cells[i].PlayerId != playerId)
            {
                return false;
            }
        }

        return true;
    }
}
