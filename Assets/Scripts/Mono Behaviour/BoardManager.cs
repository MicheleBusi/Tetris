using System.Collections;
using UnityEngine;
using Lean.Transition;

public class BoardManager : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] GameEvent tick = default;
    [SerializeField] GameEvent tickPaused = default;
    [SerializeField] GameEvent tickUnpaused = default;
    [SerializeField] GameEvent tickDelayed = default;
    [SerializeField] GameEvent pieceMoved = default;
    [SerializeField] GameEvent pieceRotated = default;
    [SerializeField] GameEvent pieceSolidified = default;
    [SerializeField] GameEvent rowDeleted = default;
    [SerializeField] GameEvent levelUp = default;
    [SerializeField] GameEvent gameLost = default;

    [Header("References")]
    [SerializeField] PieceFactory factory = default;
    [SerializeField] Transform loseLine = default;
    [SerializeField] Transform solidifiedTilesContainer = default;
    [SerializeField] Transform nextPieceDisplay = default;

    public static readonly Vector2Int BoardSize = new Vector2Int(10, 25);

    Tile[,] tiles = new Tile[BoardSize.x, BoardSize.y];

    Piece nextPiece = null;
    Piece activePiece = null;
    Tile[] activeTiles = null;

    int rowsCompletedAtThisTickRate = 0;

    private void Awake()
    {
        tick.RegisterListener(OnTick);
        pieceMoved.RegisterListener(OnPieceMove);
        pieceRotated.RegisterListener(OnPieceRotate);

        ClearBoard();
    }

    private void OnDestroy()
    {
        tick.UnregisterListener(OnTick);
        pieceMoved.UnregisterListener(OnPieceMove);
        pieceRotated.UnregisterListener(OnPieceRotate);
    }

    private void Start()
    {
        GenerateNextPiece();
        SpawnPiece();

        tickUnpaused.Raise();
    }

    void ClearBoard()
    {
        for (int i = 0; i < BoardSize.x; i++)
        {
            for (int j = 0; j < BoardSize.y; j++)
            {
                tiles[i, j] = null;
            }
        }
    }

    void OnPieceRotate()
    {
        activePiece.Rotate(new Vector3(0, 0, 90));
        foreach (var t in activeTiles)
        {
            if (t.IsOutOfBounds())
            {
                activePiece.RotateBack();
                return;
            }

            Vector2Int pos = t.GetGridPosition();
            bool tileOccupied = tiles[pos.x, pos.y];
            if (tileOccupied)
            {
                activePiece.RotateBack();
                return;
            }
        }
        return;
    }

    void SpawnPiece()
    {
        activePiece = nextPiece;

        tickDelayed.sentFloat = 0.3f;
        tickDelayed.Raise();
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

    void GenerateNextPiece()
    {
        nextPiece = factory.GetNextPiece();
        nextPiece.transform.position = nextPieceDisplay.position;
    }

    void OnPieceMove()
    {
        Vector3 translation = new Vector3(pieceMoved.sentInt, 0, 0);
        activePiece.Move(translation);

        foreach (var t in activeTiles)
        {

            // Moved outside of board bounds?
            if (t.IsOutOfBounds())
            {
                activePiece.MoveBack();
                return;
            }

            // Moved into a solidified piece?
            Vector2Int pos = t.GetGridPosition();
            if (tiles[pos.x, pos.y])
            {
                activePiece.MoveBack();
                return;
            }
        }
        return;
    }

    void OnTick()
    {
        activePiece.Move(Vector3.down);

        // Check every active tile
        foreach (var t in activeTiles)
        {
            Vector2Int tPos = t.GetGridPosition();
            if (tPos.y > loseLine.position.y)
            {
                return;
            }

            // Has reached bottom?
            bool reachedBottom = tPos.y < 0;
            if (reachedBottom)
            {
                activePiece.MoveBack();
                SolidifyActivePiece();
                return;
            }

            // Is the tile already occupied?
            bool tileOccupied = tiles[tPos.x, tPos.y];
            if (tileOccupied)
            {
                activePiece.MoveBack();
                SolidifyActivePiece();
                return;
            }
        }

        return;
    }

    void SolidifyActivePiece()
    {
        foreach (var t in activeTiles)
        {
            t.transform.parent = null;
            Vector2Int tPos = t.GetGridPosition();
            tiles[tPos.x, tPos.y] = t;
            t.transform.parent = solidifiedTilesContainer;

            if (tPos.y >= loseLine.position.y)
            {
                // Lose game
                tickPaused.Raise();
                gameLost.Raise();
                return;
            }
        }
        activeTiles = null;

        Destroy(activePiece.gameObject);
        activePiece = null;

        pieceSolidified.Raise();

        StartCoroutine(CheckCompletedRows());

        SpawnPiece();
    }

    IEnumerator CheckCompletedRows()
    {
        // Pause ticking while animations play
        tickPaused.Raise();

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
                rowDeleted.sentInt = deletedRowsCount;
                rowDeleted.Raise();

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
            //boardEventChannel.RaiseLevelUp(++level);
            levelUp.Raise();
            rowsCompletedAtThisTickRate -= 10;
        }

        // Resume ticking
        tickUnpaused.Raise();
    }

    void SlideRowsDown(int aboveThisRow)
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

    void FadeOutAllTiles()
    {
        foreach (var t in tiles)
        {
            t?.FadeOut();
        }
        foreach (var t in activeTiles)
        {
            t?.FadeOut();
        }
        nextPiece?.transform.localScaleTransition(Vector3.zero, 0.5f);
    }
}
