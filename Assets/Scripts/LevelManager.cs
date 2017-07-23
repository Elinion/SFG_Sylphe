using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager instance = null;
	public GameObject pauseMenu;

	private Player player;
	private Score score;

	void Awake ()
	{
		ImplementSingleton ();

		FetchReferencedItems ();

		pauseMenu.SetActive (false);
	}

	public void PauseLevel ()
	{
		player.Freeze ();
		score.enabled = false;
		pauseMenu.SetActive (true);
	}

	public void ResumeLevel ()
	{
		player.Unfreeze ();
		score.enabled = true;
		pauseMenu.SetActive (false);
	}

	private void FetchReferencedItems ()
	{
		GameObject playerGameObject = GameObject.FindGameObjectWithTag (Tags.Player);
		player = playerGameObject.GetComponent<Player> ();
		GameObject scoreGameObject = GameObject.FindGameObjectWithTag (Tags.Score);
		score = scoreGameObject.GetComponent<Score> ();
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
