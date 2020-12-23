using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int")]
public class IntVariable : BaseVariable
{
    [SerializeField] private int value = default;
    public int Value
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
                OnValueChanged.sentInt = value;
                OnValueChanged.Raise();
            }
        }
    }
}