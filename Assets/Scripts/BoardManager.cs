using System.Collections;
using UnityEngine;
using Lean.Transition;

public class BoardManager : MonoBehaviour
{
    // References
    [SerializeField] PieceFactory factory = default;
    [SerializeField] Transform loseLine = default;
    [SerializeField] Transform solidifiedTilesContainer = default;
    [SerializeField] Transform nextPieceDisplay = default;

    ScoreManager scoreManager;
    TickManager tickManager;
    SoundManager soundManager;
    GameStateManager gameStateManager;

    public static readonly Vector2Int BoardSize = new Vector2Int(10, 25);

    Tile[,] tiles = new Tile[BoardSize.x, BoardSize.y];

    Piece nextPiece = null;
    Piece activePiece = null;
    Tile[] activeTiles = null;

    void Awake()
    {
        tickManager = GetComponent<TickManager>();
        scoreManager = GetComponent<ScoreManager>();
        soundManager = GetComponent<SoundManager>();
        gameStateManager = GetComponent<GameStateManager>();

        ClearBoard();
    }

    private void ClearBoard()
    {
        for (int i = 0; i < BoardSize.x; i++)
        {
            for (int j = 0; j < BoardSize.y; j++)
            {
                tiles[i, j] = null;
            }
        }
    }

    private void Start()
    {
        GenerateNextPiece();
        SpawnPiece();
        tickManager.IsTicking = true;
    }

    public bool RotateActivePiece()
    {
        soundManager.RotatePiece();

        activePiece.Rotate(new Vector3(0, 0, 90));
        foreach (var t in activeTiles)
        {
            if (t.IsOutOfBounds())
            {
                activePiece.RotateBack();
                return false;
            }

            Vector2Int pos = t.GetGridPosition();
            bool tileOccupied = tiles[pos.x, pos.y];
            if (tileOccupied)
            {
                activePiece.RotateBack();
                return false;
            }
        }
        return true;
    }

    private void SpawnPiece()
    {
        activePiece = nextPiece;

        tickManager.DelayNextTickBy(0.3f);

        Vector3 startingPos = new Vector3(
                4f,
                BoardSize.y - 3,
                0f);

        activePiece.transform.positionTransition(
            startingPos,
            0.3f,
            LeanEase.Decelerate
            );

        activeTiles = activePiece.GetComponentsInChildren<Tile>();
        GenerateNextPiece();
    }

    private void GenerateNextPiece()
    {
        nextPiece = factory.GetNextPiece();
        nextPiece.transform.position = nextPieceDisplay.position;
    }

    public bool MovePieceHorizontally(int directionX)
    {
        Vector3 translation = new Vector3(directionX, 0, 0);
        activePiece.Move(translation);

        foreach (var t in activeTiles)
        {

            // Moved outside of board bounds?
            if (t.IsOutOfBounds())
            {
                activePiece.MoveBack();
                return false;
            }

            // Moved into a solidified piece?
            Vector2Int pos = t.GetGridPosition();
            if (tiles[pos.x, pos.y])
            {
                activePiece.MoveBack();
                return false;
            }
        }

        soundManager.MovePiece();
        return true;
    }

    public bool Tick()
    {
        soundManager.Tick();

        activePiece.Move(Vector3.down);

        // Check every active tile
        foreach (var t in activeTiles)
        {
            Vector2Int tPos = t.GetGridPosition();
            if (tPos.y > loseLine.position.y)
            {
                return false;
            }

            // Has reached bottom?
            bool reachedBottom = tPos.y < 0;
            if (reachedBottom)
            {
                activePiece.MoveBack();
                SolidifyActivePiece();
                return false;
            }

            // Is the tile already occupied?
            bool tileOccupied = tiles[tPos.x, tPos.y];
            if (tileOccupied)
            {
                activePiece.MoveBack();
                SolidifyActivePiece();
                return false;
            }
        }

        return true;
    }

    private void SolidifyActivePiece()
    {
        soundManager.SolidifyPiece();

        foreach (var t in activeTiles)
        {
            t.transform.parent = null;
            Vector2Int tPos = t.GetGridPosition();
            tiles[tPos.x, tPos.y] = t;
            t.transform.parent = solidifiedTilesContainer;

            if (tPos.y >= loseLine.position.y)
            {
                // Lose game
                tickManager.IsTicking = false;
                gameStateManager.GameLost();
                return;
            }
        }
        activeTiles = null;

        Destroy(activePiece.gameObject);
        activePiece = null;

        StartCoroutine(CheckCompletedRows());

        SpawnPiece();
    }


    int rowsCompletedAtThisTickRate = 0;
    IEnumerator CheckCompletedRows()
    {
        // Pause ticking while animations play
        tickManager.IsTicking = false;

        // For every row, check if all the tiles are true
        int deletedRowsCount = 0;
        for (int j = 0; j < BoardSize.y; j++)
        {
            bool rowComplete = true;
            for (int i = 0; i < BoardSize.x; i++)
            {
                if (tiles[i, j] == null)
                {
                    rowComplete = false;
                    break;
                }
            }

            // If tiles in the row are all true
            if (rowComplete)
            {
                // Delete row
                deletedRowsCount++;
                soundManager.DeleteRow(deletedRowsCount);
                scoreManager.OnRowDeleted(deletedRowsCount);

                for (int i = 0; i < BoardSize.x; i++)
                {
                    tiles[i, j].FadeOut();
                    tiles[i, j] = null;
                }

                float timeSinceFadeBegan = 0;
                while (timeSinceFadeBegan < Tile.fadeTileDuration)
                {
                    timeSinceFadeBegan += Time.deltaTime;
                    yield return null;
                }

                // Slide down the above rows
                SlideRowsDown(j + 1);

                float timeSinceSlideBegan = 0;
                while (timeSinceSlideBegan < Tile.slideDownDuration)
                {
                    timeSinceSlideBegan += Time.deltaTime;
                    yield return null;
                }

                //// Since we deleted one row, and shifted everything down,
                //// make sure we continue the next
                //// loop starting from this row again.
                j--;
            }
        }

        // Increase speed every 10 completed rows
        rowsCompletedAtThisTickRate += deletedRowsCount;
        if (rowsCompletedAtThisTickRate >= 10)
        {
            tickManager.IncreaseTickRate();
            rowsCompletedAtThisTickRate -= 10;
        }

        // Resume ticking
        tickManager.IsTicking = true;
    }

    private void SlideRowsDown(int aboveThisRow)
    {
        for (int j = aboveThisRow; j < BoardSize.y; j++)
        {
            for (int i = 0; i < BoardSize.x; i++)
            {
                tiles[i, j - 1] = tiles[i, j];
                tiles[i, j]?.SlideDown(1);
            }
        }
    }
}
