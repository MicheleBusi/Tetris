using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Game State Event Channel")]
public class GameStateEventChannel : ScriptableObject
{
    public UnityAction OnGamePause;
    public UnityAction OnGameUnpause;
    public UnityAction OnGameLost;
    public UnityAction OnGameStart;

    public void RaiseGamePause()
    {
        OnGamePause?.Invoke();
    }

    public void RaiseGameUnpause()
    {
        OnGameUnpause?.Invoke();
    }

    public void RaiseGameLost()
    {
        OnGameLost?.Invoke();
    }

    public void RaiseGameStart()
    {
        OnGameStart?.Invoke();
    }
}
