using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] GameSettings settings = default;

    [SerializeField] MeshRenderer skyboxRenderer = default;

    // Start is called before the first frame update
    void Start()
    {
        skyboxRenderer.material = settings.skyboxMaterial;

        // TO DO: Music on or off based on settings
    }

    public void SetMusicToggle(bool toggle)
    {
        settings.MusicOn = toggle;
        // TO DO: Turn music on or off based on settings
    }

    public void SetSkyboxMaterial(Material material)
    {
        settings.skyboxMaterial = material;
        skyboxRenderer.material = material;
    }
}
