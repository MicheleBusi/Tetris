using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    [SerializeField] StringVariable savePath = default;
    [SerializeField] GameEventListener SaveGameCommand = default;

    void Awake()
    {
        savePath.Value = Path.Combine(Application.persistentDataPath, "saveFile");
    }

    void Save()
    {
    }

    void Update()
    {
        
    }
}
