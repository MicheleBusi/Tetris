using UnityEngine;

public class InputManagerPC : MonoBehaviour
{
    [SerializeField] KeyCode rotatePiece = KeyCode.UpArrow;

    [SerializeField] KeyCode moveRight = KeyCode.RightArrow;
    [SerializeField] KeyCode moveLeft = KeyCode.LeftArrow;
    [SerializeField] float moveTickRate = 0.1f;

    [SerializeField] KeyCode speedDown = KeyCode.DownArrow;

    [SerializeField] KeyCode pauseGame = KeyCode.P;

    TickManager tickManager;
    BoardManager boardManager;
    GameStateManager gameStateManager;

    float lastRightMoveTimestamp = 0;
    float lastLeftMoveTimestamp = 0;

    private void Awake()
    {
        tickManager = GetComponent<TickManager>();
        boardManager = GetComponent<BoardManager>();
        gameStateManager = GetComponent<GameStateManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseGame))
        {
            if (gameStateManager.IsPaused)
            {
                gameStateManager.UnpauseGame();
            }
            else
            {
                gameStateManager.PauseGame();
            }
        }

        if (Input.GetKeyUp(speedDown))
        {
            tickManager.SetStandardTick();
        }

        // All actions below can not be performed if the game is paused.
        if (gameStateManager.IsPaused)
        {
            return;
        }
        if (Input.GetKeyDown(speedDown))
        {
            tickManager.SetFastTick();
        }

        // Actions below can not be performed if the tick down is disabled.
        if (!tickManager.IsTicking)
        {
            return;
        }

        if (Input.GetKey(moveRight))
        {
            if (Time.time - lastRightMoveTimestamp > moveTickRate)
            {
                lastRightMoveTimestamp = Time.time;
                boardManager.MovePieceHorizontally(1);
            }
        }
        if (Input.GetKey(moveLeft))
        {
            if (Time.time - lastLeftMoveTimestamp > moveTickRate)
            {
                lastLeftMoveTimestamp = Time.time; 
                boardManager.MovePieceHorizontally(-1);
            }
        }

        if (Input.GetKeyDown(rotatePiece))
        {
            boardManager.RotateActivePiece();
        }
    }
}
