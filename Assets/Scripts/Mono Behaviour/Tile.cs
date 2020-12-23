using System.Collections;
using UnityEngine;
using Lean.Transition;

public class Tile : MonoBehaviour
{
    public static readonly float fadeTileDuration = 0.25f;
    public static readonly float slideDownDuration = 0.25f;

    public void SetGridPosition(Vector2Int pos)
    {
        transform.position = new Vector3(pos.x, pos.y, 0f);
    }

    public Vector2Int GetGridPosition()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y)
            );
    }

    public void MoveDownInGrid(int byHowManyRows)
    {
        transform.position += new Vector3(0, -byHowManyRows, 0);
    }

    public bool IsOutOfBounds()
    {
        Vector2Int pos = GetGridPosition();
        
        return (
            pos.x < 0                           ||
            pos.x >= BoardManager.BoardSize.x   ||
            pos.y < 0                           ||
            pos.y >= BoardManager.BoardSize.y
            );
    }

    static readonly Vector3 tileLocalScale = new Vector3(0.9f, 0.9f, 0.9f);
    public void FadeIn(float duration)
    {
        transform.localScale = Vector3.zero;
        transform.localScaleTransition(tileLocalScale, duration, LeanEase.Smooth);
    }

    public void FadeOut()
    {
        transform.localScaleTransition(Vector3.zero, fadeTileDuration, LeanEase.Smooth);
        Destroy(gameObject, fadeTileDuration + 0.2f);
    }

    public IEnumerator DelayThenSlideDown(float delay, int slideMagnitude)
    {
        float elapsedTime = 0;
        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SlideDown(slideMagnitude);
    }

    public void SlideDown(int slideMagnitude)
    {
        float targetPosY = transform.position.y - slideMagnitude;
        transform.positionTransition_Y(targetPosY, slideDownDuration, LeanEase.Decelerate);
    }    
}
