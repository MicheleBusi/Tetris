using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Save Data/High Scores")]
public class HighScores : ScriptableObject, IPersistableObject
{
    public int[] scores = new int[5];

    private void OnEnable()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = 0;
        }
    }

    public void Load(GameDataReader reader)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = reader.ReadInt();
        }
    }

    public void Save(GameDataWriter writer)
    {
        for (int i = 0; i < scores.Length; i++)
        {
            writer.Write(scores[i]);
        }
    }
}
