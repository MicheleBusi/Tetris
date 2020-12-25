using System.IO;
using UnityEngine;

// TO DO: This should really be split into 2 classes eventually

public class PersistentStorage : MonoBehaviour
{
	//[SerializeField] GameEvent saveGameData = default;
	//[SerializeField] GameEvent loadGameData = default;

	[SerializeField] GameEvent saveHighScores = default;
	[SerializeField] GameEvent loadHighScores = default;
	[SerializeField] HighScores highScores = default;

	string savePath;

	void Awake()
	{
		savePath = Path.Combine(Application.persistentDataPath, "highScores");
		Debug.Log("Save path: " + savePath);
	}

    private void OnEnable()
    {
		saveHighScores.RegisterListener(SaveHighScores);
		loadHighScores.RegisterListener(LoadHighScores);
	}

    private void OnDisable()
	{
		saveHighScores.UnregisterListener(SaveHighScores);
		loadHighScores.UnregisterListener(LoadHighScores);
	}

	void SaveHighScores()
    {
		Debug.Log("Saving new high scores");
		SaveObject(highScores);
    }

	void LoadHighScores()
	{
		Debug.Log("Loading high scores");
		LoadObject(highScores);
    }

    void SaveObject(IPersistableObject o)
	{
		// TO DO: CAREFUL! THIS IS TEMPORARY! Will break stuff when you try
		// to perform more complex saving operations!
		File.Delete(savePath);
		using (
			var writer = new BinaryWriter(File.Open(savePath, FileMode.Create))
		)
		{
			o.Save(new GameDataWriter(writer));
		}
	}

	void LoadObject(IPersistableObject o)
	{
        if (!File.Exists(savePath))
        {
			SaveHighScores();
			return;
		}

		using (
			var reader = new BinaryReader(File.Open(savePath, FileMode.Open))
		)
		{
			o.Load(new GameDataReader(reader));
		}
	}
}
