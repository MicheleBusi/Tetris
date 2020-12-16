using UnityEngine;

public class InputManagerMobile : MonoBehaviour
{
    public bool MoveRight { get; set; }
    public bool MoveLeft { get; set; }


    [SerializeField] float moveTickRate = 0.1f;

    [SerializeField] KeyCode pauseGame = KeyCode.P;

    TickManager tickManager;
    BoardManager boardManager;
    GameStateManager pauseManager;

    float lastRightMoveTimestamp = 0;
    float lastLeftMoveTimestamp = 0;

    private void Awake()
    {
        tickManager = GetComponent<TickManager>();
        boardManager = GetComponent<BoardManager>();
        pauseManager = GetComponent<GameStateManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseGame))
        {
            if (pauseManager.IsPaused)
            {
                pauseManager.UnpauseGame();
            }
            else
            {
                pauseManager.PauseGame();
            }
        }

        // All actions below can not be performed if the game is paused.
        if (pauseManager.IsPaused)
        {
            return;
        }

        // Actions below can not be performed if the tick down is disabled.
        if (!tickManager.IsTicking)
        {
            return;
        }

        if (MoveRight)
        {
            if (Time.time - lastRightMoveTimestamp > moveTickRate)
            {
                lastRightMoveTimestamp = Time.time;
                boardManager.MovePieceHorizontally(1);
            }
        }
        if (MoveLeft)
        {
            if (Time.time - lastLeftMoveTimestamp > moveTickRate)
            {
                lastLeftMoveTimestamp = Time.time;
                boardManager.MovePieceHorizontally(-1);
            }
        }
    }

    public void OnDoubleTap()
    {
        boardManager.RotateActivePiece();
    }
}
