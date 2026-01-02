using UnityEngine;

public class GameController : MonoBehaviour
{

    // Instantiation of CellViews.
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _prefab;

    public GameView GameView;
    public BoardView BoardView;
    public GameModel GameModel;

    public int Player1Id = 1;
    public string Player1Name = "Player X";

    public int Player2Id = 2;
    public string Player2Name = "Player O";

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        GameModel = new GameModel(Player1Id, Player2Id, Player1Name, Player2Name);

        BoardModel boardModel = GameModel.Board;
        CellView[,] cells = new CellView[boardModel.Rows, boardModel.Columns];

        for (int row = 0; row < boardModel.Cells.GetLength(0); row++)
        {
            for (int col = 0; col < boardModel.Cells.GetLength(1); col++)
            {
                cells[row,col] = GameObject.Instantiate(_prefab, _parent).GetComponent<CellView>();
                cells[row, col].Clear();
            }
        }
        BoardView.Initialize(cells);
        GameView.UpdateCurrentPlayer(GameModel.CurrentPlayerId, GameModel.GetCurrentPlayerName());
        GameView.UpdateTimer(GameModel.RemainingTime);
        GameView.HideGameOver();

        // Sub on all Events
        GameModel.OnPlayerSwitched += HandleSwitchPlayer;
        GameModel.OnTimerChanged += HandleTimerChanged;
        GameModel.OnPlayerTimedOut += HandlePlayerTimedOut;
        GameModel.OnGameEnded += HandleGameEnded;

        boardModel.OnCellChanged += HandleOnCellChanged;
    }

    private void HandleSwitchPlayer()
    {
        GameModel.SwitchPlayer();
    }
    private void HandlePlayerTimedOut(int playerId)
    {
        HandleGameEnded(GameModel.CurrentBoardState);
    }
    private void HandleTimerChanged(int time)
    {
        GameView.UpdateTimer(time);
    }

    private void HandleGameEnded(BoardModel.BoardState resultState)
    {
        GameView.ShowGameOver(resultState);
    }

    private void HandleOnCellChanged(int row, int col)
    {
        BoardView.UpdateCell
        (
            row, 
            col, 
            GameModel.GetPlayerSymbol(GameModel.Board.Cells[row,col].PlayerId)
        );
    }



}
