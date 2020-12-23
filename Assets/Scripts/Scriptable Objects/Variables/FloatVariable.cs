using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Float")]
public class FloatVariable : BaseVariable
{
    [SerializeField] private float value = default;
    public float Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            if (OnValueChanged)
            {
                OnValueChanged.sentFloat = value;
                OnValueChanged.Raise();
            }
        }
    }
}