using UnityEngine;

public class InputManagerPC : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] BoardEventChannel boardEventChannel = default;
    [SerializeField] GameStateEventChannel gameStateEventChannel = default;
    [SerializeField] TickEventChannel tickEventChannel = default;

    [Header("Key bindings")]
    [SerializeField] KeyCode rotatePiece = KeyCode.UpArrow;
    [SerializeField] KeyCode moveRight = KeyCode.RightArrow;
    [SerializeField] KeyCode moveLeft = KeyCode.LeftArrow;
    [SerializeField] KeyCode speedDown = KeyCode.DownArrow;
    [SerializeField] KeyCode pauseGame = KeyCode.P;

    [SerializeField] float moveTickRate = 0.1f;

    float lastRightMoveTimestamp = 0;
    float lastLeftMoveTimestamp = 0;

    void Update()
    {
        if (Input.GetKeyDown(pauseGame))
        {
            if (GameStateManager.IsPaused) 
            { gameStateEventChannel.RaiseGameUnpause(); }
            else 
            { gameStateEventChannel.RaiseGamePause(); }
        }

        if (Input.GetKeyUp(speedDown))
        {
            tickEventChannel.RaiseSetStandardTick();
        }

        // All actions below can not be performed if the game is paused.
        if (GameStateManager.IsPaused)
        {
            return;
        }
        if (Input.GetKeyDown(speedDown))
        {
            tickEventChannel.RaiseSetFastTick();
        }

        // Actions below can not be performed if the tick down is disabled.
        if (!TickManager.IsTicking)
        {
            return;
        }

        if (Input.GetKey(moveRight))
        {
            if (Time.time - lastRightMoveTimestamp > moveTickRate)
            {
                lastRightMoveTimestamp = Time.time;
                boardEventChannel.RaiseMoveHorizontally(1);
            }
        }
        if (Input.GetKey(moveLeft))
        {
            if (Time.time - lastLeftMoveTimestamp > moveTickRate)
            {
                lastLeftMoveTimestamp = Time.time;
                boardEventChannel.RaiseMoveHorizontally(-1);
            }
        }

        if (Input.GetKeyDown(rotatePiece))
        {
            boardEventChannel.RaiseRotatePiece();
        }
    }
}
