using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Music Event Channel")]
public class MusicEventChannel : ScriptableObject
{
    public UnityAction OnMusicPause;
    public UnityAction OnMusicUnpause;

    public void RaiseMusicPause()
    {
        OnMusicPause?.Invoke();
    }
    public void RaiseMusicUnpause()
    {
        OnMusicUnpause?.Invoke();
    }
}
