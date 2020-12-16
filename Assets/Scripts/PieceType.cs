using UnityEngine;

[CreateAssetMenu]
public class PieceType : ScriptableObject
{
    public Material material = default;
    public Vector2Int[] tilesLocalPositions = default;
}
