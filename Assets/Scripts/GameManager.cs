using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
	[System.Serializable]
	public class DataToSave
	{
		public int bestScore = 0;
	}

	public static GameManager instance = null;

	public DataToSave dataToSave;
	private string savePath;

	void Awake ()
	{
		ImplementSingleton ();

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
		bf.Serialize (file, dataToSave);
		file.Close ();
	}

	public void LoadData ()
	{
		System.Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
		savePath = Application.persistentDataPath + "/save.dat";
		dataToSave = new DataToSave ();

		if (File.Exists (savePath)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (savePath, FileMode.Open);
			dataToSave = (DataToSave)bf.Deserialize (file);
			file.Close ();
		}
	}

	public void LoadScene (string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}

	public void QuitApplication ()
	{
		Application.Quit ();
	}

	private void ImplementSingleton ()
	{
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);    
		}
	}
}
