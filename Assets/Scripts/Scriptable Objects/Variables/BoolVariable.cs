using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Bool")]
public class BoolVariable : BaseVariable
{
    [SerializeField] private bool value = default;
    public bool Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            OnValueChanged?.Raise();
        }
    }
}