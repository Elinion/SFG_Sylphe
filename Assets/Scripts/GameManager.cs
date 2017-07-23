using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
	[System.Serializable]
	public class SaveBlob
	{
		public int bestScore = 0;
	}

	public static GameManager instance = null;

	public SaveBlob saveBlob;
	private string savePath;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);    
		}

		System.Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
		savePath = Application.persistentDataPath + "/save.dat";
		saveBlob = new SaveBlob ();
		LoadData ();
	}

	public void RestartLevel ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public void SaveData ()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (savePath);
		bf.Serialize (file, saveBlob);
		file.Close ();
	}

	public void LoadData ()
	{
		if (File.Exists (savePath)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (savePath, FileMode.Open);
			saveBlob = (SaveBlob)bf.Deserialize (file);
			file.Close ();
		}
	}

	public void LoadLevel (string levelName)
	{
		SceneManager.LoadScene (levelName);
	}

	public void QuitApplication ()
	{
		Application.Quit ();
	}
}
