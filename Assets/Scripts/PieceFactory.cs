using UnityEngine;

public class PieceFactory : MonoBehaviour
{
    [SerializeField] Sprite tileSprite = default;
    [SerializeField] PieceType[] pieceTypes = default;

    public int LastSpawnedIndex { get; private set; } = default;
    public int NextRandomIndex { get; private set; } = default;

    private void Awake()
    {
        NextRandomIndex = Random.Range(0, pieceTypes.Length);
    }

    private Piece GetPiece(int index)
    {
        PieceType pieceType = pieceTypes[index];

        GameObject newGameObject = new GameObject(pieceType.name);
        Piece newPiece = newGameObject.AddComponent<Piece>();
        foreach (var localPos in pieceType.tilesLocalPositions)
        {
            var GO = new GameObject("Tile");
            GO.transform.parent = newPiece.transform;
            GO.transform.localPosition = new Vector3(localPos.x, localPos.y, 0f);
            var SR = GO.AddComponent<SpriteRenderer>();
            SR.sprite = tileSprite;
            SR.color = pieceType.color;
            var tile = GO.AddComponent<Tile>();
            tile.FadeIn(0.5f);
        }

        newPiece.type = pieceType;
        newPiece.transform.parent = this.transform;

        LastSpawnedIndex = index;
        return newPiece;
    }

    public Piece GetNextPiece()
    {
        LastSpawnedIndex = NextRandomIndex;

        // Make sure you do not spawn the same piece twice.
        do
        {
            NextRandomIndex = Random.Range(0, pieceTypes.Length);
        } while (NextRandomIndex == LastSpawnedIndex);

        return GetPiece(LastSpawnedIndex);
    }
}
