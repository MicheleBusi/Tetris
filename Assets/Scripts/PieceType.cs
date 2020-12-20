using UnityEngine;

[CreateAssetMenu]
public class PieceType : ScriptableObject
{
    public Color color = default;
    public Vector2Int[] tilesLocalPositions = default;
}
