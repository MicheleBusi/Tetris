using UnityEngine;

[CreateAssetMenu(menuName = "Variables/String")]
public class StringVariable : BaseVariable
{
    [SerializeField] private string value = default;
    public string Value
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