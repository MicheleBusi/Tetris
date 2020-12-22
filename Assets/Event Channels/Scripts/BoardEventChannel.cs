using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Board Event Channel")]
public class BoardEventChannel : ScriptableObject
{
    public UnityAction OnBoardCleared;
    public UnityAction<int> OnRowDeleted;
    public UnityAction<int> OnLevelUp;
    public UnityAction OnSolidfyPiece;
    public UnityAction<int> OnMovePieceHorizontally;
    public UnityAction OnRotatePiece;

    public void BoardCleared()
    {
        OnBoardCleared?.Invoke();
    }

    public void RaiseLevelUp(int level)
    {
        OnLevelUp?.Invoke(level);
    }

    public void RaiseRowDeleted(int comboIndex)
    {
        OnRowDeleted?.Invoke(comboIndex);
    }

    public void RaiseSolidifyPiece()
    {
        OnSolidfyPiece?.Invoke();
    }

    public void RaiseMoveHorizontally(int deltaX)
    {
        OnMovePieceHorizontally?.Invoke(deltaX);
    }
    public void RaiseRotatePiece()
    {
        OnRotatePiece?.Invoke();
    }
}
