using UnityEngine;

public class InputManagerPC : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] GameEvent togglePause = default;
    [SerializeField] GameEvent tickSetStandard = default;
    [SerializeField] GameEvent tickSetFast = default;
    [SerializeField] GameEvent pieceMoved = default;
    [SerializeField] GameEvent pieceRotated = default;

    [Header("Game Variables")]
    [SerializeField] BoolVariable isGamePaused = default;

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
            togglePause.Raise();
        }

        if (Input.GetKeyUp(speedDown))
        {
            tickSetStandard.Raise();
        }

        // All actions below can not be performed if the game is paused.
        if (isGamePaused.Value)
        {
            return;
        }

        if (Input.GetKeyDown(speedDown))
        {
            tickSetFast.Raise();
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
                pieceMoved.sentInt = 1;
                pieceMoved.Raise();
            }
        }
        if (Input.GetKey(moveLeft))
        {
            if (Time.time - lastLeftMoveTimestamp > moveTickRate)
            {
                lastLeftMoveTimestamp = Time.time;
                pieceMoved.sentInt = -1;
                pieceMoved.Raise();
            }
        }

        if (Input.GetKeyDown(rotatePiece))
        {
            pieceRotated.Raise();
        }
    }
}
