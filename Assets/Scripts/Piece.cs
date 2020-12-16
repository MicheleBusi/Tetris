using UnityEngine;

public class Piece : MonoBehaviour
{
    public PieceType type = null;

    Vector3 lastRotation = Vector3.zero;
    Vector3 lastTranslation = Vector3.zero;

    public void Rotate(Vector3 euler)
    {
        if (type.name.Equals("O"))
        {
            // The O piece should not be rotated.
            return;
        }
        transform.Rotate(euler);
        lastRotation = euler;
    }

    public void RotateBack()
    {
        transform.Rotate(-lastRotation);
        lastRotation = Vector3.zero;
    }

    public void Move(Vector3 translation)
    {
        transform.Translate(translation, Space.World);
        lastTranslation = translation;
    }

    public void MoveBack()
    {
        transform.Translate(-lastTranslation, Space.World);
        lastTranslation = Vector3.zero;
    }

}
