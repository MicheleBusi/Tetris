using UnityEngine;

public class BaseVariable : ScriptableObject
{
    [SerializeField] protected GameEvent OnValueChanged = default;
}
