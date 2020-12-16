using UnityEngine;

public class SetResolution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }
}
