using TMPro;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] private TMP_Text currentPlayerText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text winnerText;

    public void UpdateCurrentPlayer(int playerId, string playerName) 
    {
        if (currentPlayerText != null)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                currentPlayerText.text = $"Player {playerId}'s Turn";
            }
            else
            {
                currentPlayerText.text = $"{playerName}'s Turn";
            }
        }
    }
    public void UpdateTimer(int seconds) 
    {
        timerText.text = $"{seconds} s";
    }
    public void ShowGameOver(BoardModel.BoardState state) 
    {
        string resultText = "Draw";

        if (state == BoardModel.BoardState.Player1Won || state == BoardModel.BoardState.Player2Won)
        {
            resultText = state == BoardModel.BoardState.Player1Won ? $"Player1 Won" : $"Player2 Won";

        }
        Debug.Log(resultText);
        if (winnerText != null)
        {
            winnerText.text = resultText;
        }
        else
        {
            Debug.LogWarning("Result Text is not assigned");
        }

        if (gameOverPanel == null)
        {
            Debug.LogWarning("Game Over Panel is not assigned");
            return;
        }
        gameOverPanel.SetActive(true);
    }
    public void HideGameOver() 
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }
}
