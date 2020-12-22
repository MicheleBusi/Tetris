using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Color")]
public class ColorSettings : ScriptableObject
{
    public Color background1    = default;
    public Color background2    = default;
    public Color[] pieceS       = default;
    public Color walls          = default;
    public Color UItext         = default;
    public Color UIbuttons      = default;
}
