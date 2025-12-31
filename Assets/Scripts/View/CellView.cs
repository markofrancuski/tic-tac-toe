using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    public event Action<int, int> OnClicked;

    [SerializeField] private TMP_Text Label;
    [SerializeField] private Button Button;

    private int Row;
    private int Column;

    private void Start()
    {
        Button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        Button.onClick.RemoveListener(OnButtonClicked);
    }
    private void OnButtonClicked()
    {
        Debug.Log($"Cell clicked: [{Row}, {Column}]");
        OnClicked?.Invoke(Row, Column);
    }

    public void Initialize(int row, int column)
    {
        this.Row = row;
        this.Column = column;
    }

    public void Display(string text)
    {
        Label.text = text;
    }
    public void SetInteractable(bool isInteractable)
    {
        Button.interactable = isInteractable;
    }
}
