using UnityEngine;

[CreateAssetMenu(menuName = "Settings/General")]
public class GeneralSettings : ScriptableObject
{
    public ColorSettings color = default;
    public bool MusicOn;
}