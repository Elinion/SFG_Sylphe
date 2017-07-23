using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
	public GameObject levelEndHUD;
	public GameObject bestScore;
	public Text bestScoreText;

	private GameObject player;
	private Score score;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag (Tags.Player);
		score = GameObject.FindGameObjectWithTag (Tags.Score).GetComponent<Score> ();
		levelEndHUD.SetActive (false);
	}

	void Start ()
	{
		float displayScore = GameManager.instance.saveBlob.bestScore * score.displayScoreMultiplier;
		bestScoreText.text = Mathf.FloorToInt (displayScore).ToString ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == Tags.Player) {
			levelEndHUD.SetActive (true);
			player.GetComponent<PlayerControls> ().enabled = false;
			Rigidbody2D rb = player.GetComponent<Rigidbody2D> ();
			rb.velocity = Vector2.zero;
			rb.gravityScale = 0f;

			if (score.Points > GameManager.instance.saveBlob.bestScore) {
				GameManager.instance.saveBlob.bestScore = score.Points;
				GameManager.instance.SaveData ();
				bestScore.SetActive (true);
				bestScore.SetActive (true);
			} 
		}
	}
}
