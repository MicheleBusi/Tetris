using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Tick Event Channel")]
public class TickEventChannel : ScriptableObject
{
    public UnityAction OnTick;
    public UnityAction OnTickPause;
    public UnityAction OnTickUnpause;
    public UnityAction OnSetFastTick;
    public UnityAction OnSetStandardTick;
    public UnityAction<float> OnTickDelay;

    public void RaiseTickDelay(float delay)
    {
        OnTickDelay?.Invoke(delay);
    }

    public void RaiseTick()
    {
        OnTick?.Invoke();
    }

    public void RaiseTickPause()
    {
        OnTickPause?.Invoke();
    }

    public void RaiseSetFastTick()
    {
        OnSetFastTick?.Invoke();
    }
    public void RaiseSetStandardTick()
    {
        OnSetStandardTick?.Invoke();
    }

    public void RaiseTickUnpause()
    {
        OnTickUnpause?.Invoke();
    }
}
