using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
	public GameObject levelEndHUD;
	public GameObject newRecord;
	public GameObject bestScore;
	public Text bestScoreText;

	private Player player;
	private Score score;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag (Tags.Player).GetComponent<Player> ();
		score = GameObject.FindGameObjectWithTag (Tags.Score).GetComponent<Score> ();
		levelEndHUD.SetActive (false);
	}

	void Start ()
	{
		float score = GameManager.instance.dataToSave.bestScore;
		bestScoreText.text = Mathf.FloorToInt (score).ToString ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == Tags.Player) {
			levelEndHUD.SetActive (true);

			if (score.Points > GameManager.instance.dataToSave.bestScore) {
				GameManager.instance.dataToSave.bestScore = score.Points;
				GameManager.instance.SaveData ();
				bestScore.SetActive (true);
				bestScoreText.text = score.Points.ToString ();
			} else {
				newRecord.SetActive (false);
			}

			player.Freeze ();
			score.enabled = false;
		}
	}
}
