using UnityEngine;
using Lean.Gui;

public class InitToggleTo : MonoBehaviour
{
    [SerializeField] BoolVariable toggle = default;

    private void Awake()
    {
        GetComponent<LeanToggle>().On = toggle.Value;
    }
}
