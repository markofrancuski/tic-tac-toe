using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    public event Action<int, int> OnClicked;

    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _button;

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    public void Initialize(int row, int column)
    {
        _button.onClick.AddListener(() =>
        {
            OnClicked?.Invoke(row, column);
        });
    }

    public void Display(string text)
    {
        _label.text = text;
    }
    public void SetInteractable(bool isInteractable)
    {
        _button.interactable = isInteractable;
    }

    public void Clear()
    {
        Display(string.Empty);
        SetInteractable(true);
    }
    public void Highlight(Color color)
    {
        _button.GetComponent<Image>().color = color;
    }

}
