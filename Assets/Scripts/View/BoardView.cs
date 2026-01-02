using UnityEngine;
using TMPro;
using System;

public class BoardView : MonoBehaviour
{
    public event Action<int, int> OnCellClicked; // row, col

    [SerializeField] private CellView[,] _cells;


    public void Initialize(CellView[,] cells)
    {
        _cells = cells;
        for (int row = 0; row < _cells.GetLength(0); row++)
        {
            for (int col = 0; col < _cells.GetLength(1); col++)
            {
                _cells[row, col].OnClicked += FireClickedEvent;
            }
        }
    }

    private void OnDestroy()
    {
        for (int row = 0; row < _cells.GetLength(0); row++)
        {
            for (int col = 0; col < _cells.GetLength(1); col++)
            {
                _cells[row, col].OnClicked -= FireClickedEvent;
            }
        }
    }

    public void UpdateCell(int row, int column, string symbol)
    {
        if (_cells != null)
        {
            Debug.LogError($"BoardView not initialized!", this);
            return;
        }
        _cells[row, column].Display(symbol);
    }

    public void DisableAllCells()
    {
        if (_cells == null)
            return;

        for (int row = 0; row < _cells.GetLength(0); row++)
        {
            for (int col = 0; col < _cells.GetLength(1); col++)
            {
                _cells[row, col].SetInteractable(false);
            }
        }
    }

    public void EnableAllCells()
    {
        if (_cells == null)
            return;
        for (int row = 0; row < _cells.GetLength(0); row++)
        {
            for (int col = 0; col < _cells.GetLength(1); col++)
            {
                _cells[row, col].SetInteractable(true);
            }
        }
    }

    private void FireClickedEvent(int row, int column)
    {
        OnCellClicked?.Invoke(row, column);
    }
}
